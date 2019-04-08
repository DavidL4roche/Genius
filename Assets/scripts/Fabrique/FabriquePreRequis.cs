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
    public Text ID;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;
    public Text SliderTexte;
    public GameObject BlockComp;
    public GameObject BlockRess;
    public Image ImageTuple;
    public Button lancer;
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
    public void blockdesprerequis()
    {
        lancer.interactable = true;
        ameliorer.interactable = true;
        int decalage = 0;
        for (int i = 0; i < mr.CompétencesRequises.Length; ++i)
        {
            Text Texte = nomTuple;
            ID.text = mr.CompétencesRequises[i].ID.ToString();
            Texte.text = mr.CompétencesRequises[i].NomCompétence;
            ValeurTupleSlider.gameObject.SetActive(true);
            ValeurTupleTexte.gameObject.SetActive(false);
            int valeurr = mr.CompétencesRequises[i].Valeur;
            ValeurTupleSlider.value = valeurr;
            SliderTexte.text = valeurr.ToString();
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
                    button.onClick.AddListener(TaskOnClick);
                    DestroyImmediate(button);
                }
            }
            else
            {
                continue;
            }
        }
    }

    public void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }

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
            ImageTuple.color = new Color(0F, 1F, 0F, 1F);
        }
        else
        {
            ImageTuple.color = new Color(1F, 0F, 0F, 1F);
            Destroy(GameObject.Find("Lancer"));
            Destroy(GameObject.Find("Ameliorer"));
            
        }
    }
    public void verificationRessAvecJoueur(int valeurressource , string nomPrerequis)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (nomPrerequis == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                if (Joueur.MesRessources[i] >= valeurressource)
                {
                    ImageTuple.color = new Color(0F, 1F, 0F, 1F);
                }
                else
                {
                    ImageTuple.color = new Color(1F, 0F, 0F, 1F);
                    lancer.interactable = false;
                    ameliorer.interactable = false;
                }
            }
        }   
    }
}
