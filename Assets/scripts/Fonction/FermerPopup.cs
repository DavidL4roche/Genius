using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FermerPopup : MonoBehaviour
{
    public void Fermer(string Fenetre)
    {
        StopInteractionSupp.interactionsupp = true;
        SceneManager.UnloadSceneAsync(Fenetre);
    }
}
