using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topic : MonoBehaviour {

    public int IDTopic;
    public string TitreTopic;
    public string CorpsTopic;

    public Topic(int id, string titre, string corps)
    {
        IDTopic = id;
        TitreTopic = titre;
        CorpsTopic = corps;
    }

    public void toString()
    {
        Debug.Log("Topic " + IDTopic + "(" + TitreTopic + ")");
    }
}
