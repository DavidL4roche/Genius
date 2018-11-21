using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerMission : MonoBehaviour {
    static int IDQuartier;
    public GameObject Mission;
    public GameObject MissionDivert;
    public GameObject MissionPNJ;
    public RawImage FondMissionD;
    public RawImage FondMission;
    public Text RangMission;
    GameObject instance;
    static Vector3 spawnerposition;
    public static bool stop;
    static int nbMission = 0;
    public static Mission[] LesMissions;
    public static MissionDivertissement SonDivertissement;
    public static PNJPrésent SonPNJ;
    int ecrangauche = 350;
    int ecrandroite = 590;
    int ecranhaut = 280;
    int ecranbas = 20;
    static float[] tabPositionX;
    static float[] tabPositionY;
    bool superpose;

    public void Start()
    {
        tabPositionY = new float[6];
        tabPositionX = new float[6];
        IDQuartier = VerifQuartier.IDQuartier;
        totalDeMissions();
        for (int i = 0; i < LesMissions.Length; ++i)
        {
            spawnerposition = genererPositionNonUtilisee();
            RangMission.text = LesMissions[i].RangMission.NomRang;
            FondMission.texture = LesMissions[i].RangMission.texture;
            instance = Instantiate(Mission, spawnerposition, Mission.transform.rotation);
            instance.transform.parent = GameObject.Find("Decor").transform;
            instance.transform.name = "Mission " + (i);
            //ListeMissions.listeDeMissions[IDQuartier].missions[i].voirRessources();
        }
        if(SonDivertissement.IDMissionD != 0)
        {
            spawnerposition = genererPositionNonUtilisee();
            FondMissionD.texture = SonDivertissement.SonRang.texture;
            instance = Instantiate(MissionDivert, spawnerposition, MissionDivert.transform.rotation);
            instance.transform.parent = GameObject.Find("Decor").transform;
            instance.transform.name = "Divertissement";
        }
        if (SonPNJ.SonPNJ.IDPNJ !=0 )
        {
            spawnerposition = genererPositionNonUtilisee();
            instance = Instantiate(MissionPNJ, spawnerposition, MissionPNJ.transform.rotation);
            instance.transform.parent = GameObject.Find("Decor").transform;
            instance.transform.name = "PNJ";
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
    public void totalDeMissions()
    {
        SonPNJ = new PNJPrésent(0);
        SonDivertissement = new MissionDivertissement(0, "", 1);
        nbMission = 0;
        for (int i = 0; i < RessourcesBdD.listeDesMissionsPrésentes.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesMissionsPrésentes[i].SonQuartier.IDQuartier)
            {
                ++nbMission;
            }
        }
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
        for (int i = 0; i < RessourcesBdD.listeDesDivertissementsPrésents.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesDivertissementsPrésents[i].SonQuartier.IDQuartier)
            {
                SonDivertissement = RessourcesBdD.listeDesDivertissementsPrésents[i].SonDivertissement;
            }
        }
        for (int i = 0; i < RessourcesBdD.listeDesPNJPrésents.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesPNJPrésents[i].SonPNJ.SonQuartier.IDQuartier)
            {
                SonPNJ = RessourcesBdD.listeDesPNJPrésents[i];
            }
        }
    }

    public Vector3 genererPositionNonUtilisee()
    {
        //Debug.Log("Je suis dans la boucle des missions...");
        bool test = false;
        while (!test)
        {
            superpose = false;
            spawnerposition = new Vector3(Random.Range(ecrangauche, ecrandroite), Random.Range(ecranbas, ecranhaut), 0);
            for (int j = 0; j < tabPositionX.Length; ++j)
            {
                if (spawnerposition.x <= tabPositionX[j] + 40 && spawnerposition.x >= tabPositionX[j] - 40 && spawnerposition.y <= tabPositionY[j] + 40 && spawnerposition.y >= tabPositionY[j] - 40)
                {
                    superpose = true;
                    break;
                }
                else
                {

                }
            }
            if (!superpose)
            {
                test = true;
            }
            else
            {

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
            else
            {

            }
        }
        return spawnerposition;
    }
}
