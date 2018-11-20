using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerExam : MonoBehaviour {
    static int IDLieu;
    public GameObject Exam;
    public RawImage Fond;
    public Text Rang;
    GameObject instance;
    static Vector3 spawnerposition;
    public static bool stop;
    static int nbExam = 0;
    public static Examen[] LesExam;
    int ecrangauche = 350;
    int ecrandroite = 590;
    int ecranhaut = 280;
    int ecranbas = 20;
    static float[] tabPositionX;
    static float[] tabPositionY;
    bool superpose;

    public void Start()
    {
        tabPositionY = new float[5];
        tabPositionX = new float[5];
        IDLieu = VerificationLieu.IDLieu;
        totalDeExamens();
        int y = 0;
        for (int i = 0; i < RessourcesBdD.listeDesExamensPrésents.Length; ++i)
        {
            if (IDLieu == RessourcesBdD.listeDesExamensPrésents[i].SonLieu.IDLieu)
            {
                //Debug.Log("Je suis dans la boucle des missions...");
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
                    tabPositionX[y] = spawnerposition.x;
                    tabPositionY[y] = spawnerposition.y;
                }
                else
                {
                    --i;
                    continue;
                }
                Examen ex = RessourcesBdD.listeDesExamensPrésents[i].SonExamen;
                LesExam[y] = ex;
                ++y;
                Rang.text = ex.RangExamen.NomRang;
                Fond.texture = ex.RangExamen.texture;
                instance = Instantiate(Exam, spawnerposition, Exam.transform.rotation);
                instance.transform.parent = GameObject.Find("Decor").transform;
                instance.transform.name = "Exam " + (y);
            }
        }
    }
    private void OnDestroy()
    {
        DestroySpawn();
    }
    public static void DestroySpawn()
    {
        for (int i = 0; i < nbExam; ++i)
        {
            Destroy(GameObject.Find("Exam " + (i + 1)));
            tabPositionX[i] = 0;
            tabPositionY[i] = 0;
        }
    }
    public void totalDeExamens()
    {
        nbExam = 0;
        for (int i = 0; i < RessourcesBdD.listeDesExamensPrésents.Length; ++i)
        {
            if (IDLieu == RessourcesBdD.listeDesExamensPrésents[i].SonLieu.IDLieu)
            {
                ++nbExam;
            }
        }
        LesExam = new Examen[nbExam];
    }
}
