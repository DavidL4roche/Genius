using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueMission : MonoBehaviour {
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];

    public Text TitreMission;
    public RawImage ImageRang;
    public Text NomEntreprise;
    public Text NomTemps;
    public Text ValeurMetier;

    public Text GainsOrcus;
    public Text GainsIA;

    public GameObject Tuple;
    public Text nomTuple;
    public GameObject IDGO;
    public Text ValeurTupleTexte;
    public Text Divert;
    public Text Social;
    public Image ImageTuple;
    public GameObject BarreRequis;
    public Image FondGainJoueur;

    public GameObject BlockRecap;
    public RawImage ImageObjet;
    public Text Multi;
    public Text Concentration;

    public Button lancer;
    public Text textLancer;
    public RawImage Icone;
    public RawImage IconeIA;
    public Text IA;

    public GameObject metier;

    public Button ameliorer;
    public RawImage fondAmeliorer;

    GameObject instance;

    private void Start()
    {
        // Calcul des gains et pertes de la mission
        Gain.calculDesGains(mr);
        Perte.calculDesPertes(mr);

        // On affiche les éléments standards de la mission
        TitreMission.text = mr.NomMission;
        ImageRang.texture = mr.RangMission.texture;
        NomEntreprise.text = mr.MissionEntreprise.NomEntreprise.ToUpper();
        NomTemps.text = mr.SaDurée.NomDuree;

        metier.SetActive(true);
        if (mr.MetierAssocie != "")
        {
            ValeurMetier.text = mr.MetierAssocie;
        }
        else
        {
            metier.SetActive(false);
        }

        // On remplit le bloc mission
        blockMission();

        // On rend les éléments d'amélioration de mission nuls
        FicheAmélioration.Optimisation = 1.0F;
        FicheAmélioration.IDObjetUtilise = 0;
        FicheAmélioration.Concentration = false;

    }

    // Génère le bloc Mission
    public void blockMission()
    {
        // On affiche les gains en ressources
        for (int i = 0; i < mr.SesGains.Length; ++i)
        {
            switch (mr.SesGains[i].NomGain)
            {
                case "IA":
                    GainsIA.text = "+" + RessourcesJoueur.getPriceInK(mr.SesGains[i].ValeurDuGain).ToString();
                    break;
                case "Orcus":
                    GainsOrcus.text = "+" + RessourcesJoueur.getPriceInK(mr.SesGains[i].ValeurDuGain).ToString();
                    break;
                default:
                    break;
            }
        }

        // On affiche les pertes en ressources
        for (int i = 0; i < mr.SesPertes.Length; ++i)
        {
            if (mr.SesPertes[i].ValeurDeLaPerte != 0)
            {
                Text Texte = nomTuple;
                Texte.text = mr.SesPertes[i].NomPerte;
                ValeurTupleTexte.gameObject.SetActive(true);

                switch (mr.SesPertes[i].NomPerte)
                {
                    case "Divertissement":
                        Divert.text = "-" + mr.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
                        break;
                    case "Social":
                        Social.text = "-" + mr.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
                        break;
                    default:
                        break;
                }

                verificationRessAvecJoueur(mr.SesPertes[i].ValeurDeLaPerte, mr.SesPertes[i].NomPerte);
            }
        }

        // On rend les éléments de lancement de mission actifs
        rendreActifLancer(true);

        // On affiche les compétences requises
        int decalage = 0;
        for (int i = 0; i < mr.CompétencesRequises.Length; ++i)
        {
            Text Texte = nomTuple;
            Text ID = IDGO.GetComponentInChildren<Text>();
            ID.text = mr.CompétencesRequises[i].ID.ToString();
            Texte.text = ">_" + truncateString(mr.CompétencesRequises[i].NomCompétence, 26);
            //ValeurTupleSlider.gameObject.SetActive(true);
            ValeurTupleTexte.gameObject.SetActive(false);
            int valeurr = mr.CompétencesRequises[i].Valeur;
            /*
            ValeurTupleSlider.value = valeurr;
            SliderTexte.text = valeurr.ToString();
            */

            verificationCompAvecJoueur(mr.CompétencesRequises[i].ID, valeurr);
            instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout1").transform;
            instance.transform.name = "Tuple " + (i + 1);
            instance.tag = "competence";

            ++decalage;
        }
        decalage = 0;

            Multi.text = "x" + FicheAmélioration.Optimisation.ToString();
        Concentration.text = (FicheAmélioration.Concentration) ? "On" : "Off";

        //Objet
        if (FicheAmélioration.IDObjetUtilise == 0)
        {
            ImageObjet.texture = Resources.Load<Texture>("icones/ObjetCross");
        }
        else
        {
            ImageObjet.texture = Resources.Load<Texture>("icones/Item" + FicheAmélioration.IDObjetUtilise);
        }
    }

    // ------------------------------------------ AUTRES FONCTIONS --------------------------------------------

    // Vérifie les compétences du joueur avec celles demandées et affiche le tuple avec la couleur correspondante
    public void verificationCompAvecJoueur(int idComp, int valeur)
    {
        int i = 0;
        for (; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            if (idComp == RessourcesBdD.listeDesCompétences[i].ID)
            {
                break;
            }
        }

        // On affiche visuellement la valeur du joueur dans la compétence
        double pourcentage = ((double)Joueur.MesValeursCompetences[i] / 100) * 264.07;
        ImageTuple.GetComponent<RectTransform>().sizeDelta = new Vector2((float)pourcentage, 37.548f);

        // On affiche le trait représentant la valeur de l'examen
        double posBarre = ((double)valeur / 100) * 255.5;
        posBarre = 14.5 + posBarre;
        BarreRequis.GetComponent<RectTransform>().sizeDelta = new Vector2((float)posBarre, 46.76f);

        // On affiche le gain possible
        if (Joueur.MesValeursCompetences[i] >= valeur)
        {
            ImageTuple.color = changeColor(true);

            double gainJoueur = Joueur.MesValeursCompetences[i];
            if (Joueur.MesValeursCompetences[i] < 100) {
                gainJoueur += mr.SesGains[5].ValeurDuGain;
            }
            gainJoueur = ((double)gainJoueur / 100) * 264.07;

            FondGainJoueur.GetComponent<RectTransform>().sizeDelta = new Vector2((float)gainJoueur, 37.548f);
        }
        else
        {
            FondGainJoueur.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 37.548f);

            // On rend les éléments de lancement de mission inactifs
            rendreActifLancer(false);
        }
    }

    // Vérifie les ressources du joueur avec celles demandées et affiche le tuple avec la couleur correspondante
    public void verificationRessAvecJoueur(int valeurressource, string nomPrerequis)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (nomPrerequis == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                if (Joueur.MesRessources[i] >= valeurressource)
                {
                    ImageTuple.color = changeColor(true);
                }
                else
                {
                    // On rend les éléments de lancement de mission inactifs
                    rendreActifLancer(false);
                }
            }
        }
    }

    // Renvoie la couleur (Color) si la condition est vraie ou fausse
    public Color changeColor(bool color)
    {
        if (color)
        {
            Color myColor = new Color32(57, 119, 155, 255);
            return myColor;
        }
        else
        {
            Color myColor = new Color32(55, 57, 75, 255);
            return myColor;
        }
    }

    public string truncateString(string myStr, int trun)
    {
        // Si la chaine est supérieur en taille au paramètre trun alors on ajoute "..." à la fin
        if (myStr.Length >= trun - 4)
        {
            return myStr.Substring(0, trun - 4) + ((myStr.Length - 1 >= trun) ? "..." : "");
        }
        else
        {
            return myStr;
        }
    }

    public void changeColorLancer(bool boolean)
    {
        if (boolean)
        {
            Color32 vert = new Color32(32, 34, 52, 255);
            textLancer.color = vert;
            Icone.color = vert;
            IconeIA.color = vert;
            IA.color = vert;
        }
        else
        {
            Color32 black = new Color32(147, 147, 147, 255);
            textLancer.color = black;
            Icone.color = black;
            IconeIA.color = black;
            IA.color = black;
        }
    }

    public void rendreActifLancer(bool boolean)
    {
        //ImageTuple.color = changeColor(boolean);
        lancer.interactable = boolean;
        BlockRecap.SetActive(boolean);
        changeColorLancer(boolean);
        ameliorer.interactable = boolean;
        fondAmeliorer.color = changeColor(boolean);
    }
}
