using MySql.Data.MySqlClient;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Joueur : MonoBehaviour {
    public static int IDJoueur;
    public static string NomJoueur;
    public static int[] MesRessources;
    public static int[] MesValeursCompetences;
    public static int[] MesObjets;
    public static bool[] MesDiplomes;
    public static bool[] MesTrophees;
    public static bool[] MesArtefacts;
    public static AutreJoueur[] MesAmis;
    public static bool[] MesActionsSocialesSkill;
    public static bool[] MesActionsSocialesObjet;
    public static int attenteMaj;
    public static DateTime dateDerniereCo;
    public static DateTime DateActuel = System.DateTime.Now;
    public static int DateActuelMinute = System.DateTime.Now.Minute;
    public static int DateActuelSeconde = System.DateTime.Now.Second;
    public static int SecondesUpdate = 10;
    
    private string url = Configuration.url + "scripts/";
    private string urlRess = Configuration.url + "scripts/scriptsRessources/";
    private WWW download;
    private string monJson;
    private JSONNode monNode;

    public static bool continueDaedelus = false;
    public static bool continueRecupActionsSociales = false;
    public static bool continueTransfertRessources = false;
    public static bool continueMesAmis = false;
    public static bool continueMissionJouable = false;

    // Use this for initialization
    public void Start() {
        Debug.Log("Instanciation du joueur en cours");
        DontDestroyOnLoad(gameObject);

        // On répète toutes les SecondesUpdate l'incrémentation des ressources
        InvokeRepeating("IncrementationRessourcesStatic", 0, SecondesUpdate);

        // On envoie la date de dernière connexion et transfert les ressources en base toutes les SecondesUpdate
        StartCoroutine(UpdateDateDerniereCoEnBase());
        StartCoroutine(transfertRessourcesEnBaseScript());
    }
	
	// Update is called once per frame
	void Update () {
        if (Configuration.continueJoueur)
        {
            majdepuisBDD();
            PendantAbsence();
            StartCoroutine(RessourcesBdD.recupExamJouable());
            StartCoroutine(RessourcesBdD.recupDivertJouable());
            StartCoroutine(RessourcesBdD.recupPNJJouable());
            StartCoroutine(RessourcesBdD.RecupArtefactJouable());
            StartCoroutine(RessourcesBdD.RecupObjetMagasin());
            StartCoroutine(RessourcesBdD.RecupDeLaListeDesJoueurs());
        }

        if (Configuration.continueJoueur && continueMesAmis)
        {
            continueMesAmis = false;
            //Debug.Log("On rentre dans Joueur -> RecupMesAmis");
            StartCoroutine(RessourcesBdD.RecupMesAmis());
            //Debug.Log("On continue dans Joueur -> RecupMesAmis");
            StartCoroutine(RessourcesBdD.RecupActionsSociales());
            //Debug.Log("On finit dans Joueur -> RecupMesAmis");
        }

        if (Configuration.continueJoueur && continueMissionJouable)
        {
            // On récupère les missions jouables
            StartCoroutine(RessourcesBdD.recupMissionJouable());

            Configuration.continueJoueur = false;
            continueMissionJouable = false;
        }
    }

    public IEnumerator UpdateDateDerniereCoEnBase()
    {
        for (; ; )
        {
            // execute block of code here
            download = new WWW(url + "UpdateDateCo.php?id=" + IDJoueur);
            yield return new WaitForSeconds(SecondesUpdate);
        }
    }

    public IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    
    public IEnumerator transfertRessourcesEnBaseScript()
    {
        for (; ; )
        {
            if (continueTransfertRessources)
            {
                if (RessourcesBdD.listeDesRessources != null)
                {
                    for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
                    {
                        string urlRessource = url + "ChangeRessource.php?idRessource=" + (i + 1) + "&idJoueur=" + IDJoueur + "&value=" + MesRessources[i];
                        download = new WWW(urlRessource);
                    }
                }
                else
                {
                    // On détruit tout les objets
                    GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

                    for (int i = 0; i < GameObjects.Length; i++)
                    {
                        Destroy(GameObjects[i]);
                    }

                    // On "relance" le jeu
                    SceneManager.LoadScene("Index");
                    ChargerPopup.Charger("Erreur");
                    MessageErreur.messageErreur = "Une erreur est survenue. Veuillez relancer le jeu.";
                }
            }

            yield return new WaitForSeconds(SecondesUpdate);
        }
    }

    // Fais la mise à jour depuis la base de toutes les données importantes
    void majdepuisBDD()
    {
        StartCoroutine(majRessources(IDJoueur));
        StartCoroutine(majComp(IDJoueur));
        StartCoroutine(majObjet(IDJoueur));
        StartCoroutine(majDiplome(IDJoueur));
        StartCoroutine(majTrophee(IDJoueur));
        StartCoroutine(majArtefact(IDJoueur));
    }

    // Met à jour les objets du joueur
    IEnumerator majObjet(int idJoueur)
    {
        string urlComp = urlRess + "MAJObjet.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);
        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesObjets.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDItem"] == RessourcesBdD.listeDesObjets[j].ID)
                {
                    MesObjets[j] = (int)Node["msg"][i]["Quantity"];
                    //if (MesObjets[i] > 0)
                        //Debug.Log("Je possède "+ MesObjets[i] + " " + RessourcesBdD.listeDesObjets[j].Nom);
                }
            }
        }
    }

    // Met à jour les compétences du joueur
    IEnumerator majComp(int idJoueur)
    {
        string urlComp = urlRess + "MAJComp.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);

        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < MesValeursCompetences.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDSkill"] == RessourcesBdD.listeDesCompétences[j].ID)
                {
                    MesValeursCompetences[j] = (int)Node["msg"][i]["SkillLevel"];
                    //Debug.Log("Valeur compétence Joueur " + RessourcesBdD.listeDesCompétences[j].NomCompétence + " : " + MesValeursCompetences[j]);
                }
            }
        }
    }

    // Met à jour les ressources du joueur
    IEnumerator majRessources(int idJoueur)
    {
        string urlComp = urlRess + "MAJRessources.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);

        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesRessources.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDRessource"] == RessourcesBdD.listeDesRessources[j].ID)
                {
                    MesRessources[j] = (int)Node["msg"][i]["Value"];
                    //Debug.Log("Valeur ressource Joueur " + RessourcesBdD.listeDesRessources[j].NomRessource + " : " + MesRessources[j]);
                }
            }
        }

        continueTransfertRessources = true;
    }

    // Met à jour les diplomes du joueur
    IEnumerator majDiplome(int idJoueur)
    {
        string urlComp = urlRess + "MAJDiplome.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);
        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesDiplomes.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDDiplom"] == RessourcesBdD.listeDesDiplomes[j].IDDiplome)
                {
                    MesDiplomes[j] = true;
                    //Debug.Log("Diplome Joueur " + RessourcesBdD.listeDesDiplomes[j].NomDiplome + " : " + MesDiplomes[j]);
                }
            }
        }
    }

    // Met à jour les trophées du joueur
    IEnumerator majTrophee(int idJoueur)
    {
        string urlComp = urlRess + "MAJTrophee.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);
        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesTrophees.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDTrophy"] == RessourcesBdD.listeDesTrophees[j].IDTrophee)
                {
                    MesTrophees[j] = true;
                    //Debug.Log("Trophée Joueur " + RessourcesBdD.listeDesTrophees[j].Description + " : " + MesTrophees[j]);
                }
            }
        }
    }


    // Met à jour les artéfacts du joueur
    IEnumerator majArtefact(int idJoueur)
    {
        string urlComp = urlRess + "MAJArtefact.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);
        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesArtefacts.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDArtefact"] == RessourcesBdD.listeDesArtefacts[j].IDArtefact)
                {
                    MesArtefacts[j] = true;
                    //Debug.Log("Artéfact Joueur " + RessourcesBdD.listeDesArtefacts[j].NomArtefact + " : " + MesArtefacts[j]);
                }
            }
        }
    }

    public static IEnumerator transfertEnBase()
    {
        Debug.Log("Transfert des données du joueur en base");

        // Les Compétences
        for (int i = 0; i < MesValeursCompetences.Length; ++i)
        {
            // On insère (ou update) en base le niveau de compétence du joueur
            string urlCompetence = Configuration.url + "scripts/scriptsTransferts/TransfertCompetencesEnBase.php?id=" + IDJoueur
                + "&idComp=" + RessourcesBdD.listeDesCompétences[i].ID + "&valeur=" + MesValeursCompetences[i];

            WWW dlCompetence = new WWW(urlCompetence);
            yield return dlCompetence;
        }

        // Les Ressources
        for (int i = 0; i < MesRessources.Length; ++i)
        {
            // On insère (ou update) en base le niveau de compétence du joueur
            string urlRessource = Configuration.url + "scripts/scriptsTransferts/TransfertRessourcesEnBase.php?id=" + IDJoueur
                + "&idRess=" + RessourcesBdD.listeDesRessources[i].ID + "&valeur=" + MesRessources[i];

            WWW dlRessource = new WWW(urlRessource);
            yield return dlRessource;
        }

        // Date de dernière connexion
        string urlComp = Configuration.url + "scripts/UpdateDateCo.php?id=" + IDJoueur;
        WWW dl = new WWW(urlComp);
        yield return dl;

        // Les Objets
        for (int i = 0; i < MesObjets.Length; ++i)
        {
            // On insère (ou update) en base le niveau de compétence du joueur
            string urlObjet = Configuration.url + "scripts/scriptsTransferts/TransfertObjetsEnBase.php?id=" + IDJoueur
                + "&idObjet=" + RessourcesBdD.listeDesObjets[i].ID + "&valeur=" + MesObjets[i];

            WWW dlObjet = new WWW(urlObjet);
            yield return dlObjet;
        }
    }

    // Transfert des compétences en base
    public static IEnumerator transfertCompetencesEnBase()
    {
        for (int i = 0; i < MesValeursCompetences.Length; ++i)
        {
            // On insère (ou update) en base le niveau de compétence du joueur
            string urlCompetence = Configuration.url + "scripts/scriptsTransferts/TransfertCompetencesEnBase.php?id=" + IDJoueur
                + "&idComp=" + RessourcesBdD.listeDesCompétences[i].ID + "&valeur=" + MesValeursCompetences[i];

            WWW dlCompetence = new WWW(urlCompetence);
            yield return dlCompetence;
        }
    }

    // Transfert des ressources en base
    public static IEnumerator transfertRessourcesEnBase()
    {
        Debug.Log("On rentre dans transfertRessourcesEnBase");
        for (int i = 0; i < MesRessources.Length; ++i)
        {
            // On insère (ou update) en base le niveau de compétence du joueur
            string urlRessource = Configuration.url + "scripts/scriptsTransferts/TransfertRessourcesEnBase.php?id=" + IDJoueur
                + "&idRess=" + RessourcesBdD.listeDesRessources[i].ID + "&valeur=" + MesRessources[i];

            WWW dlRessource = new WWW(urlRessource);
            yield return dlRessource;
        }

        if (AchatEnMagasin.continueAchatMagasin)
        {
            AchatEnMagasin.continueReloadMagasin = true;
        }
    }

    // Transfert des objets en base
    public static IEnumerator transfertObjetsEnBase()
    {
        for (int i = 0; i < MesObjets.Length; ++i)
        {
            // On insère (ou update) en base le niveau de compétence du joueur
            string urlObjet = Configuration.url + "scripts/scriptsTransferts/TransfertObjetsEnBase.php?id=" + IDJoueur
                + "&idObjet=" + RessourcesBdD.listeDesObjets[i].ID + "&valeur=" + MesObjets[i];

            WWW dlObjet = new WWW(urlObjet);
            yield return dlObjet;
        }
    }
    
    // Transfert des actions sociales en base
    public static IEnumerator transfertActionsSocialesEnBase()
    {
        Debug.Log("Transfert Actions Sociales en Base");
        for (int i = 0; i < Joueur.MesAmis.Length; ++i)
        {
            // Si l'action Sociale (ITEM) du joueur avec cet ami est vrai (effectuée)
            if (Joueur.MesActionsSocialesObjet[i])
            {
                // On insère (ou update) en base le niveau de compétence du joueur
                string urlItem = Configuration.url + "scripts/scriptsTransferts/TransfertActionSocialeEnBase.php?id=" + IDJoueur
                    + "&idAmi=" + Joueur.MesAmis[i].SonID + "&type=ITEM";
                Debug.Log(urlItem);

                WWW dlItem = new WWW(urlItem);
                yield return dlItem;
            }

            // Si l'action Sociale (SKILL) du joueur avec cet ami est vrai (effectuée)
            if (Joueur.MesActionsSocialesSkill[i])
            {
                // On insère (ou update) en base le niveau de compétence du joueur
                string urlSkill = Configuration.url + "scripts/scriptsTransferts/TransfertActionSocialeEnBase.php?id=" + IDJoueur
                    + "&idAmi=" + Joueur.MesAmis[i].SonID + "&type=SKILL";
                Debug.Log(urlSkill);

                WWW dlSkill = new WWW(urlSkill);
                yield return dlSkill;
            }
        }
    }

    IEnumerator IncrementationRessources()
    {
        Debug.Log("Incrémentation Ressources");
        bool stop = false;
        while (!stop)
        {
            //long soustract = DateActuel.ToFileTimeUtc();
            yield return new WaitForSeconds(5);//(60*6);
            for (int i = 0; i < MesRessources.Length; ++i)
            {
                switch (RessourcesBdD.listeDesRessources[i].NomRessource)
                {
                    case "Social":
                    case "Divertissement":
                        if (MesRessources[i] < 100)
                        {
                            MesRessources[i] += 1;
                        }
                        else
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
            
        }   
    }

    void IncrementationRessourcesStatic()
    {
        if (MesRessources != null)
        {
            for (int i = 0; i < MesRessources.Length; ++i)
            {
                switch (RessourcesBdD.listeDesRessources[i].NomRessource)
                {
                    case "Social":
                    case "Divertissement":
                        if (MesRessources[i] < 100)
                        {
                            MesRessources[i] += 1;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            RelancerJeuErreur();
        }
    }

    void PendantAbsence()
    {
        TimeSpan tempsABS = System.DateTime.Now.Subtract(dateDerniereCo);
        for(int i = 0; i< RessourcesBdD.listeDesRessources.Length;++i)
        {
            //Debug.Log(RessourcesBdD.listeDesRessources[i].NomRessource + i);
            if(RessourcesBdD.listeDesRessources[i].NomRessource == "Social" || RessourcesBdD.listeDesRessources[i].NomRessource == "Divertissement")
            {
                MesRessources[i] += (int)(tempsABS.TotalSeconds / SecondesUpdate);
                if (MesRessources[i] > 100)
                {
                    MesRessources[i] = 100; 
                }
            }
        }
    }
    static public void VoirStat()
    {
        Debug.Log("Joueur numéro ->" +IDJoueur);
        for(int i = 0 ; i<RessourcesBdD.listeDesRessources.Length ; ++i)
        {
            Debug.Log("Ressources ->" + RessourcesBdD.listeDesRessources[i].NomRessource + " = " + MesRessources[i]);
        }
        for (int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            Debug.Log("Compétence ->" + RessourcesBdD.listeDesCompétences[i].ID + " = " + MesValeursCompetences[i]);
        }
    }

    static public void RelancerJeuErreur()
    {
        // On détruit tout les objets
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        // On "relance" le jeu
        SceneManager.LoadScene("Index");
        ChargerPopup.Charger("Erreur");
        MessageErreur.messageErreur = "Une erreur est survenue. Veuillez relancer le jeu.";
    }

    // Vérifie le status d'un tuto
    public static IEnumerator VerifierStatusTuto(int idTuto, GameObject EcranTuto)
    {
        string urlComp = Configuration.url + "scripts/VerifierStatusTuto.php";
        WWW download;
        string monJson;
        JSONNode monNode;

        urlComp += "?idTuto=" + idTuto + "&idPlayer=" + Joueur.IDJoueur;
        download = new WWW(urlComp);
        yield return download;

        if ((!string.IsNullOrEmpty(download.error)))
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {
            monJson = download.text;
            monNode = JSON.Parse(monJson);

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'un utilisateur est renvoyé)
            string result = monNode["result"].Value;

            if (result.ToLower() == "false")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
            }
            else
            {
                if (monNode["msg"] == "0")
                {
                    EcranTuto.SetActive(true);

                    // Change le status d'un tuto
                    urlComp = Configuration.url + "scripts/ChangeTutoStatus.php";
                    urlComp += "?idTuto=" + idTuto + "&idPlayer=" + Joueur.IDJoueur + "&status=1";
                    download = new WWW(urlComp);
                    yield return download;
                    //ChangeTutoStatus(idTuto);
                }
            }
        }
    }

    // Change le status d'un tuto
    static IEnumerator ChangeTutoStatus(int idTuto)
    {
        string urlComp = Configuration.url + "scripts/ChangeTutoStatus.php";
        urlComp += "?idTuto=" + idTuto + "&idPlayer=" + Joueur.IDJoueur + "&status=1";
        WWW download = new WWW(urlComp);
        yield return download;
    }
}
