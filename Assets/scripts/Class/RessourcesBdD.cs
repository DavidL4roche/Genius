using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class RessourcesBdD : MonoBehaviour
{
    public static Lieu[] listeDesLieux;
    public static Compétence[] listeDesCompétences;
    public static Objet[] listeDesObjets;
    public static Rang[] listeDesRangs;
    public static Ressource[] listeDesRessources;
    public static Diplome[] listeDesDiplomes;
    public static Trophee[] listeDesTrophees;
    public static Examen[] listeDesExamens;
    public static ExamenPrésent[] listeDesExamensPrésents;
    public static int[] listeDesExamensNonJouables;
    public static PNJ[] listeDesPNJ;
    public static Entreprise[] listeDesEntreprises;
    public static Mission[] listeDesMissions;
    public static MissionPrésente[] listeDesMissionsPrésentes;
    public static MissionDivertissement[] listeDesDivertissements;
    public static MissionDivertissementPrésente[] listeDesDivertissementsPrésents;
    public static Quartier[] listeDesQuartiers;
    public static Bonus[] listeDesBonus;
    public static Duree[] listeDesDurees;
    public static Gain[] listeDesGains;
    public static Perte[] listeDesPertes;
    public static Artefact[] listeDesArtefacts;
    public static Artefact[] listeDesArtefactsJouables;
    public static PNJPrésent[] listeDesPNJPrésents;
    public static ObjetPrésent[] LeMagasin;
    public static Topic[] listeDesTopicsAide;

    public static AutreJoueur[] listeDesJoueurs;
    public static bool stoprecupmission = false;

    static public RessourcesBdD instance;

    public static WWW download;
    public static string monJson;
    public static JSONNode monNode;
    public static string url = Configuration.url + "scripts/scriptsRessources/";

    // Valeurs pour continuer après effectué ces missions
    bool continueTotalMissions = false;
    public static bool continueLieu = false; // 1
    public static bool continueGain = false;
    public static bool continuePerte = false;
    public static bool continueBonus = false;
    public static bool continueDuree = false; // 5
    public static bool continueRang = false;
    public static bool continueQuartier = false;
    public static bool continueDivert = false;
    public static bool continueComp = false;
    public static bool continueRess = false; // 10
    public static bool continueDiplome = false;
    public static bool continueEntreprise = false;
    public static bool continueArtefact = false;
    public static bool continuePNJ = false;
    public static bool continueTopicsAide = false; // 15
    public static bool continueExam = false;
    public static bool continueMission = false;
    public static bool continueTrophee = false;
    public static bool continueObjet = false; // 19
    
    private static bool continueBonusLocal = true;
    private static bool continueQuartierLocal = true;
    private static bool continueDiplomeLocal = true;
    private static bool continueEntrepriseLocal = true;
    private static bool continueArtefactLocal = true;
    private static bool continueRangLocal = true;
    private static bool continueCompLocal = true;
    private static bool continueMissionLocal = true;
    private static bool continueRessourcesLocal = true;
    private static bool continueDureeLocal = true;

    public static bool continueJoueur = false;
    public static bool continueRessJoueur = false;
    public static bool continuePNJJoueur = false;
    public static bool continueDivertJoueur = false;
    public static bool continueObjetJoueur = false;
    public static bool continueMissionJoueur = false;
    public static bool continueMissionNotifQuartier = false;

    public static bool lancementRecup = false;

    // Permet de gérer les StartCoroutine dans les fonctions static
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    public static void Recup()
    {
        Debug.Log("Récupération des données du joueur");
        lancementRecup = true;

        try
        {
            instance.StartCoroutine(RecupLieu());
            instance.StartCoroutine(RecupGain());
            instance.StartCoroutine(RecupPerte());
            instance.StartCoroutine(RecupBonus());
            instance.StartCoroutine(RecupDuree());
            instance.StartCoroutine(RecupRang());
            instance.StartCoroutine(RecupQuartier());
            instance.StartCoroutine(RecupDivert());
            instance.StartCoroutine(RecupComp());
            instance.StartCoroutine(RecupRess());
            instance.StartCoroutine(RecupDiplome());
            instance.StartCoroutine(RecupTopicsAide());
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
            ReloadGame();
        }

        /*
        ChargerPopup.Charger("Succes");
        MessageErreur.messageErreur = "Récupération des données en base réussie.";*/
    }

    // On lance ces fonctions après que celles données de Recup soient finies
    public void Update()
    {
        try
        {
            // Récupération Examen, Trophee, Objet et Artéfact
            if (continueDiplome && continueComp && continueRang && continueDiplomeLocal && continueBonus && continueBonusLocal)
            {
                instance.StartCoroutine(RecupExam());
                instance.StartCoroutine(RecupTrophee());
                instance.StartCoroutine(RecupObjet());
                instance.StartCoroutine(RecupArtefact());
                continueBonusLocal = false;
                continueDiplomeLocal = false;
            }

            // Récupération Entreprise
            if (continueQuartier && continueQuartierLocal)
            {
                instance.StartCoroutine(RecupEntreprise());
                continueQuartierLocal = false;
            }

            // Récupération Mission
            if (continueEntreprise && continueRang && continueComp && continueEntrepriseLocal && continueRangLocal && continueCompLocal && continueDureeLocal && continueDuree)
            {
                instance.StartCoroutine(RecupMission());
                continueEntrepriseLocal = false;
                continueRangLocal = false;
                continueCompLocal = false;
                continueDureeLocal = false;
            }

            // Récupération PNJ
            if (continueArtefact && continueArtefactLocal && continueMission && continueMissionLocal)
            {
                instance.StartCoroutine(RecupPNJ());
                continueArtefactLocal = false;
                continueMissionLocal = false;
            }

            // TODO : Attendre que tout soit validé pour lancer ça
            if (continueLieu && continueGain && continuePerte && continueBonus && continueDuree && continueRang && continueQuartier && continueDivert &&
                continueComp && continueRess && continueDiplome && continueEntreprise && continueArtefact && continuePNJ && continueTopicsAide &&
                continueExam && continueMission && continueTrophee && continueObjet && continueRessourcesLocal)
            {
                SpawnerMission.continueTotalMissions = true;
                Configuration.continueJoueur = true;
                continueRessourcesLocal = false;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            ReloadGame();
        }
    }

    public static void ReloadGame()
    {
        // On détruit tout les objets
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        // On "relance" le jeu
        SceneManager.LoadScene("Index1");
        ChargerPopup.Charger("Erreur");
        MessageErreur.messageErreur = "Une erreur est survenue. Veuillez relancer le jeu.";
    }

    // Fonction de généralisation pour lecture script PHP
    public static bool VerifierStatusScript(WWW dl)
    {
        if ((!string.IsNullOrEmpty(dl.error)))
        {
            print("Error downloading: " + dl.error);
            return false;
        }
        else
        {
            monJson = dl.text;
            monNode = JSON.Parse(monJson);

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'une réponse est renvoyé)
            string result = monNode["result"].Value;

            // Si le résultat est faux, on affiche l'erreur et on relance le jeu : TODO !!!!!!!!!!!
            if (result.ToLower() == "false")
            {
                // On redirige vers la connexion
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
                return false;
            }
            else
            {
                // Le JSON est bon et valide
                return true;
            }
        }
    }

    // Fonction de généralisation pour lecture script PHP
    public static JSONNode RenvoiJSONScript(WWW dl)
    {
        monJson = dl.text;
        monNode = JSON.Parse(monJson);
        return monNode;
    }

    // Récupération des Lieux
    public static IEnumerator RecupLieu()
    {
        string urlComp = url + "RecupLieu.php";
        WWW dl = new WWW(urlComp);
        yield return dl;
        
        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesLieux = new Lieu[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesLieux[i] = new Lieu((int)Node["msg"][i]["IDPlace"], Node["msg"][i]["PlaceName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesLieux.Length; i++)
        {
            listeDesLieux[i].toString();
        }*/

        continueLieu = true;
    }

    // Récupération des Gains
    private static IEnumerator RecupGain()
    {
        string urlComp = url + "RecupGain.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesGains = new Gain[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesGains[i] = new Gain((int)Node["msg"][i]["IDGain"], Node["msg"][i]["GainName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesGains.Length; i++)
        {
            listeDesGains[i].toString();
        }*/

        continueGain = true;
    }

    // Récupération des Pertes
    private static IEnumerator RecupPerte()
    {
        string urlComp = url + "RecupPerte.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesPertes = new Perte[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesPertes[i] = new Perte((int)Node["msg"][i]["IDLoss"], Node["msg"][i]["LossName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesPertes.Length; i++)
        {
            listeDesPertes[i].toString();
        }*/

        continuePerte = true;
    }

    // Récupération des Bonus
    private static IEnumerator RecupBonus()
    {
        string urlComp = url + "RecupBonus.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesBonus = new Bonus[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesBonus[i] = new Bonus((int)Node["msg"][i]["IDBonus"], Node["msg"][i]["BonusName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesBonus.Length; i++)
        {
            listeDesBonus[i].toString();
        }*/

        continueBonus = true;
    }

    // Récupération des Durées
    private static IEnumerator RecupDuree()
    {
        string urlComp = url + "RecupDuree.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesDurees = new Duree[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesDurees[i] = new Duree((int)Node["msg"][i]["IDDuration"], Node["msg"][i]["DurationName"].Value, (int)Node["msg"][i]["DurationValue"]);
            }
        }
        /*
        for (int i = 0; i < listeDesDurees.Length; i++)
        {
            listeDesDurees[i].toString();
        }*/

        continueDuree = true;
    }

    // Récupération des Rangs
    private static IEnumerator RecupRang()
    {
        string urlComp = url + "RecupRang.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesRangs = new Rang[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesRangs[i] = new Rang((int)Node["msg"][i]["IDRank"], Node["msg"][i]["RankName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesRangs.Length; i++)
        {
            listeDesRangs[i].toString();
        }*/

        continueRang = true;
    }

    // Récupération des Quartiers
    private static IEnumerator RecupQuartier()
    {
        string urlComp = url + "RecupQuartier.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesQuartiers = new Quartier[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesQuartiers[i] = new Quartier((int)Node["msg"][i]["IDDistrict"], Node["msg"][i]["DistrictName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesQuartiers.Length; i++)
        {
            listeDesQuartiers[i].toString();
        }*/

        continueQuartier = true;
    }

    // Récupération des Divertissements
    private static IEnumerator RecupDivert()
    {
        string urlComp = url + "RecupDivert.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesDivertissements = new MissionDivertissement[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesDivertissements[i] = new MissionDivertissement((int)Node["msg"][i]["IDEntertainment"], Node["msg"][i]["EntertainmentName"].Value, (int)Node["msg"][i]["IDRank"]);
            }
        }
        /*
        for (int i = 0; i < listeDesDivertissements.Length; i++)
        {
            listeDesDivertissements[i].toString();
        }*/

        continueDivert = true;
        continueDivertJoueur = true;
    }

    // Récupération des Compétences
    private static IEnumerator RecupComp()
    {
        string urlComp = url + "RecupComp.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesCompétences = new Compétence[Node["msg"].Count];
            Joueur.MesValeursCompetences = new int[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesCompétences[i] = new Compétence((int)Node["msg"][i]["IDSkill"], Node["msg"][i]["SkillName"].Value, Node["msg"][i]["SkillDescription"].Value);
            }
        }
        /*
        for (int i = 0; i < 5; i++)
        {
            listeDesCompétences[i].toString();
        }*/
        continueComp = true;
    }

    // Récupération des Ressources
    private static IEnumerator RecupRess()
    {
        string urlComp = url + "RecupRess.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesRessources = new Ressource[Node["msg"].Count];
            Joueur.MesRessources = new int[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesRessources[i] = new Ressource((int)Node["msg"][i]["IDRessource"], Node["msg"][i]["RessourceName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesRessources.Length; i++)
        {
            listeDesRessources[i].toString();
        }*/

        continueRess = true;
        continueRessJoueur = true;
    }

    // Récupération des Objets
    private static IEnumerator RecupObjet()
    {
        string urlComp = url + "RecupObjet.php";

        /*
        // On attend que les Rangs ont été récupéré
        while(!continueObjet)
        {
            // On ne fait rien
        }
        */
        
        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesObjets = new Objet[Node["msg"].Count];
            Joueur.MesObjets = new int[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesObjets[i] = new Objet((int)Node["msg"][i]["IDItem"], Node["msg"][i]["ItemName"].Value, (int)Node["msg"][i]["IDRank"],
                                                (int)Node["msg"][i]["IDBonus"], (int)Node["msg"][i]["BonusGain"]);
            }
        }
        /*
        for (int i = 0; i < listeDesObjets.Length; i++)
        {
            listeDesObjets[i].toString();
        }*/

        continueObjet = true;
        continueObjetJoueur = true;
    }

    // Récupération des Diplômes
    private static IEnumerator RecupDiplome()
    {
        string urlComp = url + "RecupDiplome.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesDiplomes = new Diplome[Node["msg"].Count];
            Joueur.MesDiplomes = new bool[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesDiplomes[i] = new Diplome((int)Node["msg"][i]["IDDiplom"], Node["msg"][i]["DiplomName"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesDiplomes.Length; i++)
        {
            listeDesDiplomes[i].toString();
        }*/

        continueDiplome = true;
    }

    // Récupération des Trophées
    private static IEnumerator RecupTrophee()
    {
        string urlComp = url + "RecupTrophee.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesTrophees = new Trophee[Node["msg"].Count];
            Joueur.MesTrophees = new bool[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesTrophees[i] = new Trophee((int)Node["msg"][i]["IDTrophy"], (int)Node["msg"][i]["IDRank"], Node["msg"][i]["Description"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesTrophees.Length; i++)
        {
            listeDesTrophees[i].toString();
        }*/

        continueTrophee = true;
    }

    // Récupération des Examens
    private static IEnumerator RecupExam()
    {
        string urlComp = url + "RecupExam.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesExamens = new Examen[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesExamens[i] = new Examen((int)Node["msg"][i]["IDExam"], Node["msg"][i]["ExamName"].Value, (int)Node["msg"][i]["IDDiplom"],
                                                (int)Node["msg"][i]["IDSkillSlot1"], (int)Node["msg"][i]["IDSkillSlot2"], (int)Node["msg"][i]["IDSkillSlot3"],
                                                (int)Node["msg"][i]["IDSkillSlot4"], (int)Node["msg"][i]["IDSkillSlot5"], (int)Node["msg"][i]["IDRank"]);
            }
        }
        /*
        for (int i = 0; i < listeDesExamens.Length; i++)
        {
            listeDesExamens[i].toString();
        }*/

        continueExam = true;
    }

    // Récupération des Entreprises
    private static IEnumerator RecupEntreprise()
    {
        string urlComp = url + "RecupEntreprise.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesEntreprises = new Entreprise[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesEntreprises[i] = new Entreprise((int)Node["msg"][i]["IDCompany"], Node["msg"][i]["CompanyName"].Value, (int)Node["msg"][i]["Size"]);
            }
        }
        /*
        for (int i = 0; i < listeDesEntreprises.Length; i++)
        {
            listeDesEntreprises[i].toString();
        }*/
        
        for (int i = 0; i < listeDesEntreprises.Length; ++i)
        {
            // SES EMPLACEMENTS

            // On récupère les emplacements de l'entreprise (i)
            instance.StartCoroutine(RecupEmpEntreprise(i));

            // On récupère les spécialités de l'entreprise (i)
            instance.StartCoroutine(RecupSpeEntreprise(i));
        }

        continueEntreprise = true;
    }

    // Fonction d'ajout des emplacements d'une entreprise
    private static IEnumerator RecupEmpEntreprise(int i)
    {
        //Debug.Log("Entreprise " + i + "--------------------------------------------------");

        string urlEmp = url + "RecupEmpEntreprise.php?id=" + listeDesEntreprises[i].IDEntreprise;

        WWW dlEmp = new WWW(urlEmp);
        yield return dlEmp;

        if (VerifierStatusScript(dlEmp))
        {
            JSONNode Node = RenvoiJSONScript(dlEmp);
            if (Node["msg"].Count > 1)
            {
                listeDesEntreprises[i].SesEmplacements = new Quartier[Node["msg"].Count];
                for (int j = 0; j < Node["msg"].Count; ++j)
                {
                    listeDesEntreprises[i].SesEmplacements[j] = Quartier.trouverSonQuartier((int)Node["msg"][j]["IDDistrict"]);
                }
            }
            else if (Node["msg"].Count == 1)
            {
                listeDesEntreprises[i].SesEmplacements = new Quartier[1];
                listeDesEntreprises[i].SesEmplacements[0] = Quartier.trouverSonQuartier((int)Node["msg"][0]["IDDistrict"]);
            }
            else
            {
                listeDesEntreprises[i].SesEmplacements = new Quartier[1];
                listeDesEntreprises[i].SesEmplacements[0] = new Quartier(0, "0");
            }
        }
        /*
        Debug.Log("Emplacements des entreprises ---------------------------------------------------------");
        for (int j = 0; j < listeDesEntreprises[i].SesEmplacements.Length; j++)
        {
            listeDesEntreprises[i].SesEmplacements[j].toString();
        }*/
    }

    // Fonction d'ajout des spécialités d'une entreprise
    private static IEnumerator RecupSpeEntreprise(int i)
    {
        // Ses spécialités
        string urlSpe = url + "RecupSpeEntreprise.php?id=" + listeDesEntreprises[i].IDEntreprise;
        //Debug.Log("Spécialisations des entreprises Début 1 --------------------------------------------------------------");
        WWW dlspe = new WWW(urlSpe);
        yield return dlspe;

        //Debug.Log("Spécialisations des entreprises Début --------------------------------------------------------------");
        if (VerifierStatusScript(dlspe))
        {
            JSONNode Node = RenvoiJSONScript(dlspe);
            if (Node["msg"].Count > 1)
            {
                listeDesEntreprises[i].SesSpécialisations = new Quartier[Node["msg"].Count];
                for (int j = 0; j < Node["msg"].Count; ++j)
                {
                    listeDesEntreprises[i].SesSpécialisations[j] = Quartier.trouverSonQuartier((int)Node["msg"][j]["IDDistrict"]);
                }
            }
            else if (Node["msg"].Count == 1)
            {
                listeDesEntreprises[i].SesSpécialisations = new Quartier[Node["msg"].Count];
                for (int j = 0; j < Node["msg"].Count; ++j)
                {
                    listeDesEntreprises[i].SesSpécialisations[0] = Quartier.trouverSonQuartier((int)Node["msg"][j]["IDDistrict"]);
                }
            }
            else
            {
                listeDesEntreprises[i].SesSpécialisations = new Quartier[1];
                listeDesEntreprises[i].SesSpécialisations[0] = new Quartier(0, "0");
            }
        }
        /*
        Debug.Log("Spécialisations des entreprises --------------------------------------------------------------");
        for (int j = 0; j < listeDesEntreprises[i].SesSpécialisations.Length; j++)
        {
            listeDesEntreprises[i].SesSpécialisations[j].toString();
        }
        Debug.Log("Fin spécialisations des entreprises --------------------------------------------------------------");*/
    }

    // Récupération des Missions
    private static IEnumerator RecupMission()
    {
        string urlComp = url + "RecupMission.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesMissions = new Mission[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesMissions[i] = new Mission((int)Node["msg"][i]["IDMission"], Node["msg"][i]["MissionName"].Value, (int)Node["msg"][i]["IDRank"],
                                                (int)Node["msg"][i]["IDSkill1"], (int)Node["msg"][i]["IDSkill2"], (int)Node["msg"][i]["IDSkill3"],
                                                (int)Node["msg"][i]["IDSkill4"], (int)Node["msg"][i]["IDSkill5"], Node["msg"][i]["AssociatedJob"].Value);
            }
        }
        /*
        for (int i = 0; i < 5; i++)
        {
            listeDesMissions[i].toString();
        }*/

        /*
        for (int i = 0; i < listeDesMissions.Length;)
        {
            while (lien.Read())
            {
                int[] tableauidskill = new int[5];
                for (int j = 1; j < 6; ++j)
                {
                    string nomdansbdd = "IDSkill" + j;
                    //Debug.Log(lien[nomdansbdd].ToString());
                    if (lien[nomdansbdd].ToString() != "" && lien[nomdansbdd].ToString() != null && Int32.Parse(lien[nomdansbdd].ToString()) != 0)
                    {
                        int idskill = Int32.Parse(lien[nomdansbdd].ToString());
                        tableauidskill[j - 1] = idskill;
                    }
                    else
                    {
                        tableauidskill[j - 1] = 0;
                    }
                }
                listeDesMissions[i] = new Mission((int)lien["IDMission"], lien["MissionName"].ToString(), (int)lien["IDRank"], tableauidskill[0], tableauidskill[1], tableauidskill[2], tableauidskill[3], tableauidskill[4], lien["AssociatedJob"].ToString());
                ++i;
            }
        }
        lien.Close();
        */

        continueMission = true;
        continueMissionNotifQuartier = true;
        continueMissionJoueur = true;
        continueMissionLocal = true;
    }

    // Récupération des Artéfacts
    private static IEnumerator RecupArtefact()
    {
        string urlComp = url + "RecupArtefact.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesArtefacts = new Artefact[Node["msg"].Count];
            Joueur.MesArtefacts = new bool[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesArtefacts[i] = new Artefact((int)Node["msg"][i]["IDArtefact"], Node["msg"][i]["ArtefactName"].Value, (int)Node["msg"][i]["IDBonus"]);
            }
        }
        /*
        for (int i = 0; i < listeDesArtefacts.Length; i++)
        {
            listeDesArtefacts[i].toString();
        }*/

        continueArtefact = true;
    }

    // Récupération des PNJ
    private static IEnumerator RecupPNJ()
    {
        string urlComp = url + "RecupPNJ.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesPNJ = new PNJ[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesPNJ[i] = new PNJ((int)Node["msg"][i]["IDNPCharacter"], Node["msg"][i]["NPCName"].Value, (int)Node["msg"][i]["IDArtefact"]);
            }
        }
        /*
        for (int i = 0; i < listeDesPNJ.Length; i++)
        {
            Debug.Log("PNJ : " + listeDesPNJ[i].NomPNJ);
        }*/

        // On associe le quartier au PNJ
        for (int i = 0; i < listeDesPNJ.Length; ++i)
        {
            string urlPNJ = url + "RecupQuartierPNJ.php?id=" + listeDesPNJ[i].IDPNJ;

            WWW downPNJ = new WWW(urlPNJ);
            yield return downPNJ;

            if (VerifierStatusScript(downPNJ))
            {
                JSONNode NodePNJ = RenvoiJSONScript(downPNJ);
                listeDesPNJ[i].associerSonQuartier((int)NodePNJ["msg"][0]["IDDistrict"]);
            }
        }
        /*
        for (int i = 0; i < listeDesPNJ.Length; i++)
        {
            Debug.Log("Quartier PNJ " + listeDesPNJ[i].SonQuartier.NomQuartier);
        }*/

        continuePNJ = true;
        continuePNJJoueur = true;
    }

    // Récuparation des topics de l'Aide
    private static IEnumerator RecupTopicsAide()
    {
        string urlComp = url + "RecupTopicsAide.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesTopicsAide = new Topic[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesTopicsAide[i] = new Topic((int)Node["msg"][i]["idTopic"], Node["msg"][i]["title"].Value, Node["msg"][i]["body"].Value);
            }
        }
        /*
        for (int i = 0; i < listeDesTopicsAide.Length; i++)
        {
            listeDesTopicsAide[i].toString();
        }*/

        continueTopicsAide = true;
    }

    // Récupération des Objets du Magasin
    public static IEnumerator RecupObjetMagasin()
    {
        string urlComp = url + "RecupObjetMagasin.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            LeMagasin = new ObjetPrésent[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                LeMagasin[i] = new ObjetPrésent((int)Node["msg"][i]["IDItem"]);
            }
        }
        /*
        for (int i=0; i< LeMagasin.Length; i++)
        {
            Debug.Log(LeMagasin[i].SonObjet.Nom);
        }*/

        if (AchatEnMagasin.continueReloadMagasin)
        {
            AchatEnMagasin.continueReloadMag = true;
            AchatEnMagasin.continueReloadMagasin = false;
        }
    }

    public static IEnumerator RecupArtefactJouable()
    {
        string urlComp = url + "RecupArtefactJouable.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesArtefactsJouables = new Artefact[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesArtefactsJouables[i] = new Artefact((int)Node["msg"][i]["IDArtefact"], Node["msg"][i]["ArtefactName"].Value, (int)Node["msg"][i]["IDBonus"]);
            }
        }
        /*
        Debug.Log("LISTE DES ARTEFACTS --------------------------------------");
        for (int i=0; i< listeDesArtefactsJouables.Length; i++)
        {
            listeDesArtefactsJouables[i].toString();
        }*/
    }
    
    // Récupération des missions jouables par le joueur
    public static IEnumerator recupMissionJouable()
    {
        int tempsattente = 610 - ((Joueur.DateActuelMinute % 10) * 60 + Joueur.DateActuelSeconde);
        string urlComp = url + "RecupPresentMissions.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesMissionsPrésentes = new MissionPrésente[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesMissionsPrésentes[i] = new MissionPrésente((int)Node["msg"][i]["IDDistrict"], (int)Node["msg"][i]["IDMission"], (int)Node["msg"][i]["IDCompany"]);
            }
        }
        /*
        for (int i = 0; i < 5; i++)
        {
            listeDesMissionsPrésentes[i].toString();
        }*/

        for (int i = 0; i < listeDesMissionsPrésentes.Length;++i)
        {
            //Debug.Log(listeDesMissionsPrésentes[i].SaMission.MissionEntreprise.SesSpécialisations[0].IDQuartier);
            listeDesMissionsPrésentes[i].SaMission = testMissionSpecialise(listeDesMissionsPrésentes[i].SaMission, listeDesMissionsPrésentes[i].SonQuartier);
        }

        instance.continueTotalMissions = true;
        ChargerLieu chargement = new ChargerLieu();
        chargement.Recharger();

        Joueur.continueDaedelus = true;
    }

    static public void DestroyListeMission()
    {
        listeDesMissionsPrésentes = new MissionPrésente[0];
        listeDesPNJPrésents = new PNJPrésent[0];
    }

    static public void DestroyListeDivertissement()
    {
        listeDesDivertissementsPrésents = new MissionDivertissementPrésente[0];
    }

    private static Mission testMissionSpecialise(Mission mission, Quartier quartier)
    {
        bool test = false;
        //Debug.Log("Test Mission Specialise");
        //Debug.Log("Taille Spe " + mission.MissionEntreprise.SesSpécialisations.Length);
        foreach (Quartier q in mission.MissionEntreprise.SesSpécialisations)
        {
            if (quartier.IDQuartier == q.IDQuartier)
            {
                test = true;
            }
        }
        if (test)
        {
            mission.RangMission = Rang.trouverSonRang(mission.RangMission.IDRang + 4);
            ++mission.MissionEntreprise.TailleEntreprise;
        }
        else
        {

        }
        //mission.voirMission();
        return mission;
    }

    public static IEnumerator recupExamJouable()
    {
        string urlComp = url + "RecupExamJouable.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesExamensPrésents = new ExamenPrésent[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesExamensPrésents[i] = new ExamenPrésent((int)Node["msg"][i]["IDPlace"], (int)Node["msg"][i]["IDExam"]);
            }
        }
        /*
        for (int i = 0; i < listeDesExamensPrésents.Length; i++)
        {
            listeDesExamensPrésents[i].toString();
        }*/
    }

    public static IEnumerator recupExamensNonJouables()
    {
        // On réinitialise notre tableau d'examens
        listeDesExamensNonJouables = new int[0];

        string urlComp = url + "RecupExamensNonJouables.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesExamensNonJouables = new int[Node["msg"].Count];
            for (int j = 0; j < Node["msg"].Count; ++j)
            {
                listeDesExamensNonJouables[j] = (int)Node["msg"][j]["IDExam"];
            }
        }

        FabriqueListeExamens.continueListeExamens = true;
    }

    public static IEnumerator recupDivertJouable()
    {
        string urlComp = url + "RecupDivertJouable.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesDivertissementsPrésents = new MissionDivertissementPrésente[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesDivertissementsPrésents[i] = new MissionDivertissementPrésente((int)Node["msg"][i]["IDDistrict"], (int)Node["msg"][i]["IDEntertainment"]);
            }
        }
        /*
        for (int i = 0; i < listeDesDivertissementsPrésents.Length; i++)
        {
            listeDesDivertissementsPrésents[i].toString();
        }*/
    }
    public static IEnumerator recupPNJJouable()
    {
        string urlComp = url + "RecupPNJJouable.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesPNJPrésents = new PNJPrésent[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesPNJPrésents[i] = new PNJPrésent((int)Node["msg"][i]["IDNPCharacter"], (int)Node["msg"][i]["IDMission"]);
            }
        }
        /*
        for (int i=0; i<listeDesPNJPrésents.Length; i++)
        {
            listeDesPNJPrésents[i].toString();
        }*/
    }

    public static bool testSiPNJJouable()
    {
        //Debug.Log("Liste des diplomes : " + listeDesDiplomes);
        bool test = false;

        for (int i = 0; i < RessourcesBdD.listeDesDiplomes.Length; ++i)
        {
            if (RessourcesBdD.listeDesDiplomes[i].NomDiplome != null)
            {
                if (Joueur.MesDiplomes[i] == true && RessourcesBdD.listeDesDiplomes[i].SonRang.NomRang == "B")
                {
                    test = true;
                    break;
                }
            }
            else
            {
                //Debug.Log("Aucun diplome trouvé");
            }
        }
        return test;
    }

    public static Objet randomObjetAvecRang(string NomRang)
    {
        Objet objetrandom = null;
        int compteur = 0;
        foreach (Objet objet in listeDesObjets)
        {
            if (objet.RangObjet.NomRang == NomRang)
            {
                ++compteur;
            }
        }
        Objet[] tableau = new Objet[compteur];
        int inc = 0;
        foreach (Objet objet in listeDesObjets)
        {
            if (objet.RangObjet.NomRang == NomRang)
            {
                tableau[inc] = objet;
                ++inc;
            }
        }
        int randomObjet = UnityEngine.Random.Range(0,tableau.Length);
        for(int i = 0; i<tableau.Length; ++i)
        {
            if (i == randomObjet)
            {
                objetrandom = tableau[i];
                break;
            }
        }
        return objetrandom;
    }

    // Reset les amis du joueur après suppression d'un ami
    public static void ResetAmis()
    {
        instance.StartCoroutine(RecupDeLaListeDesJoueurs());
        instance.StartCoroutine(RecupMesAmis());
    }

    // Récupère les amis du joueur
    public static IEnumerator RecupMesAmis()
    {
        //Debug.Log("On rentre dans RessourcesBdD -> RecupMesAmis");
        Joueur.MesAmis = new AutreJoueur[0];
        string urlAmis = url + "RecupMesAmis.php?id=" + Joueur.IDJoueur;

        WWW dlAmis = new WWW(urlAmis);
        yield return dlAmis;

        if (VerifierStatusScript(dlAmis))
        {
            JSONNode NodeAmis = RenvoiJSONScript(dlAmis);
            Joueur.MesAmis = new AutreJoueur[NodeAmis["msg"].Count];
            for (int i = 0; i < NodeAmis["msg"].Count; ++i)
            {
                Joueur.MesAmis[i] = new AutreJoueur((int)NodeAmis["msg"][i]["IDPCharacter"], NodeAmis["msg"][i]["PCName"].Value);
            }
        }

        foreach (AutreJoueur ami in Joueur.MesAmis)
        {
            instance.StartCoroutine(ami.majObjetAmi(ami.SonID));
            instance.StartCoroutine(ami.majComp(ami.SonID));
            instance.StartCoroutine(ami.majDiplome(ami.SonID));
        }

        /*
        Debug.Log("LISTE DES AMIS --------------------------------------");
        for (int i = 0; i < Joueur.MesAmis.Length; i++)
        {
            Debug.Log(i + " : " + Joueur.MesAmis[i].SonNom);
        }
        Debug.Log("Fin RecupAmis");
        */

        Joueur.continueRecupActionsSociales = true;
    }

    // Récupère la liste des Joueurs (sans le joueur)
    public static IEnumerator RecupDeLaListeDesJoueurs()
    {
        listeDesJoueurs = new AutreJoueur[0];

        string urlJoueurs = url + "RecupDeLaListeDesJoueurs.php?id=" + Joueur.IDJoueur;

        WWW dlJoueurs = new WWW(urlJoueurs);
        yield return dlJoueurs;

        if (VerifierStatusScript(dlJoueurs))
        {
            JSONNode NodeJoueurs = RenvoiJSONScript(dlJoueurs);
            listeDesJoueurs = new AutreJoueur[NodeJoueurs["msg"].Count];
            for (int i = 0; i < NodeJoueurs["msg"].Count; ++i)
            {
                listeDesJoueurs[i] = new AutreJoueur((int)NodeJoueurs["msg"][i]["IDPCharacter"], NodeJoueurs["msg"][i]["PCName"].Value);
            }
        }

        /*
        Debug.Log("LISTE DES AUTRES JOUEURS --------------------------------------");
        for (int i = 0; i < listeDesJoueurs.Length; i++)
        {
            Debug.Log(i + " : " + listeDesJoueurs[i].SonNom);
        }
        Debug.Log("Fin RecupListeJoueurs");
        */

        Joueur.continueMesAmis = true;
    }

    public static IEnumerator RecupActionsSociales()
    {
        while (!Joueur.continueRecupActionsSociales)
        {
            yield return new WaitForSeconds(1);
        }

        // On initialise les tableaux
        Joueur.MesActionsSocialesObjet = new bool[Joueur.MesAmis.Length];
        Joueur.MesActionsSocialesSkill = new bool[Joueur.MesAmis.Length];

        string urlComp = url + "RecupActionsSocialesObjet.php?id=" + Joueur.IDJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                for (int j = 0; j < Joueur.MesAmis.Length; ++j)
                {
                    if ((int)Node["msg"][i]["IDPFriend"] == Joueur.MesAmis[j].SonID)
                    {
                        Debug.Log("Ami ayant action sociale Objet : " + Joueur.MesAmis[j].SonNom);
                        Joueur.MesActionsSocialesObjet[j] = true;
                    }
                    else
                    {
                        Joueur.MesActionsSocialesObjet[j] = false;
                    }
                }
            }
        }

        string urlComp2 = url + "RecupActionsSocialesComp.php?id=" + Joueur.IDJoueur;

        WWW dl2 = new WWW(urlComp2);
        yield return dl2;

        if (VerifierStatusScript(dl2))
        {
            JSONNode Node2 = RenvoiJSONScript(dl2);
            for (int i = 0; i < Node2["msg"].Count; ++i)
            {
                for (int j = 0; j < Joueur.MesAmis.Length; ++j)
                {
                    if ((int)Node2["msg"][i]["IDPFriend"] == Joueur.MesAmis[j].SonID)
                    {
                        Debug.Log("Ami ayant action sociale Compétence : " + Joueur.MesAmis[j].SonNom);
                        Joueur.MesActionsSocialesSkill[j] = true;
                    }
                    else
                    {
                        Joueur.MesActionsSocialesSkill[j] = false;
                    }
                }
            }
        }

        Joueur.continueMissionJouable = true;
    }

    // Change le booléen continueTotalMissions
    public static void setTotalMissions(bool result)
    {
        instance.continueTotalMissions = result;
    }

    public static bool getTotalMissions()
    {
        return instance.continueTotalMissions;
    }
}
