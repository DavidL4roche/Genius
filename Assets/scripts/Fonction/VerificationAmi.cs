using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationAmi : MonoBehaviour {
    public static int AmiChoisi;
    public void Cliquer()
    {
        char[] nomAmi= gameObject.name.ToCharArray();
        AmiChoisi = (int)System.Char.GetNumericValue(nomAmi[(nomAmi.Length - 1)]);
        Debug.Log("Vous avez choisi l'ami numéro " + AmiChoisi);
    }
}
