﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueDivertissement : MonoBehaviour {
    MissionDivertissement mr = SpawnerMission.SonDivertissement;

    public Text NomDivert;
    public RawImage ImageRang;
    public Text GainDivertissement;

    GameObject instance;
    private void Start()
    {
        Gain.calculDesGains(mr);
        NomDivert.text = mr.NomDivertissement;
        ImageRang.texture = mr.SonRang.texture;
        blockdesgains();
    }
    public void blockdesgains()
    {
        //ImageTuple.color = new Color(1F, 1F, 1F, 1F);
        for (int i = 0; i < mr.SesGains.Length; ++i)
        {
            //Debug.Log("Gain " + i + " : " + mr.SesGains[i].NomGain + "(" + mr.SesGains[i].ValeurDuGain.ToString() + ")");
            if(mr.SesGains[i].NomGain == "Divertissement")
            {
                GainDivertissement.text = mr.SesGains[i].ValeurDuGain.ToString();
            }
        }
    }
}
