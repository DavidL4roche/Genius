using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationMission : MonoBehaviour {
    public static int MissionChoisi;
    public void Cliquer()
    {
        //Debug.Log(gameObject.name);
        char[] nomMission = gameObject.name.ToCharArray();
        MissionChoisi = (int)System.Char.GetNumericValue(nomMission[(nomMission.Length - 1)]);
    }
}
