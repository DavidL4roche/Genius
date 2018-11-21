using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatSocial : MonoBehaviour {
    public GameObject Tuple;
    public Text nomTuple;
    public Text Quantité;
    public Image Cadre;
    GameObject instance;
    public void Start()
    {
        Cadre.color = new Color(0F, 1F, 0F, 1F);
        switch (VerifActionSocial.ActionSocialeChoisie)
        {
            case "Objet":
                Objet obj = gainObjetSocial();
                int Social = 10;
                // les tuples
                nomTuple.text = "Social";
                Quantité.text = "+"+Social.ToString();
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Social";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                nomTuple.text = obj.Nom;
                Quantité.text = "1";
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Objet";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                GainSocial(Social);
                GainObjet(obj);
                break;
            case "Compétence":
                int[] gaincompetsocial = gainCompSocial();
                nomTuple.text = "Social";
                Quantité.text = gaincompetsocial[1].ToString();
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Social";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                nomTuple.text = "Compétence";
                Quantité.text = "+"+ gaincompetsocial[0].ToString();
                instance = Instantiate(Tuple, new Vector3(0, 0, 0), Tuple.transform.rotation);
                instance.transform.name = "Compétence";
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                GainSocial(gaincompetsocial[1]);
                GainComp(gaincompetsocial[0], RessourcesBdD.listeDesCompétences[VerifTupleActionSocial.TupleChoisieActionSocial].ID);
                break;
            default:
                break;
        }
        
    }
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
    int[] gainCompSocial()
    {
        int[] les2Valeurs= new int[2];
        int[] tabDeValeursGainComp = {1,2,5,10};
        int[] tabDeValeurSocial = {5,10,20,40};
        int valeurcompami = Joueur.MesAmis[VerificationAmi.AmiChoisi].SesCompétences[VerifTupleActionSocial.TupleChoisieActionSocial];
        if (valeurcompami > 75)
        {
            int random = Random.Range(0, 3);
            les2Valeurs[0] = tabDeValeursGainComp[random];
        }
        else if (valeurcompami > 50)
        {
            int random = Random.Range(0,2);
            les2Valeurs[0] = tabDeValeursGainComp[random];
        }
        else if (valeurcompami > 25)
        {
            int random = Random.Range(0, 1);
            les2Valeurs[0] = tabDeValeursGainComp[random];
        }
        else
        {
            les2Valeurs[0] = tabDeValeursGainComp[0]; 
        }
        switch (les2Valeurs[0])
        {
            case 40:
                les2Valeurs[1] = tabDeValeurSocial[3];
                break;
            case 20:
                les2Valeurs[1] = tabDeValeurSocial[2];
                break;
            case 10:
                les2Valeurs[1] = tabDeValeurSocial[1];
                break;
            default:
                les2Valeurs[1] = tabDeValeurSocial[0];
                break;
            
        }
        return les2Valeurs;
    }
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
