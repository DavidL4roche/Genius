using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifActionSocial : MonoBehaviour {
    public static string ActionSocialeChoisie;

    public void Cliquer()
    {
        ActionSocialeChoisie = gameObject.transform.name;
    }
}
