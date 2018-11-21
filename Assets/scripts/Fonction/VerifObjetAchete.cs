using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifObjetAchete : MonoBehaviour {
    public static int ObjetAchete;
    public void Cliquer()
    {
        //Debug.Log(gameObject.name);
        char[] nomObjet = gameObject.name.ToCharArray();
        ObjetAchete = (int)System.Char.GetNumericValue(nomObjet[(nomObjet.Length - 1)]);
    }
}
