using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInteractionBG : MonoBehaviour {
    public static bool interactionbg = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(interactionbg==true)
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
