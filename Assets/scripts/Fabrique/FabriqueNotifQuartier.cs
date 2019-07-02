using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueNotifQuartier : MonoBehaviour {

    public GameObject[] NotifQuartiers = new GameObject[7];

    Mission[] LesMissions;
    MissionDivertissement SonDivertissement;
    PNJPrésent SonPNJ;
    int nbMission = 0;
    int IDQuartier;
    bool continueNotifQuartier = true;

    void Start () {
        
	}

    public void Update()
    {
        if (RessourcesBdD.listeDesQuartiers != null && RessourcesBdD.continueMissionNotifQuartier && continueNotifQuartier)
        {
            for (int j = 1; j < RessourcesBdD.listeDesQuartiers.Length; j++)
            {
                int cpt = 0;
                Quartier quartier = RessourcesBdD.listeDesQuartiers[j];
                IDQuartier = quartier.IDQuartier;
                totalDeMissions();

                //Debug.Log("Quartier " + j);

                // On compte les missions faisables dans le quartier
                for (int i = 0; i < LesMissions.Length; ++i)
                {
                    Mission mission = LesMissions[i];

                    bool playing = true;

                    // On regarde si la mission est jouable pour le joueur
                    for (int k = 0; k < mission.CompétencesRequises.Length; ++k)
                    {
                        if (!verificationCompAvecJoueur(mission.CompétencesRequises[k].ID, mission.CompétencesRequises[k].Valeur))
                        {
                            playing = false;
                            break;
                        }
                    }

                    if (playing)
                    {
                        ++cpt;
                        //Debug.Log("Ajout cpt mission" + cpt);
                    }
                }

                // On compte le divertissement dans le quartier
                if (SonDivertissement.IDMissionD != 0)
                {
                    cpt += 1;
                    //Debug.Log("Ajout cpt divertissement" + cpt);
                }

                // On instancie un PNJ dans le quartier
                if (SonPNJ.SonPNJ.IDPNJ != 0)
                {
                    bool playing = true;

                    // On regarde si la mission est jouable pour le joueur
                    for (int k = 0; k < SonPNJ.SaMission.CompétencesRequises.Length; ++k)
                    {
                        if (!verificationCompAvecJoueur(SonPNJ.SaMission.CompétencesRequises[k].ID, SonPNJ.SaMission.CompétencesRequises[k].Valeur))
                        {
                            playing = false;
                            break;
                        }
                    }

                    if (playing)
                    {
                        ++cpt;
                        //Debug.Log("Ajout cpt pnj" + cpt);
                    }
                }

                NotifQuartiers[j - 1].SetActive(true);

                // On affiche le nombre de missions faisables dans la bulle de notif du quartier
                NotifQuartiers[j - 1].gameObject.GetComponentInChildren<Text>().text = cpt.ToString();

                if (cpt == 0)
                {
                    NotifQuartiers[j - 1].SetActive(false);
                }
            }

            continueNotifQuartier = false;
        }
    }

    // Crée le nombre de missions, divertissements et PNJ correspondants
    void totalDeMissions()
    {
        SonPNJ = new PNJPrésent(0);
        SonDivertissement = new MissionDivertissement(0, "", 1);
        nbMission = 0;

        // On calcule le nombre de missions du quartier
        for (int i = 0; i < RessourcesBdD.listeDesMissionsPrésentes.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesMissionsPrésentes[i].SonQuartier.IDQuartier)
            {
                ++nbMission;
            }
        }

        // On crée le nombre de missions correspondantes
        LesMissions = new Mission[nbMission];
        int y = 0;
        for (int i = 0; i < RessourcesBdD.listeDesMissionsPrésentes.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesMissionsPrésentes[i].SonQuartier.IDQuartier)
            {
                LesMissions[y] = new Mission(RessourcesBdD.listeDesMissionsPrésentes[i].SaMission);
                ++y;
            }
        }

        // On crée le nombre de divertissements correspondants
        for (int i = 0; i < RessourcesBdD.listeDesDivertissementsPrésents.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesDivertissementsPrésents[i].SonQuartier.IDQuartier)
            {
                SonDivertissement = RessourcesBdD.listeDesDivertissementsPrésents[i].SonDivertissement;
            }
        }

        // On crée le nombre de PNJ correspondants
        for (int i = 0; i < RessourcesBdD.listeDesPNJPrésents.Length; ++i)
        {
            if (IDQuartier == RessourcesBdD.listeDesPNJPrésents[i].SonPNJ.SonQuartier.IDQuartier)
            {
                SonPNJ = RessourcesBdD.listeDesPNJPrésents[i];
            }
        }
    }

    // Vérifie la compétence demandée avec celle du Joueur (valeur)
    public bool verificationCompAvecJoueur(int id, int valeur)
    {
        int k = 0;
        for (; k < RessourcesBdD.listeDesCompétences.Length; ++k)
        {
            if (id == RessourcesBdD.listeDesCompétences[k].ID)
            {
                break;
            }
        }

        //Debug.Log("Valeur Joueur : " + Joueur.MesValeursCompetences[k]);

        if (Joueur.MesValeursCompetences[k] >= valeur)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
