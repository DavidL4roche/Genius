using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriquePreRequis : MonoBehaviour
{
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    public GameObject Tuple;
    public Text nomTuple;
    public GameObject IDGO;
    public Text ValeurTupleTexte;
    public Text Divert;
    public Text Social;
    public GameObject BlockComp;
    public GameObject BlockRess;
    public Image ImageTuple;
    public GameObject BlockRecap;

    public Button lancer;
    public Text textLancer;
    public RawImage Icone;
    public RawImage IconeIA;
    public Text IA;

    public Button ameliorer;
    GameObject instance;

    public void Start()
    {
        Perte.calculDesPertes(mr);
        blockdesprerequis();
        /*
        // Si l'objet competence (bouton dans la liste) est trouvé
        if (competence != null)
        {
            competence.onClick.AddListener(TaskOnClick);
            //Destroy(competence);
        }
        
        competence.onClick.AddListener(TaskOnClick);*/
    }

    // Permet de remplir le bloc des prérequis
    public void blockdesprerequis()
    {
        lancer.interactable = true;
        ameliorer.interactable = true;
        BlockRecap.SetActive(false);
        changeColorLancer(true);

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
        for (int i = 0; i< mr.SesPertes.Length; ++i)
        {
            //Debug.Log(mission.tabprerequis[i] + " = " + mission.tabValeurPrerequis[i]);
            if (mr.SesPertes[i].ValeurDeLaPerte != 0)
            {
                Text Texte = nomTuple;
                Texte.text = mr.SesPertes[i].NomPerte;
                ValeurTupleTexte.gameObject.SetActive(true);

                switch(mr.SesPertes[i].NomPerte)
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
                
                /*
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = mr.SesPertes[i].ValeurDeLaPerte.ToString();
                verificationRessAvecJoueur(mr.SesPertes[i].ValeurDeLaPerte, mr.SesPertes[i].NomPerte);    
                instance = Instantiate(Tuple,new Vector3(0F,0F,0F),Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout2").transform;
                instance.transform.name = "Tuple " + (i + 1);
                ++decalage;

                // On supprime les boutons du tuple
                Button[] buttons = instance.GetComponentsInChildren<Button>();
                foreach (Button button in buttons)
                {
                    DestroyImmediate(button);
                }
                */
            }
            else
            {
                continue;
            }
        }
    }

    // Vérifie les compétences du joueur avec celles demandées et affiche le tuple avec la couleur correspondante
    public void verificationCompAvecJoueur(int idComp,int valeur)
    {
        int i = 0;
        for (; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            if(idComp == RessourcesBdD.listeDesCompétences[i].ID)
            {
                break;
            }
        }

        if(Joueur.MesValeursCompetences[i] >= valeur)
        {
            //Debug.Log("Joueur : " + Joueur.MesValeursCompetences[i] + " - Valeur demandée : " + valeur);
            ImageTuple.color = changeColor(true);
        }
        else
        {
            //Debug.Log("Joueur : " + Joueur.MesValeursCompetences[i] + " - Valeur demandée : " + valeur);
            ImageTuple.color = changeColor(false);
            //nomTuple.color = new Color32(153,154,164,255);
            //Destroy(GameObject.Find("Lancer"));
            //Destroy(GameObject.Find("Ameliorer"));
            lancer.interactable = false;
            ameliorer.interactable = false;
            // TODO : Changer les couleurs des boutons pour montrer qu'ils ne sont pas cliquables
        }
    }

    // Vérifie les ressources du joueur avec celles demandées et affiche le tuple avec la couleur correspondante
    public void verificationRessAvecJoueur(int valeurressource , string nomPrerequis)
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
                    ImageTuple.color = changeColor(false);
                    lancer.interactable = false;
                    ameliorer.interactable = false;
                    changeColorLancer(false);
                }
            }
        }   
    }

    // Renvoie la couleur (Color) si la condition est vraie ou fausse
    public Color changeColor(bool color)
    {
        if (color)
        {
            Color myColor = new Color32(57,119,155, 255);
            return myColor;
        }
        else
        {
            Color myColor = new Color32(55,57,75, 255);
            return myColor;
        }
    }

    public string truncateString(string myStr, int trun)
    {
        // Si la chaine est supérieur en taille au paramètre trun alors on ajoute "..." à la fin
        if (myStr.Length >= trun-4)
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
            textLancer.color = new Color32(43, 152, 26, 255);
            Icone.color = new Color32(43, 152, 26, 255);
            IconeIA.color = new Color32(43, 152, 26, 255);
            IA.color = new Color32(43, 152, 26, 255);
        }
    }
}
