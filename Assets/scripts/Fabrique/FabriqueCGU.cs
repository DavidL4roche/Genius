using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueCGU : MonoBehaviour {

    public TextMeshProUGUI zoneTexte;

    private void Start()
    {
        try
        {
            TextReader reader;
            string fileName = "#AUTRES/CGU.txt";
            reader = new StreamReader(fileName, Encoding.Default);
            string result = reader.ReadToEnd();
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
