using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class SupprimerUnAmi : MonoBehaviour {

    public bool continueSupprimer = false;

    public void SupprimerAmi()
    {
        int idami = Joueur.MesAmis[VerificationAmi.AmiChoisi].SonID;
        StartCoroutine(fonctionSupprimerAmi(idami));
    }

    public void Update()
    {
        if (continueSupprimer)
        {
            Debug.Log("On rentre dans la boucle !");
            StartCoroutine(RessourcesBdD.RecupDeLaListeDesJoueurs());
            StartCoroutine(RessourcesBdD.RecupMesAmis());
            FermerPopup fermerpop = new FermerPopup();
            FermerUneFenetre fermerfen = new FermerUneFenetre();
            ChargerFenetreSupp chargerfen = new ChargerFenetreSupp();
            fermerpop.Fermer("ValidSupprimerAmi");
            fermerfen.Fermer("ReseauSocial");
            chargerfen.Charger("ReseauSocial");

            continueSupprimer = false;
        }
    }

    public IEnumerator fonctionSupprimerAmi(int idAmi)
    {
        string urlComp = Configuration.url + "scripts/SupprimerAmi.php?id=" + Joueur.IDJoueur + "&idAmi=" + idAmi;
        WWW dl = new WWW(urlComp);
        yield return dl;
        continueSupprimer = true;
        Debug.Log("ContinueSupprimer est true");
    }
}
