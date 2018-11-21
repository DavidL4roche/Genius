using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargerPopup : MonoBehaviour
{
    public static void Charger(string Fenetre)
    {
        StopInteractionSupp.interactionsupp = false;
        SceneManager.LoadScene(Fenetre, LoadSceneMode.Additive);
    }
    public void ChargerNonStatique(string Fenetre)
    {
        StopInteractionSupp.interactionsupp = false;
        SceneManager.LoadScene(Fenetre, LoadSceneMode.Additive);
    }
}