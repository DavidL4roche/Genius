using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueTopic : MonoBehaviour {

    Topic topic = RessourcesBdD.listeDesTopicsAide[VerificationTopic.IDTopic];

    public Text TitreTopic;
    public Text CorpsTopic;

    private void Start()
    {
        TitreTopic.text = topic.TitreTopic;
        CorpsTopic.text = topic.CorpsTopic;
    }
}
