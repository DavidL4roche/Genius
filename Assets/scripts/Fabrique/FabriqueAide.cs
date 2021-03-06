﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueAide : MonoBehaviour {

    public Text NomTopic;
    public GameObject IDTopicGO;
    public GameObject TupleAide;

    public Text NomTuto;
    public GameObject TupleTuto;

    GameObject instance;

	void Start () {

        NomTuto.text = "Tutoriel de début";

        instance = Instantiate(TupleTuto, new Vector3(0.0F, 0.0F, 0.0F), TupleTuto.transform.rotation);
        instance.transform.parent = GameObject.Find("VerticalLayout").transform;
        instance.transform.name = "TupleTuto";

        int i = 0;
        foreach (Topic topic in RessourcesBdD.listeDesTopicsAide)
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
