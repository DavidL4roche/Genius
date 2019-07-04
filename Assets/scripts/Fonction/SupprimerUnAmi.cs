using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using SimpleJSON;

public class SupprimerUnAmi : MonoBehaviour {

    public bool continueSupprimer = false;
    public bool continueReload = false;

    public void SupprimerAmi()
    {
        int idami = Joueur.MesAmis[VerificationAmi.AmiChoisi].SonID;
        StartCoroutine(fonctionSupprimerAmi(idami));
    }

    public void Update()
    {
        if (continueSupprimer)
        {
            StartCoroutine(RecupMesAmis());
            continueSupprimer = false;
        }

        if (continueReload)
        {
            FermerPopup fermerpop = new FermerPopup();
            FermerUneFenetre fermerfen = new FermerUneFenetre();
            ChargerFenetreSupp chargerfen = new ChargerFenetreSupp();
            fermerpop.Fermer("ValidSupprimerAmi");
            fermerfen.Fermer("ReseauSocial");
            chargerfen.Charger("ReseauSocial");
        }
    }

    public IEnumerator fonctionSupprimerAmi(int idAmi)
    {
        string urlComp = Configuration.url + "scripts/SupprimerAmi.php?id=" + Joueur.IDJoueur + "&idAmi=" + idAmi;
        WWW dl = new WWW(urlComp);
        yield return dl;
        continueSupprimer = true;
    }

    // Récupère les amis du joueur
    public IEnumerator RecupMesAmis()
    {
        Joueur.MesAmis = new AutreJoueur[0];
        string urlAmis = Configuration.url + "scripts/scriptsRessources/RecupMesAmis.php?id=" + Joueur.IDJoueur;

        WWW dlAmis = new WWW(urlAmis);
        yield return dlAmis;
        
        JSONNode NodeAmis = RessourcesBdD.RenvoiJSONScript(dlAmis);
        Joueur.MesAmis = new AutreJoueur[NodeAmis["msg"].Count];
        for (int i = 0; i < NodeAmis["msg"].Count; ++i)
        {
            Joueur.MesAmis[i] = new AutreJoueur((int)NodeAmis["msg"][i]["IDPCharacter"], NodeAmis["msg"][i]["PCName"].Value);
        }

        foreach (AutreJoueur ami in Joueur.MesAmis)
        {
            ami.trouverToutesInformations();
        }

        continueReload = true;
    }
}
