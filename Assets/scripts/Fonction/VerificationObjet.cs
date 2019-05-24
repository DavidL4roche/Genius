using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificationObjet : MonoBehaviour {

    public static int ObjetChoisi;
    public void Cliquer()
    {
        Text idObjet = gameObject.GetComponentInChildren<Text>();
        ObjetChoisi = System.Int32.Parse(idObjet.text);
        //Debug.Log(gameObject.name);
        /*
        char[] nomExam = gameObject.name.ToCharArray();
        ExamChoisi = (int)System.Char.GetNumericValue(nomExam[(nomExam.Length - 1)]);
        */
        //--ExamChoisi;
    }
}
