using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationGameObject : MonoBehaviour {

    public static string NomGameObject;
    public void Cliquer () {
        NomGameObject = gameObject.name;
	}
}
