using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargerFenetreSupp : MonoBehaviour {
    public void Charger(string Fenetre)
    {
        StopInteractionBG.interactionbg = false;
        //Debug.Log(StopInteractionBG.interactionbg);
        SceneManager.LoadScene(Fenetre, LoadSceneMode.Additive);
    }
}
