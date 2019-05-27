using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueMissionPNJ : MonoBehaviour
{
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    PNJPrésent mr = SpawnerMission.SonPNJ;
    public Text NomPnj;
    public Text NomArtefact;
    public GameObject TupleObjet;
    public Text TupleNomObjet;
    public Text TupleQuantiteObjet;
    public RawImage IconeTupleObjet;
    public GameObject Tuple;
    public Text NomTuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;
    //public Image ImageTuple;
    GameObject instance;
    private void Start()
    {
        Gain.calculDesGainsPNJ(mr.SaMission);
        Gain.attribuerUnArtefact(mr.SaMission,mr.SonPNJ.SonArtefact);
        NomArtefact.text = mr.SonPNJ.SonArtefact.NomArtefact;
        NomPnj.text = mr.SonPNJ.NomPNJ;
        blockdesgains();
    }
    public void blockdesgains()
    {
        //ImageTuple.color = new Color(1F, 1F, 1F, 1F);
        for (int i = 0; i < mr.SaMission.SesGains.Length; ++i)
        {
            if (mr.SaMission.SesGains[i].ValeurDuGain > 0 && mr.SaMission.SesGains[i].NomGain != "Objet")
            {
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.gameObject.SetActive(true);
                NomTuple.text = mr.SaMission.SesGains[i].NomGain;
                ValeurTupleTexte.text = mr.SaMission.SesGains[i].ValeurDuGain.ToString();
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
        /*
        for (int i = 0; i < mr.SaMission.SesObjets.Length; ++i)
        {
            ValeurTupleSlider.gameObject.SetActive(false);
            ValeurTupleTexte.gameObject.SetActive(true);
            TupleNomObjet.text = mr.SaMission.SesObjets[i].Nom;
            TupleQuantiteObjet.text = "1";
            instance = Instantiate(TupleObjet, new Vector3(0F, 0F, 0F), TupleObjet.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "Tuple " + (i + 1);
        }
        */
    }
}
