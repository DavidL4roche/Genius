using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageErreur : MonoBehaviour {
    public Text TextErreur;
    public static string messageErreur;
    public void Start()
    {
        TextErreur.text = messageErreur;
    }
}
