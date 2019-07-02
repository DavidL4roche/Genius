using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnerMission : MonoBehaviour {
    static int IDQuartier;

    public GameObject Mission;
    public GameObject MissionDivert;
    public GameObject MissionPNJ;
    public RawImage FondMissionD;
    public RawImage FondMission;
    public RawImage FondPNJ;
    public RawImage OnMission;
    public RawImage OnPNJ;
    public Text RangMission;
    GameObject instance;
    static Vector3 spawnerposition;
    public static bool stop;
    static int nbMission = 0;
    public static Mission[] LesMissions;
    public static MissionDivertissement SonDivertissement;
    public static PNJPrésent SonPNJ;
    int ecrangauche = -140;
    int ecrandroite = 140;
    int ecranhaut = 230;
    int ecranbas = -260;
    static float[] tabPositionX;
    static float[] tabPositionY;
    bool superpose;

    public static bool continueTotalMissions = false;
    public static bool continueStart = false;

    public void Start()
    {
        tabPositionY = new float[6];
        tabPositionX = new float[6];

        // On récupère l'id du quartier
        IDQuartier = VerifQuartier.IDQuartier;

        if (continueStart)
        {
            StartCoroutine(Joueur.transfertEnBase());
            GetMissions();
        }
    }

    public void Update()
    {
        if (continueTotalMissions)
        {
            GetMissions();
        }
        continueTotalMissions = false;
        continueStart = true;
    }

    public void GetMissions() {
        // On crée les missions, divertissements et PNJ du quartier
        totalDeMissions();

        // On instancie les missions dans le quartier
        for (int i = 0; i < LesMissions.Length; ++i)
        {
            spawnerposition = genererPositionNonUtilisee();
            RangMission.text = LesMissions[i].RangMission.NomRang;
            FondMission.texture = LesMissions[i].RangMission.texture;

            Mission mission = LesMissions[i];

            // On regarde si la mission est jouable pour le joueur
            for (int j = 0; j < mission.CompétencesRequises.Length; ++j)
            {
                if (verificationCompAvecJoueur(mission.CompétencesRequises[j].ID, mission.CompétencesRequises[j].Valeur))
                {
                    OnMission.enabled = true;
                }
                else
                {
                    OnMission.enabled = false;
                    break;
                }
            }

            //FondMission.color = new Color32(255, 255, 255, 127); // TEST
            instance = Instantiate(Mission, spawnerposition, Mission.transform.rotation);
            instance.transform.parent = GameObject.Find("Decor").transform;
            instance.transform.name = "Mission " + (i);
            //ListeMissions.listeDeMissions[IDQuartier].missions[i].voirRessources();
        }

        // On instancie un divertissement dans le quartier
        if (SonDivertissement.IDMissionD != 0)
        {
            spawnerposition = genererPositionNonUtilisee();
            FondMissionD.texture = SonDivertissement.SonRang.texture;

            instance = Instantiate(MissionDivert, spawnerposition, MissionDivert.transform.rotation);
            instance.transform.parent = GameObject.Find("Decor").transform;
            instance.transform.name = "Divertissement";
        }

        // On instancie un PNJ dans le quartier
        if (SonPNJ.SonPNJ.IDPNJ != 0)
        {
            spawnerposition = genererPositionNonUtilisee();

            RawImage iconePNJ = MissionPNJ.gameObject.GetComponentInChildren<RawImage>();
            iconePNJ.texture = Resources.Load<Texture>("icones/PNJ" + SonPNJ.SonPNJ.IDPNJ);

            // On regarde si la mission est jouable pour le joueur
            for (int j = 0; j < SonPNJ.SaMission.CompétencesRequises.Length; ++j)
            {
                if (verificationCompAvecJoueur(SonPNJ.SaMission.CompétencesRequises[j].ID, SonPNJ.SaMission.CompétencesRequises[j].Valeur))
                {
                    OnPNJ.enabled = true;
                }
                else
                {
                    OnPNJ.enabled = false;
                    break;
                }
            }

            instance = Instantiate(MissionPNJ, spawnerposition, MissionPNJ.transform.rotation);
            instance.transform.parent = GameObject.Find("Decor").transform;

            int k = 0;
            for (k = 0; k < RessourcesBdD.listeDesMissions.Length; k++)
            {
                if (SonPNJ.SaMission.IDMission == RessourcesBdD.listeDesMissions[k].IDMission)
                {
                    break;
                }
            }
            instance.transform.name = "PNJ" + k;
        }
        // Debug.Log("Je suis après la boucle des missions...");
        //MissionsDuQuartier.RandomMission();
        // Debug.Log("Je suis après le random des missions...");
        //Missions.VoirMissions();
    }

    public static void DestroySpawn()
    {
        LesMissions = new Mission[0];
        tabPositionX = new float[0];
        tabPositionY = new float[0];
    }

    public static void DestroyDivert()
    {
        int i = tabPositionX.Length;
        tabPositionX[i] = 0;
        tabPositionY[i] = 0;
    }

    // Crée le nombre de missions, divertissements et PNJ correspondants
    public void totalDeMissions()
    {
        SonPNJ = new PNJPrésent(0);
        SonDivertissement = new MissionDivertissement(0, "", 1);
        nbMission = 0;

        // On calcule le nombre de missions du quartier
        if (RessourcesBdD.listeDesMissionsPrésentes != null)
        {
            for (int i = 0; i < RessourcesBdD.listeDesMissionsPrésentes.Length; ++i)
            {
                if (IDQuartier == RessourcesBdD.listeDesMissionsPrésentes[i].SonQuartier.IDQuartier)
                {
                    ++nbMission;
                }
            }
        }
        /*
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
            MessageErreur.messageErreur = "Une erreur est survenue. Veuillez réessayer plus tard.";
        }
        */

        // On crée le nombre de missions correspondantes
        LesMissions = new Mission[nbMission];
        int y = 0;
        for (int i = 0; i < RessourcesBdD.listeDesMissionsPrésentes.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesMissionsPrésentes[i].SonQuartier.IDQuartier)
            {
                LesMissions[y] = new Mission(RessourcesBdD.listeDesMissionsPrésentes[i].SaMission);
                ++y;
            }
        }

        // On crée le nombre de divertissements correspondants
        for (int i = 0; i < RessourcesBdD.listeDesDivertissementsPrésents.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesDivertissementsPrésents[i].SonQuartier.IDQuartier)
            {
                SonDivertissement = RessourcesBdD.listeDesDivertissementsPrésents[i].SonDivertissement;
            }
        }

        // On crée le nombre de PNJ correspondants
        for (int i = 0; i < RessourcesBdD.listeDesPNJPrésents.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesPNJPrésents[i].SonPNJ.SonQuartier.IDQuartier)
            {
                SonPNJ = RessourcesBdD.listeDesPNJPrésents[i];
            }
        }
    }

    // Génerer une position non utilisée dans le quartier
    public Vector3 genererPositionNonUtilisee()
    {
        //Debug.Log("Je suis dans la boucle des missions...");
        bool test = false;
        while (!test)
        {
            superpose = false;
            int ecart = 85;
            spawnerposition = new Vector3(Random.Range(ecrangauche, ecrandroite), Random.Range(ecranbas, ecranhaut), 0);
            for (int j = 0; j < tabPositionX.Length; ++j)
            {
                if (spawnerposition.x <= tabPositionX[j] + ecart && spawnerposition.x >= tabPositionX[j] - ecart && spawnerposition.y <= tabPositionY[j] + ecart && spawnerposition.y >= tabPositionY[j] - ecart)
                {
                    superpose = true;
                    break;
                }
            }
            if (!superpose)
            {
                test = true;
            }
        }
        for (int j = 0; j < tabPositionX.Length; ++j)
        {
            if (0 == tabPositionX[j] && 0 == tabPositionY[j])
            {
                tabPositionX[j] = spawnerposition.x;
                tabPositionY[j] = spawnerposition.y;
                break;
            }
        }
        return spawnerposition;
    }

    // Vérifie la compétence demandée avec celle du Joueur (valeur)
    public bool verificationCompAvecJoueur(int id, int valeur)
    {
        int k = 0;
        for (; k < RessourcesBdD.listeDesCompétences.Length; ++k)
        {
            if (id == RessourcesBdD.listeDesCompétences[k].ID)
            {
                break;
            }
        }

        if (Joueur.MesValeursCompetences[k] >= valeur)
        {
            FondMission.color = new Color32(255, 255, 255, 255); // TEST
            return true;
        }
        else
        {
            return false;
        }
    }
}
