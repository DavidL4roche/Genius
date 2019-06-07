using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueAide : MonoBehaviour {

    public Text NomTopic;
    public GameObject IDTopicGO;
    public GameObject TupleAide;
    GameObject instance;

	void Start () {
        int i = 0;
        foreach(Topic topic in RessourcesBdD.listeDesTopicsAide)
        {
            NomTopic.text = topic.TitreTopic;
            Text IDTopic = IDTopicGO.GetComponentInChildren<Text>();
            IDTopic.text = i.ToString(); //topic.IDTopic.ToString();

            instance = Instantiate(TupleAide, new Vector3(0.0F, 0.0F, 0.0F), TupleAide.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "Tuple " + (i + 1);
            ++i;
        }
    }
}
