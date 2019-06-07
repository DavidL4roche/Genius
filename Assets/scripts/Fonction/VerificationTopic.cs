using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificationTopic : MonoBehaviour {
    public static int IDTopic;
    public void Cliquer()
    {
        Text textIDTopic = gameObject.GetComponentInChildren<Text>();
        IDTopic = System.Int32.Parse(textIDTopic.text);
    }
}
