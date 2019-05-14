using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchatEnMagasin : MonoBehaviour {
	// Use this for initialization
	public void Achat () {
        ObjetPrésent obj = RessourcesBdD.LeMagasin[VerificationObjet.ObjetChoisi];
        bool testsijoueurpeutacheter = testSiAssezDeRessource(obj);
        if (testsijoueurpeutacheter)
        {
            gainObjet(obj);
        }
        else
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Pas assez de ressources.";
        }
	}	
    bool testSiAssezDeRessource(ObjetPrésent obj)
    {
        for (int i =0; i<RessourcesBdD.listeDesRessources.Length;++i)
        {
            switch (RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                case "Orcus":
                    if (Joueur.MesRessources[i]>obj.SonPrixOrcus)
                    {

                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "IA":
                    if (Joueur.MesRessources[i] > obj.SonPrixIA)
                    {

                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
        }
        return true;
    }
    public void gainObjet(ObjetPrésent obj)
    {
        for(int i = 0; i<RessourcesBdD.listeDesObjets.Length; ++i)
        {
            if(obj.SonObjet.ID == RessourcesBdD.listeDesObjets[i].ID)
            {
                ++Joueur.MesObjets[i];
                break;
            }
        }
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            switch (RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                case "Orcus":
                    Joueur.MesRessources[i] = Joueur.MesRessources[i] - obj.SonPrixOrcus;
                    break;
                case "IA":
                    Joueur.MesRessources[i] = Joueur.MesRessources[i] - obj.SonPrixIA;
                    break;
                default:
                    break;
            }
        }
        string requete = "Insert INTO item_bought VALUES (" + obj.SonObjet.ID + "," + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();

        // On envoie les données de ressources du joueur dans la base
        Joueur.transfertRessourcesEnBase();

        FermerUneFenetre fermer = new FermerUneFenetre();
        fermer.Fermer("Magasin");
        RessourcesBdD.LeMagasin = new ObjetPrésent[0];
        RessourcesBdD.RecupObjetMagasin();
        ChargerFenetreSupp charger = new ChargerFenetreSupp();
        charger.Charger("Magasin");
    }
}
