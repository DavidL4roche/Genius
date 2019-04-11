using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificationCompetence : MonoBehaviour {
    public static int CompetenceChoisie;
    public void Cliquer()
    {
        Text nomCompetence = gameObject.GetComponentInChildren<Text>();
        CompetenceChoisie = System.Int32.Parse(nomCompetence.text)-1;
        // TODO : Vérifier ce qui est faux (non correspondance avec fenêtre Prérequis)

        /*
        Debug.Log(gameObject.name);
        char[] nomCompetence = gameObject.name.ToCharArray();
        CompetenceChoisie = (int)System.Char.GetNumericValue(nomCompetence[(nomCompetence.Length - 1)]);
        Debug.Log("Nom compétence : " + nomCompetence);
        Debug.Log("Compétence choisie : " + CompetenceChoisie);
        */
    }
}
