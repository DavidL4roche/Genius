using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalerCamera : MonoBehaviour {

    public Camera cameraJeu;
    public float valueX = 0;
    public float valueY = 0;

	public void ChangerPositionCamera() {
        cameraJeu.transform.position = new Vector3(valueX, valueY, -380);
	}
}
