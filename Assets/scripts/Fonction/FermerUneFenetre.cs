using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FermerUneFenetre : MonoBehaviour {
    public void Fermer(string Fenetre)
    {
        StopInteractionBG.interactionbg = true;
        //Debug.Log(StopInteractionBG.interactionbg);
        SceneManager.UnloadSceneAsync(Fenetre);
    }
}
