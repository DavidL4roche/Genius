using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueCGU : MonoBehaviour {

    public Text zoneTexte;

    private void Start()
    {
        try
        {
            TextReader reader;
            string fileName = "#AUTRES/CGU.txt";
            reader = new StreamReader(fileName);
            string result = reader.ReadToEnd();
            Debug.Log(result);
            zoneTexte.text = result;
            reader.Close();
        }

        catch (IOException e)
        {
            Debug.Log("Le fichier ne peut être lu :");
            Debug.Log(e.Message);
        }
    }
}
