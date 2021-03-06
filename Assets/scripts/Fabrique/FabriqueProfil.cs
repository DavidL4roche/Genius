﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueProfil : MonoBehaviour {
    public Button ArtefactOrcus;
    public Button ArtefactIA;
    public Button ArtefactObjet;
    public Button ArtefactBoutique;
    public Button ArtefactSocial;
    public Button ArtefactDivertissement;

    public Text NomJoueur;

    public GameObject EcranTuto;

    public void Start()
    {
        try
        {
            // On vérifie que le joueur a fait le tuto Profil (3)
            StartCoroutine(Joueur.VerifierStatusTuto(3, EcranTuto));

            NomJoueur.text = Joueur.NomJoueur;
            if (RessourcesBdD.listeDesArtefactsJouables != null)
            {
                for (int i = 0; i < RessourcesBdD.listeDesArtefactsJouables.Length; ++i)
                {
                    switch (RessourcesBdD.listeDesArtefactsJouables[i].IDArtefact)
                    {
                        // Artéfact Orcus
                        case 1:
                            ArtefactOrcus.interactable = true;
                            break;
                        // Artéfact Boutique
                        case 2:
                            ArtefactBoutique.interactable = true;
                            break;
                        // Artéfact IA
                        case 3:
                            ArtefactIA.interactable = true;
                            break;
                        // Artéfact Objet
                        case 4:
                            ArtefactObjet.interactable = true;
                            break;
                        // Artéfact Divertissement
                        case 5:
                            ArtefactDivertissement.interactable = true;
                            break;
                        // Artéfact Social
                        case 6:
                            ArtefactSocial.interactable = true;
                            break;
                        default:
                            break;

                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            RessourcesBdD.ReloadGame();
        }
    }
}
