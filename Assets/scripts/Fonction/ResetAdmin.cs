using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAdmin : MonoBehaviour {

    public GameObject BoutonReset;
    
    private WWW download;

    void Start () {
		if (Joueur.IDJoueur != 43)
        {
            BoutonReset.SetActive(false);
        }
        else
        {
            BoutonReset.SetActive(true);
        }
	}
	
    // Permet de "reset" les données de l'utilisateur test
	public void Reset () {
        Debug.Log("Reset du compte test");
        string urlRessource = Configuration.url + "scripts/ResetAdmin.php";
        download = new WWW(urlRessource);

        // On recharge le jeu
        // On détruit tout les objets
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        // On "relance" le jeu
        SceneManager.LoadScene("Index1");
        ChargerPopup.Charger("Succes");
        MessageErreur.messageErreur = "Réinitialisation réussie. Veuillez relancer le jeu";
    }
}
