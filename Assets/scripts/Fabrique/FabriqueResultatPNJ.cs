using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatPNJ : MonoBehaviour {
    Mission mission = RessourcesBdD.listeDesMissions[VerificationPNJ.MissionChoisi];
    PNJPrésent mr = SpawnerMission.SonPNJ;
    public GameObject Tuple;
    public Text nomTuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;

    public Text GainOrcus;
    public Text GainIA;
    public RawImage GainArtefact;
    public Text PerteDivert;
    public Text PerteSocial;

    //public Text SliderTexte;
    //public Image ImageTuple;
    GameObject instance;
    public void Start()
    {
        //mission.voirRessources();
        AGagner();
        APerdu();
        blockdesprerequis();
        ValeurTupleTexte.color = new Color(1F, 1F, 1F, 1F);
        //ReloadMissions();
        Joueur.transfertEnBase();
        transfertEnBase(mr.SonPNJ.SonArtefact.IDArtefact);
        RessourcesBdD.recupPNJJouable();
        RessourcesBdD.RecupArtefactJouable();
    }
    public void blockdesprerequis()
    {
        for (int i = 0; i < mission.SesGains.Length; ++i)
        {
            switch(mission.SesGains[i].NomGain)
            {
                case "IA":
                    GainIA.text = RessourcesJoueur.getPriceInK(mission.SesGains[i].ValeurDuGain).ToString();
                    break;
                case "Orcus":
                    GainOrcus.text = RessourcesJoueur.getPriceInK(mission.SesGains[i].ValeurDuGain).ToString();
                    break;
                case "Artefact":
                    GainArtefact.texture = Resources.Load<Texture>("icones/Artefact" + FabriqueMissionPNJ.trouverArtefact(mr.SonPNJ.SonArtefact.IDArtefact));
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < mission.SesPertes.Length; ++i)
        {
            switch(mission.SesPertes[i].NomPerte)
            {
                case "Social":
                    PerteSocial.text = "-" + mission.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
                    break;
                case "Divertissement":
                    PerteDivert.text = "-" + mission.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
                    break;
                default:
                    break;
            }
        }
    }
    void AGagner()
    {
        for (int i = 0; i < mission.SesGains.Length; ++i)
        {
            if (mission.SesGains[i].ValeurDuGain != 0)
            {
                switch (mission.SesGains[i].NomGain)
                {
                    case "Orcus":
                    case "IA":
                    case "Divertissement":
                    case "Social":
                        gainRess(mission.SesGains[i].NomGain, mission.SesGains[i].ValeurDuGain);
                        break;
                    case "Compétence":
                        gainComp(mission.SesGains[i].ValeurDuGain);
                        break;
                    case "Objet":
                        gainObjet();
                        break;
                    case "Artefact":
                        gainArtefact();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void gainComp(int valeur)
    {
        for (int i = 0; i < mission.CompétencesRequises.Length; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesCompétences.Length; ++j)
            {
                if (mission.CompétencesRequises[i].ID == RessourcesBdD.listeDesCompétences[j].ID)
                {
                    if ((Joueur.MesValeursCompetences[j] + valeur) > 100)
                    {
                        Joueur.MesValeursCompetences[j] = 100;
                    }
                    else
                    {
                        Joueur.MesValeursCompetences[j] += valeur;
                        //Debug.Log(Joueur.MesValeursCompetences[j]+ " gain de compétences");
                    }
                }
            }
        }
    }
    void gainRess(string NomRess, int valeur)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (NomRess == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                Joueur.MesRessources[i] = Joueur.MesRessources[i] + valeur;
                if ((NomRess == "Social" || NomRess == "Divertissement") && Joueur.MesRessources[i] > 100)
                {
                    Joueur.MesRessources[i] = 100;
                }
            }
        }
    }
    void gainObjet()
    {
        for (int i = 0; i < mission.SesObjets.Length; ++i)
        {
            for (int j = 0; i < RessourcesBdD.listeDesObjets.Length; ++j)
            {
                if (mission.SesObjets[i].ID == RessourcesBdD.listeDesObjets[j].ID)
                {
                    ++Joueur.MesObjets[j];
                    Debug.Log("Objet gagné");
                    break;
                }
            }
        }
    }
    void gainArtefact()
    {
        for (int i = 0; i < RessourcesBdD.listeDesArtefacts.Length; ++i)
        {
            if (mr.SonPNJ.SonArtefact.IDArtefact == RessourcesBdD.listeDesArtefacts[i].IDArtefact)
            {
                Joueur.MesArtefacts[i] = true;
                transfertEnBase(RessourcesBdD.listeDesArtefacts[i].IDArtefact);
                break;
            }
        }
    }
    public void transfertEnBase(int idartefact)
    {
        string requete = "Insert INTO artefact_pc VALUES (" + idartefact + "," + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
    }
    void APerdu()
    {
        //Ressources
        for (int i = 0; i < mission.SesPertes.Length; ++i)
        {
            if (mission.SesPertes[i].ValeurDeLaPerte != 0)
            {
                for (int j = 0; j < RessourcesBdD.listeDesRessources.Length; ++j)
                {
                    if (RessourcesBdD.listeDesRessources[j].NomRessource == mission.SesPertes[i].NomPerte)
                    {
                        Joueur.MesRessources[j] = Joueur.MesRessources[j] - mission.SesPertes[i].ValeurDeLaPerte;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        //Objet
        if (FicheAmélioration.IDObjetUtilise != 0)
        {
            for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
            {
                if (RessourcesBdD.listeDesObjets[i].ID == FicheAmélioration.IDObjetUtilise)
                {
                    --Joueur.MesObjets[i];
                    break;
                }
            }
        }
        else
        {

        }
    }
    public void ReloadMissions()
    {
        string requete = "Insert INTO friend VALUES (" + Joueur.IDJoueur + "," + SpawnerMission.SonPNJ.SonPNJ.IDPNJ + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
    }
}
