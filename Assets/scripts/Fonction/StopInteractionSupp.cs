using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInteractionSupp : MonoBehaviour
{
    public static bool interactionsupp = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (interactionsupp == true)
        {
            CanvasGroup myCanGrp = GetComponent<CanvasGroup>();
            myCanGrp.interactable = true;
        }
        else
        {
            CanvasGroup myCanGrp = GetComponent<CanvasGroup>();
            myCanGrp.interactable = false;
        }
    }
}
