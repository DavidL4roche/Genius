using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificationExamen : MonoBehaviour {
    public static int ExamChoisi;
    public void Cliquer()
    {
        Text idExam = gameObject.GetComponentInChildren<Text>();
        ExamChoisi = System.Int32.Parse(idExam.text);
        //Debug.Log(gameObject.name);
        /*
        char[] nomExam = gameObject.name.ToCharArray();
        ExamChoisi = (int)System.Char.GetNumericValue(nomExam[(nomExam.Length - 1)]);
        */
        //--ExamChoisi;
    }
}
