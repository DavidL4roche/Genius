using UnityEngine;
using MySql.Data.MySqlClient;

public class Connexion : MonoBehaviour {
    public static MySqlConnection connexion;
    // Use this for initialization
	void Start () {
        try
        {
            DontDestroyOnLoad(gameObject);
            string constr = "Server="+Configuration.host+"; Port=3306; Database="+Configuration.database+"; Uid="+Configuration.login+"; Pwd="+Configuration.password+";";
            connexion = new MySqlConnection(constr);
            connexion.Open();
            RessourcesBdD.Recup();
        }
        catch(System.IO.IOException e)
        {
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
            Debug.Log("Fermeture de la connexion, champion");
            connexion.Close();
        }  
    }
}
