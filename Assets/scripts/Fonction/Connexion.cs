using UnityEngine;
using MySql.Data.MySqlClient;
using SimpleJSON;
using System.Collections;

public class Connexion : MonoBehaviour {

    public static MySqlConnection connexion;

    public void Start()
    {
        try
        {
            DontDestroyOnLoad(gameObject);
            string constr = "Server=" + Configuration.host + "; Port=3306; Database=" + Configuration.database + "; Uid=" + Configuration.login + "; Pwd=" + Configuration.password + ";";
            connexion = new MySqlConnection(constr);
            connexion.Open();
            RessourcesBdD.Recup();
        }
        catch (System.IO.IOException e)
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Erreur de connexion.";
            Debug.Log(e.ToString());
        }
    }

    public MySqlConnection getConnexion()
    {
        return connexion;
    }

    public void stopConnexion()
    {
        this.getConnexion().Close();
    }
	
	// Update is called once per frame
    private void OnApplicationQuit()
    {
        if (Joueur.IDJoueur != 0)
        {
            Joueur.transfertEnBase();
        }
        if (connexion != null && connexion.State.ToString() != "Close")
        {
            Debug.Log("Fermeture de la connexion");
            connexion.Close();
        }  
    }
}
