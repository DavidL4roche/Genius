using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchatEnMagasin : MonoBehaviour {

    public static bool continueAchatMagasin = false;
    public static bool continueReloadMagasin = false;
    public static bool continueReloadMag = false;

	// Use this for initialization
	public void Achat () {

        continueReloadMagasin = false;
        continueReloadMag = false;
        continueAchatMagasin = true;

        ObjetPrésent obj = RessourcesBdD.LeMagasin[VerificationObjet.ObjetChoisi];
        bool testsijoueurpeutacheter = testSiAssezDeRessource(obj);

        if (testsijoueurpeutacheter)
        {
            gainObjet(obj);
            //StartCoroutine(Joueur.transfertRessourcesEnBase());
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

        // On envoie en base l'objet acheté pour le joueur
        StartCoroutine(EnvoiObjetAcheteEnBase(obj.SonObjet.ID));

        // On envoie les données de ressources du joueur dans la base
        StartCoroutine(Joueur.transfertRessourcesEnBase());

        // On envoie les données d'objets du joueur dans la base
        StartCoroutine(Joueur.transfertObjetsEnBase());
    }

    public void Update()
    {
        if (continueReloadMagasin)
        {
            RessourcesBdD.LeMagasin = new ObjetPrésent[0];
            StartCoroutine(RessourcesBdD.RecupObjetMagasin());
        }

        if (continueReloadMag)
        {
            FermerPopup fermerPopup = new FermerPopup();
            FermerUneFenetre fermer = new FermerUneFenetre();
            ChargerFenetreSupp charger = new ChargerFenetreSupp();
            fermerPopup.Fermer("ValidMagasin");
            fermer.Fermer("Magasin");
            charger.Charger("Magasin");

            continueReloadMag = false;
        }
    }

    public static IEnumerator EnvoiObjetAcheteEnBase(int idObjet)
    {
        string urlComp = Configuration.url + "scripts/ObjetAchete.php?id=" + Joueur.IDJoueur + "&idObjet=" + idObjet;
        WWW dl = new WWW(urlComp);
        yield return dl;
    }
}
