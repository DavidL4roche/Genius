using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificationPNJ : MonoBehaviour {
    public static int MissionChoisi;
    public void Cliquer()
    {
        MissionChoisi = Int32.Parse(gameObject.name.Substring(3));
    }
}
