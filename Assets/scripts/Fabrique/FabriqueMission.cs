using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueMission : MonoBehaviour {
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    public GameObject TupleObjet;
    public Text TupleNomObjet;
    public Text TupleQuantiteObjet;
    public RawImage IconeTupleObjet;
    public GameObject Tuple;
    public Text NomTemps;
    public Text TitreMission;
    public Text NomRang;
    public Text NomEntreprise;
    public Text NomTuple;
    public Text ValeurTupleTexte;
    public Text ValeurMetier;
    public Slider ValeurTupleSlider;
    public Image ImageTuple;
    public RawImage ImageRang;
    GameObject instance;
    private void Start()
    {
        Gain.calculDesGains(mr);
        TitreMission.text = mr.NomMission;
        NomRang.text = mr.RangMission.NomRang;
        ImageRang.texture = mr.RangMission.texture;
        NomEntreprise.text = mr.MissionEntreprise.NomEntreprise;
        NomTemps.text = mr.SaDurée.NomDuree;
        //Debug.Log("Métier : " + mr.MetierAssocie);
        ValeurMetier.text = mr.MetierAssocie;
        blockdesgains();
    }
    public void blockdesgains()
    {
        ImageTuple.color = new Color(1F, 1F, 1F, 1F);
        for (int i = 0; i< mr.SesGains.Length; ++i)
        {
            if (mr.SesGains[i].ValeurDuGain > 0 && mr.SesGains[i].NomGain !="Objet")
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
        for ( int i = 0;i< mr.SesObjets.Length;++i)
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
