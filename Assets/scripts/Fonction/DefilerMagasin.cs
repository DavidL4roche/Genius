using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefilerMagasin : MonoBehaviour {

    public HorizontalLayoutGroup HorizontalLayout;
    public int decalage = 348;

    public void Precedent()
    {
        HorizontalLayout.transform.position = new Vector3(HorizontalLayout.transform.position.x + decalage, HorizontalLayout.transform.position.y, HorizontalLayout.transform.position.z);
    }

    public void Suivant()
    {
        HorizontalLayout.transform.position = new Vector3(HorizontalLayout.transform.position.x - decalage, HorizontalLayout.transform.position.y, HorizontalLayout.transform.position.z);
    }

    public float getSuivantMultiple(float x)
    {
        float multiple = 1056.35F; // Taille de l'Horizontal Bar
        while (x > multiple)
        {
            multiple -= decalage;
        }

        return multiple;
    }
}
