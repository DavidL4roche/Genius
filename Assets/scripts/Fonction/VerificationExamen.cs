using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationExamen : MonoBehaviour {
    public static int ExamChoisi;
    public void Cliquer()
    {
        //Debug.Log(gameObject.name);
        char[] nomExam = gameObject.name.ToCharArray();
        ExamChoisi = (int)System.Char.GetNumericValue(nomExam[(nomExam.Length - 1)]);
        --ExamChoisi;
        Debug.Log(ExamChoisi);
    }
}
