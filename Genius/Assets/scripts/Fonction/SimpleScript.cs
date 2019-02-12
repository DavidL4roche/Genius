using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleScript : MonoBehaviour {

	// Use this for initialization
	public void Start () {
		Debug.Log("SIMPLE SCRIPT ! Je suis dans la scène : " + SceneManager.GetActiveScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
