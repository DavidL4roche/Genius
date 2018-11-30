using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpErreur : MonoBehaviour {

    public string typeErreur;
    public string erreur;

	public void popUpErreur() {
        
        switch(typeErreur)
        {
            case "Succes":
                ChargerPopup.Charger("Succes");
                MessageErreur.messageErreur = erreur;
                break;
            default:
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = erreur;
                break;
        }
    }
}
