using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueExamen : MonoBehaviour {
    //Examen examen = SpawnerExam.LesExam[VerificationExamen.ExamChoisi];
    Examen examen = RessourcesBdD.listeDesExamens[VerificationExamen.ExamChoisi];
    public GameObject Tuple;
    public Text NomExamen;
    //public Text NomRang;
    public Text NomTuple;
    public Text ValeurTupleTexte;
    public Image ImageTuple;
    public RawImage ImageRang;
    GameObject instance;
    private void Start()
    {
        Debug.Log("Nom examen : " + examen.NomExamen);
        //Debug.Log(VerificationMission.MissionChoisi);
        Gain.calculDesGains(examen);
        NomExamen.text = examen.NomExamen;
        //NomRang.text = examen.RangExamen.NomRang;
        ImageRang.texture = examen.RangExamen.texture;
        blockdesgains();
    }
    public void blockdesgains()
    {
        ImageTuple.color = new Color(1F, 1F, 1F, 1F);
        for (int i = 0; i < examen.SesGains.Length; ++i)
        {
            if (examen.SesGains[i].ValeurDuGain > 0 && examen.SesGains[i].NomGain != "Diplome")
            {
                ValeurTupleTexte.gameObject.SetActive(true);
                NomTuple.text = examen.SesGains[i].NomGain;
                ValeurTupleTexte.text = examen.SesGains[i].ValeurDuGain.ToString();
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
            else if(examen.SesGains[i].ValeurDuGain > 0 && examen.SesGains[i].NomGain == "Diplome")
            {
                ValeurTupleTexte.gameObject.SetActive(true);
                NomTuple.text = examen.LeDiplome.NomDiplome;
                ValeurTupleTexte.text = examen.SesGains[i].ValeurDuGain.ToString();
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
    }
}
