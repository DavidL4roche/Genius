﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact : MonoBehaviour {
    public int IDArtefact;
    public string NomArtefact;
    public Bonus SonBonus;
    public Artefact(int id, string name, int idbonus)
    {
        IDArtefact = id;
        NomArtefact = name;
        SonBonus = Bonus.trouverSonBonus(idbonus);
    }
    public Artefact (Artefact artefact)
    {
        IDArtefact =  artefact.IDArtefact;
        NomArtefact = artefact.NomArtefact;
        SonBonus = artefact.SonBonus;
    }
    public static Artefact trouverSonArtefact(int idarte)
    {
        foreach (Artefact artefact in RessourcesBdD.listeDesArtefacts)
        {
            if (idarte == artefact.IDArtefact)
            {
                return new Artefact(artefact);
            }
        }
        return null;
    }
}
