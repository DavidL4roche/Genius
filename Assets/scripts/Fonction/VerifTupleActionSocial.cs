using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifTupleActionSocial : MonoBehaviour {
    public static int TupleChoisieActionSocial;
    public void Cliquer()
    {
         char[] nomtuple = gameObject.name.ToCharArray();
         TupleChoisieActionSocial = (int)System.Char.GetNumericValue(nomtuple[(nomtuple.Length - 1)]);
    }
}
