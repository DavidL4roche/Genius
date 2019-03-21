using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriquePreRequis : MonoBehaviour
{
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    public GameObject Tuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;
    public GameObject BlockComp;
    public GameObject BlockRess;
    public Image ImageTuple;
    public Button lancer;
    public Button ameliorer;
    GameObject instance;

    public GameObject TupleObjet;
    public Text TupleNomObjet;
    public Text TupleQuantiteObjet;
    public Text NomTuple;

    public void Start()
    {
        Gain.calculDesGains(mr);
        Perte.calculDesPertes(mr);
        blockdesgains();
    }

    public void blockdesgains()
    {
        ImageTuple.color = new Color(1F, 1F, 1F, 1F);
        for (int i = 0; i < mr.SesGains.Length; ++i)
        {
            if (mr.SesGains[i].ValeurDuGain > 0 && mr.SesGains[i].NomGain != "Objet")
            {
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.gameObject.SetActive(true);
                NomTuple.text = mr.SesGains[i].NomGain;
                ValeurTupleTexte.text = mr.SesGains[i].ValeurDuGain.ToString();
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
        for (int i = 0; i < mr.SesObjets.Length; ++i)
        {
            ValeurTupleSlider.gameObject.SetActive(false);
            ValeurTupleTexte.gameObject.SetActive(true);
            TupleNomObjet.text = mr.SesObjets[i].Nom;
            TupleQuantiteObjet.text = "1";
            instance = Instantiate(TupleObjet, new Vector3(0F, 0F, 0F), TupleObjet.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "Tuple " + (i + 1);
        }
    }
}
