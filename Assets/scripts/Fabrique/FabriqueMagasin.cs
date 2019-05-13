using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueMagasin : MonoBehaviour {
    ObjetPrésent[] listeobjets;
    public GameObject tupleObjet;
    public RawImage rangtexture;
    public Text valeurOrcus;
    public Text valeurIA;
    public Text nomObjet;
    
    public Text[] textes = new Text[4];

    GameObject instance;
    public void Start()
    {
        bool test = testsiArtefactMagasin();
        if (test)
        {
            ObjetPrésent[] tableau = new ObjetPrésent[RessourcesBdD.listeDesObjets.Length];
            for(int i = 0 ; i< RessourcesBdD.listeDesObjets.Length ; ++i)
            {
                tableau[i] = new ObjetPrésent(RessourcesBdD.listeDesObjets[i].ID);
            }
            listeobjets = tableau;
        }
        else
        {
            listeobjets = RessourcesBdD.LeMagasin;
        }
        for (int i = 0; i<listeobjets.Length;++i)
        {
            ObjetPrésent obj = listeobjets[i];
            rangtexture.texture = obj.SonObjet.RangObjet.texture;
            valeurOrcus.text = RessourcesJoueur.getPriceInK(obj.SonPrixOrcus);
            valeurIA.text = RessourcesJoueur.getPriceInK(obj.SonPrixIA);
            nomObjet.text = obj.SonObjet.Nom;
            instance = Instantiate(tupleObjet, new Vector3(0, 0, 0), tupleObjet.transform.rotation);
            instance.transform.parent = GameObject.Find("HorizontalLayout").transform;
            instance.transform.name = "Objet " + i;
        }

        // On affiche les ressources du joueur dans la zone appropriée
        getRessources();
    }

    public bool testsiArtefactMagasin()
    {
        int total = 0;
        string requete = "Select Count(*) AS Total, IDArtefact from artefact_used where IDPCharacter NOT IN (Select IDPCharacter from item_bought WHERE IDPCharacter="+Joueur.IDJoueur+") AND IDPCharacter="+Joueur.IDJoueur+" AND IDArtefact IN(Select IDArtefact from artefact WHERE IDBonus IN(Select IDBonus from bonus WHERE BonusName='Boutique'));";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            total = Int32.Parse(lien["Total"].ToString());
        }
        lien.Close();
        if(total > 0)
        {
            return true;
        }
        return false;
    }

    public void getRessources()
    {
        if (Joueur.MesRessources != null)
        {
            for (int i = 0; i < Joueur.MesRessources.Length; ++i)
            {
                switch (RessourcesBdD.listeDesRessources[i].NomRessource)
                {
                    // SOCIAL
                    case "Social":
                        //barres[0].value = (Joueur.MesRessources[i]);
                        textes[0].text = Joueur.MesRessources[i].ToString() + "%";
                        break;

                    // DIVERTISSEMENT
                    case "Divertissement":
                        //barres[1].value = (Joueur.MesRessources[i]);
                        textes[1].text = Joueur.MesRessources[i].ToString() + "%";
                        break;

                    //ORCUS
                    case "Orcus":
                        textes[2].text = Joueur.MesRessources[i].ToString();
                        break;

                    // MATIERE IA
                    case "IA":
                        textes[3].text = Joueur.MesRessources[i].ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }    
}
