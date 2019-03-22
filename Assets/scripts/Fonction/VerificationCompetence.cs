using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationCompetence : MonoBehaviour {
    public static int CompetenceChoisie;
    public void Cliquer()
    {
        //Debug.Log(gameObject.name);
        char[] nomCompetence = gameObject.name.ToCharArray();
        CompetenceChoisie = (int)System.Char.GetNumericValue(nomCompetence[(nomCompetence.Length - 1)]);
    }
}
