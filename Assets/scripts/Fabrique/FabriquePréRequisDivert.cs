using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriquePréRequisDivert : MonoBehaviour {
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    MissionDivertissement divert = SpawnerMission.SonDivertissement;

    public Text coutIA;
    
    public Button lancer;
    public Text textLancer;
    public RawImage Icone;

    GameObject instance;

    public void Start()
    {
        Perte.calculDesPertes(divert);
        blockdesprerequis();
    }
    public void blockdesprerequis()
    {
        lancer.interactable = true;
        for (int i = 0; i < divert.SesPertes.Length; ++i)
        {
            //Debug.Log(mission.tabprerequis[i] + " = " + mission.tabValeurPrerequis[i]);
            if (divert.SesPertes[i].NomPerte =="Orcus")
            {
                coutIA.text = ((divert.SesPertes[i].ValeurDeLaPerte > 0) ? "-" : "") + divert.SesPertes[i].ValeurDeLaPerte.ToString();
                verificationRessAvecJoueur(divert.SesPertes[i].ValeurDeLaPerte, "Orcus");
            }
        }
    }

    // Vérifie les ressources du joueur avec celles demandées et affiche le tuple avec la couleur correspondante
    public void verificationRessAvecJoueur(int valeurressource, string nomPrerequis)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (nomPrerequis == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                if (Joueur.MesRessources[i] < valeurressource)
                {
                    // On rend les éléments de lancement de mission inactifs
                    rendreActifLancer(false);
                }
            }
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
        }
        else
        {
            Color32 black = new Color32(147, 147, 147, 255);
            textLancer.color = black;
            Icone.color = black;
        }
    }

    public void rendreActifLancer(bool boolean)
    {
        lancer.interactable = boolean;
        changeColorLancer(boolean);
    }
}
