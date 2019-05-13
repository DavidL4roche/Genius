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
            valeurOrcus.text = getPriceInK(obj.SonPrixOrcus);
            valeurIA.text = getPriceInK(obj.SonPrixIA);
            nomObjet.text = obj.SonObjet.Nom;
            instance = Instantiate(tupleObjet, new Vector3(0, 0, 0), tupleObjet.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "Objet " + i;
        }
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

    public string getPriceInK(int price)
    {
        if (price >= 10000)
        {
            string kPrice = price.ToString();
            return kPrice.Substring(0, 2) + "k";
        }
        else
        {
            return price.ToString();
        }
    }
}
