using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueMissionPNJ : MonoBehaviour
{
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    PNJPrésent mr = SpawnerMission.SonPNJ;
    public Text NomPnj;
    public RawImage iconePNJ;
    public Text NomArtefact;
    public GameObject TupleObjet;
    public Text TupleNomObjet;
    public Text TupleQuantiteObjet;
    public RawImage IconeTupleObjet;
    public GameObject Tuple;
    public Text NomTuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;

    public Text GainOrcus;
    public Text GainIA;
    public RawImage GainArtefact;

    //public Image ImageTuple;
    GameObject instance;

    public GameObject EcranTuto;

    private void Start()
    {
        // On vérifie que le joueur a fait le tuto PNJ (1)
        StartCoroutine(Joueur.VerifierStatusTuto(1, EcranTuto));

        Gain.calculDesGainsPNJ(mr.SaMission);
        Gain.attribuerUnArtefact(mr.SaMission,mr.SonPNJ.SonArtefact);
        GainArtefact.texture = Resources.Load<Texture>("icones/Artefact" + trouverArtefact(mr.SonPNJ.SonArtefact.IDArtefact));
        NomPnj.text = mr.SonPNJ.NomPNJ;
        iconePNJ.texture = Resources.Load<Texture>("icones/PNJ-Light" + mr.SonPNJ.IDPNJ);
        blockdesgains();
    }
    public void blockdesgains()
    {
        //ImageTuple.color = new Color(1F, 1F, 1F, 1F);
        for (int i = 0; i < mr.SaMission.SesGains.Length; ++i)
        {
            switch(mr.SaMission.SesGains[i].NomGain)
            {
                case "Orcus":
                    GainOrcus.text = RessourcesJoueur.getPriceInK(mr.SaMission.SesGains[i].ValeurDuGain).ToString();
                    break;
                case "IA":
                    GainIA.text = RessourcesJoueur.getPriceInK(mr.SaMission.SesGains[i].ValeurDuGain).ToString();
                    break;
                default:
                    break;
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

    public static string trouverArtefact(int IDArtefact)
    {
        switch(IDArtefact)
        {
            case 1:
                return "Orcus";
            case 2:
                return "Boutique";
            case 3:
                return "IA";
            case 4:
                return "Objets";
            case 5:
                return "Divertissement";
            case 6:
                return "Social";
            default:
                return null;
        }
    }
}
