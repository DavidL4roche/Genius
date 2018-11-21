using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueDivertissement : MonoBehaviour {
    MissionDivertissement mr = SpawnerMission.SonDivertissement;
    public GameObject Tuple;
    public Text NomDivert;
    public Text NomRang;
    public Text NomTuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;
    public Image ImageTuple;
    public RawImage ImageRang;
    GameObject instance;
    private void Start()
    {
        //Debug.Log(VerificationMission.MissionChoisi);
        Gain.calculDesGains(mr);
        NomDivert.text = mr.NomDivertissement;
        NomRang.text = mr.SonRang.NomRang;
        ImageRang.texture = mr.SonRang.texture;
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
    }
}
