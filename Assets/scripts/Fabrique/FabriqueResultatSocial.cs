using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatSocial : MonoBehaviour {
    public GameObject Tuple;
    public Text nomTuple;
    public Text Quantité;
    public RawImage Cadre;
    GameObject instance;
    public void Start()
    {
        Cadre.color = new Color32(6, 212, 168, 255);
        switch (VerifActionSocial.ActionSocialeChoisie)
        {
            case "Objet":
                // On génère l'objet gagné
                Objet obj = gainObjetSocial();

                // On fait gagner +10 de Social au joueur
                int Social = 10;

                // Tuple Social
                nomTuple.text = "Social";
                Quantité.text = "+" + Social.ToString();
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Social";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;

                // Tuple Objet
                nomTuple.text = obj.Nom;
                Quantité.text = "1";
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Objet";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;

                // On ajoute les gains en Social et en Objet au Joueur
                GainSocial(Social);
                GainObjet(obj);

                // On envoie les objets et les ressources (Social) en base
                Joueur.transfertRessourcesEnBase();
                Joueur.transfertObjetsEnBase();
                break;

            case "Compétence":
                //On génère les gains de compétences et de social
                int[] gaincompetsocial = gainCompSocial();

                // Tuple Social
                nomTuple.text = "Social";
                Quantité.text = gaincompetsocial[1].ToString();
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Social";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;

                // Tuple Compétence
                nomTuple.text = "Compétence";
                Quantité.text = "+"+ gaincompetsocial[0].ToString();
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Compétence";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;

                // On ajoute les gains en Social et en Compétence au Joueur
                GainSocial(gaincompetsocial[1]);
                GainComp(gaincompetsocial[0], RessourcesBdD.listeDesCompétences[VerifTupleActionSocial.TupleChoisieActionSocial].ID);

                // On envoie les compétences et les ressources (Social) en base
                Joueur.transfertRessourcesEnBase();
                Joueur.transfertCompetencesEnBase();
                break;

            default:
                break;
        }
        
    }

    // Retourne l'objet de l'ami cliqué (10% de chances) ou un objet de rang C (90% de chances)
    Objet gainObjetSocial()
    {
        Objet obj;
        if (Joueur.MesAmis[VerificationAmi.AmiChoisi].SesObjets[VerifTupleActionSocial.TupleChoisieActionSocial] > 0)
        {
            int random = Random.Range(0,100);
            if (random < 10)
            {
                obj = new Objet(RessourcesBdD.listeDesObjets[VerifTupleActionSocial.TupleChoisieActionSocial]);
            }
            else
            {
                obj = RessourcesBdD.randomObjetAvecRang("C");
            }
        }
        else
        {
            obj = RessourcesBdD.randomObjetAvecRang("C");
        }
        return obj;
    }

    // Retourne les valeurs de compétences gagnées
    int[] gainCompSocial()
    {
        int[] les2Valeurs= new int[2];
        int[] tabDeValeursGainComp = {1,2,5,10};
        int[] tabDeValeurSocial = {5,10,20,40};
        int valeurcompami = Joueur.MesAmis[VerificationAmi.AmiChoisi].SesCompétences[VerifTupleActionSocial.TupleChoisieActionSocial];
        if (valeurcompami > 75)
        {
            Debug.Log("Valeur compétence ami > 75 !");
            int random = Random.Range(0, 3);
            les2Valeurs[0] = tabDeValeursGainComp[random];
        }
        else if (valeurcompami > 50)
        {
            Debug.Log("Valeur compétence ami > 50 !");
            int random = Random.Range(0,2);
            les2Valeurs[0] = tabDeValeursGainComp[random];
        }
        else if (valeurcompami > 25)
        {
            Debug.Log("Valeur compétence ami > 25 !");
            int random = Random.Range(0, 1);
            les2Valeurs[0] = tabDeValeursGainComp[random];
        }
        else
        {
            Debug.Log("Valeur compétence ami nulle !");
            les2Valeurs[0] = tabDeValeursGainComp[0]; 
        }
        switch (les2Valeurs[0])
        {
            case 10://40:
                Debug.Log("Gain comp : 10");
                les2Valeurs[1] = tabDeValeurSocial[3];
                break;
            case 5://20:
                Debug.Log("Gain comp : 5");
                les2Valeurs[1] = tabDeValeurSocial[2];
                break;
            case 2:// 10:
                Debug.Log("Gain comp : 2");
                les2Valeurs[1] = tabDeValeurSocial[1];
                break;
            default:
                Debug.Log("Gain comp : 1");
                les2Valeurs[1] = tabDeValeurSocial[0];
                break;
            
        }
        return les2Valeurs;
    }

    // On ajoute l'objet en paramètres dans les objets du joueur
    void GainObjet(Objet obj)
    {
        for(int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            if (obj.ID == RessourcesBdD.listeDesObjets[i].ID)
            {
                ++Joueur.MesObjets[i];
            }
        }
    }

    // On ajoute la valeur d'une compétence (paramètre) à la compétence du joueur
    void GainComp(int valcomp, int idcomp)
    {
        for (int i = 0;i<RessourcesBdD.listeDesCompétences.Length;++i )
        {
            if (idcomp == RessourcesBdD.listeDesCompétences[i].ID)
            {
                int total = valcomp + Joueur.MesValeursCompetences[i];
                if (total > 100)
                {
                    Joueur.MesValeursCompetences[i] = 100;
                }
                else
                {
                    Joueur.MesValeursCompetences[i] = total;
                }
            }
        }
    }

    // On ajoute n (paramètre) points de Social au joueur
    void GainSocial(int val)
    {
        for (int i = 0; i<RessourcesBdD.listeDesRessources.Length;++i)
        {
            if (RessourcesBdD.listeDesRessources[i].NomRessource == "Social")
            {
                int total = val + Joueur.MesRessources[i];
                if (total > 100)
                {
                    Joueur.MesRessources[i] = 100;
                }
                else
                {
                    Joueur.MesRessources[i] = total;
                }
            }
        }
    }
}
