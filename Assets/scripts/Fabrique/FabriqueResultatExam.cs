using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatExam : MonoBehaviour {
    Examen examen = RessourcesBdD.listeDesExamens[VerificationExamen.ExamChoisi];

    public Text Diplome;
    public Text perteDivert;
    public Text perteSocial;

    GameObject instance;
    public void Start()
    {
        //mission.voirRessources();
        AGagner();
        APerdu();
        blockdesprerequis();
        StartCoroutine(EnvoiExamenPasseEnBase());
        StartCoroutine(Joueur.transfertEnBase());
    }
    public void blockdesprerequis()
    {
        for (int i = 0; i < examen.SesGains.Length; ++i)
        {
            if (examen.SesGains[i].NomGain == "Diplome")
            {
                Diplome.text = examen.NomExamen;
            }
            else if (examen.SesGains[i].NomGain == "Compétence")
            {
                //Debug.Log("Compétence !");
                //Diplome.text = "+" + examen.SesGains[i].ValeurDuGain.ToString();
            }
        }
        for (int i = 0; i < examen.SesPertes.Length; ++i)
        {
            if (examen.SesPertes[i].NomPerte == "Divertissement")
            {
                perteDivert.text = "-" + examen.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
            }
            else if (examen.SesPertes[i].NomPerte == "Social")
            {
                perteSocial.text = "-" + examen.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
            }
        }
    }
    void AGagner()
    {
        for (int i = 0; i < examen.SesGains.Length; ++i)
        {
            if (examen.SesGains[i].ValeurDuGain != 0)
            {
                switch (examen.SesGains[i].NomGain)
                {
                    case "Orcus":
                    case "IA":
                    case "Divertissement":
                    case "Social":
                        gainRess(examen.SesGains[i].NomGain, examen.SesGains[i].ValeurDuGain);
                        break;
                    case "Compétence":
                        gainComp(examen.SesGains[i].ValeurDuGain);
                        break;
                    case "Diplome":
                        gainDiplome();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void gainComp(int valeur)
    {
        for (int i = 0; i < examen.CompétencesRequises.Length; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesCompétences.Length; ++j)
            {
                if (examen.CompétencesRequises[i].ID == RessourcesBdD.listeDesCompétences[j].ID)
                {
                    if ((Joueur.MesValeursCompetences[j] + valeur) > 100)
                    {
                        Joueur.MesValeursCompetences[j] = 100;
                    }
                    else
                    {
                        Joueur.MesValeursCompetences[j] += valeur;
                        //Debug.Log(Joueur.MesValeursCompetences[j]+ " gain de compétences");
                    }
                }
            }
        }
    }
    void gainRess(string NomRess, int valeur)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (NomRess == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                Joueur.MesRessources[i] = Joueur.MesRessources[i] + valeur;
                if ((NomRess == "Social" || NomRess == "Divertissement") && Joueur.MesRessources[i] > 100)
                {
                    Joueur.MesRessources[i] = 100;
                }
            }
        }
    }
    void gainDiplome()
    {
        for(int i = 0; i<RessourcesBdD.listeDesDiplomes.Length; ++i)
        {
            if (examen.LeDiplome.IDDiplome == RessourcesBdD.listeDesDiplomes[i].IDDiplome)
            {
                Joueur.MesDiplomes[i] = true;
                break;
            }
        }
    }
    void APerdu()
    {
        //Ressources
        for (int i = 0; i < examen.SesPertes.Length; ++i)
        {
            if (examen.SesPertes[i].ValeurDeLaPerte != 0)
            {
                for (int j = 0; j < RessourcesBdD.listeDesRessources.Length; ++j)
                {
                    if (RessourcesBdD.listeDesRessources[j].NomRessource == examen.SesPertes[i].NomPerte)
                    {
                        Joueur.MesRessources[j] = Joueur.MesRessources[j] - examen.SesPertes[i].ValeurDeLaPerte;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
    public IEnumerator EnvoiExamenPasseEnBase()
    {
        string urlComp = Configuration.url + "scripts/ExamenEffectue.php?idJoueur=" + Joueur.IDJoueur + "&idExamen=" + examen.LeDiplome.IDDiplome;

        WWW dl = new WWW(urlComp);
        yield return dl;
    }
}
