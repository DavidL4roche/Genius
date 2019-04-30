using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificationExamenCompetence : MonoBehaviour {

    public Text IDExamen;
    public Text IDCompetence;

    public void Cliquer()
    {
        VerificationExamen.ExamChoisi = System.Int32.Parse(IDExamen.text);
        VerificationCompetence.CompetenceChoisie = System.Int32.Parse(IDCompetence.text);
    }
}
