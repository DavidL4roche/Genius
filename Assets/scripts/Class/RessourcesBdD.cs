using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class RessourcesBdD : MonoBehaviour
{
    public static Lieu[] listeDesLieux;
    public static Compétence[] listeDesCompétences;
    public static Objet[] listeDesObjets;
    public static Rang[] listeDesRangs;
    public static Ressource[] listeDesRessources;
    public static Diplome[] listeDesDiplomes;
    public static Trophee[] listeDesTrophees;
    public static Examen[] listeDesExamens;
    public static ExamenPrésent[] listeDesExamensPrésents;
    public static int[] listeDesExamensNonJouables;
    public static PNJ[] listeDesPNJ;
    public static Entreprise[] listeDesEntreprises;
    public static Mission[] listeDesMissions;
    public static MissionPrésente[] listeDesMissionsPrésentes;
    public static MissionDivertissement[] listeDesDivertissements;
    public static MissionDivertissementPrésente[] listeDesDivertissementsPrésents;
    public static Quartier[] listeDesQuartiers;
    public static Bonus[] listeDesBonus;
    public static Duree[] listeDesDurees;
    public static Gain[] listeDesGains;
    public static Perte[] listeDesPertes;
    public static Artefact[] listeDesArtefacts;
    public static Artefact[] listeDesArtefactsJouables;
    public static PNJPrésent[] listeDesPNJPrésents;
    public static ObjetPrésent[] LeMagasin;
    public static Topic[] listeDesTopicsAide;

    public static AutreJoueur[] listeDesJoueurs;
    public static bool stoprecupmission = false;

    static public RessourcesBdD instance;

    public static WWW download;
    public static string monJson;
    public static JSONNode monNode;
    public static string url = Configuration.url + "scripts/scriptsRessources/";

    // Permet de gérer les StartCoroutine dans les fonctions static
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    public static void Recup()
    {
        instance.StartCoroutine(RecupLieu());
        instance.StartCoroutine(RecupGain());
        instance.StartCoroutine(RecupPerte());
        instance.StartCoroutine(RecupBonus());
        instance.StartCoroutine(RecupDuree());
        instance.StartCoroutine(RecupRang());
        instance.StartCoroutine(RecupQuartier());
        RecupDivert();
        RecupComp();
        RecupRess();
        instance.StartCoroutine(RecupObjet());
        RecupDiplome();
        instance.StartCoroutine(RecupTrophee());
        instance.StartCoroutine(RecupExam());
        instance.StartCoroutine(RecupEntreprise());
        RecupMission();
        instance.StartCoroutine(RecupArtefact());
        RecupPNJ();
        RecupTopicsAide();

        ChargerPopup.Charger("Succes");
        MessageErreur.messageErreur = "Récupération des données en base réussie.";
    }

    // Fonction de généralisation pour lecture script PHP
    public static bool VerifierStatusScript(WWW dl)
    {
        if ((!string.IsNullOrEmpty(dl.error)))
        {
            print("Error downloading: " + dl.error);
            return false;
        }
        else
        {
            monJson = dl.text;
            monNode = JSON.Parse(monJson);

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'une réponse est renvoyé)
            string result = monNode["result"].Value;

            // Si le résultat est faux, on affiche l'erreur et on relance le jeu : TODO !!!!!!!!!!!
            if (result.ToLower() == "false")
            {
                // On redirige vers la connexion
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
                return false;
            }
            else
            {
                // Le JSON est bon et valide
                return true;
            }
        }
    }

    // Fonction de généralisation pour lecture script PHP
    public static JSONNode RenvoiJSONScript(WWW dl)
    {
        monJson = dl.text;
        monNode = JSON.Parse(monJson);
        return monNode;
    }

    // Récupération des Lieux
    public static IEnumerator RecupLieu()
    {
        string urlComp = url + "RecupLieu.php";
        WWW dl = new WWW(urlComp);
        yield return dl;
        
        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesLieux = new Lieu[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesLieux[i] = new Lieu((int)Node["msg"][i]["IDPlace"], Node["msg"][i]["PlaceName"].Value);
            }
        }

        for (int i = 0; i < listeDesLieux.Length; i++)
        {
            listeDesLieux[i].toString();
        }
    }

    // Récupération des Gains
    private static IEnumerator RecupGain()
    {
        string urlComp = url + "RecupGain.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesGains = new Gain[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesGains[i] = new Gain((int)Node["msg"][i]["IDGain"], Node["msg"][i]["GainName"].Value);
            }
        }

        for (int i = 0; i < listeDesGains.Length; i++)
        {
            listeDesGains[i].toString();
        }
    }

    // Récupération des Pertes
    private static IEnumerator RecupPerte()
    {
        string urlComp = url + "RecupPerte.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesPertes = new Perte[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesPertes[i] = new Perte((int)Node["msg"][i]["IDLoss"], Node["msg"][i]["LossName"].Value);
            }
        }

        for (int i = 0; i < listeDesPertes.Length; i++)
        {
            listeDesPertes[i].toString();
        }
    }

    // Récupération des Bonus
    private static IEnumerator RecupBonus()
    {
        string urlComp = url + "RecupBonus.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesBonus = new Bonus[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesBonus[i] = new Bonus((int)Node["msg"][i]["IDBonus"], Node["msg"][i]["BonusName"].Value);
            }
        }

        for (int i = 0; i < listeDesBonus.Length; i++)
        {
            listeDesBonus[i].toString();
        }
    }

    // Récupération des Durées
    private static IEnumerator RecupDuree()
    {
        string urlComp = url + "RecupDuree.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesDurees = new Duree[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesDurees[i] = new Duree((int)Node["msg"][i]["IDDuration"], Node["msg"][i]["DurationName"].Value, (int)Node["msg"][i]["DurationValue"]);
            }
        }

        for (int i = 0; i < listeDesDurees.Length; i++)
        {
            listeDesDurees[i].toString();
        }
    }

    // Récupération des Rangs
    private static IEnumerator RecupRang()
    {
        string urlComp = url + "RecupRang.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesRangs = new Rang[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesRangs[i] = new Rang((int)Node["msg"][i]["IDRank"], Node["msg"][i]["RankName"].Value);
            }
        }

        for (int i = 0; i < listeDesRangs.Length; i++)
        {
            listeDesRangs[i].toString();
        }
    }

    // Récupération des Quartiers
    private static IEnumerator RecupQuartier()
    {
        string urlComp = url + "RecupQuartier.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesQuartiers = new Quartier[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesQuartiers[i] = new Quartier((int)Node["msg"][i]["IDDistrict"], Node["msg"][i]["DistrictName"].Value);
            }
        }

        for (int i = 0; i < listeDesQuartiers.Length; i++)
        {
            listeDesQuartiers[i].toString();
        }
    }

    // Récupération des Objets
    private static IEnumerator RecupObjet()
    {
        string urlComp = url + "RecupObjet.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesObjets = new Objet[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesObjets[i] = new Objet((int)Node["msg"][i]["IDItem"], Node["msg"][i]["ItemName"].Value, (int)Node["msg"][i]["IDRank"],
                                              (int)Node["msg"][i]["IDBonus"], (int)Node["msg"][i]["BonusGain"]);
            }
            Joueur.MesObjets = new int[listeDesObjets.Length];
        }

        for (int i=0; i< listeDesObjets.Length; i++)
        {
            listeDesObjets[i].toString();
        }
    }

    // Récupération des Trophées
    private static IEnumerator RecupTrophee()
    {
        string urlComp = url + "RecupTrophee.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesTrophees = new Trophee[Node["msg"].Count];
            Joueur.MesTrophees = new bool[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesTrophees[i] = new Trophee((int)Node["msg"][i]["IDTrophy"], (int)Node["msg"][i]["IDRank"], Node["msg"][i]["Description"].Value);
            }
        }

        for (int i = 0; i < listeDesTrophees.Length; i++)
        {
            listeDesTrophees[i].toString();
        }
    }

    // Récupération des Examens
    private static IEnumerator RecupExam()
    {
        string urlComp = url + "RecupExam.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesExamens = new Examen[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesExamens[i] = new Examen((int)Node["msg"][i]["IDExam"], Node["msg"][i]["ExamName"].Value, (int)Node["msg"][i]["IDDiplom"],
                                                (int)Node["msg"][i]["IDSkillSlot1"], (int)Node["msg"][i]["IDSkillSlot2"], (int)Node["msg"][i]["IDSkillSlot3"],
                                                (int)Node["msg"][i]["IDSkillSlot4"], (int)Node["msg"][i]["IDSkillSlot5"], (int)Node["msg"][i]["IDRank"]);
            }
        }

        for (int i = 0; i < listeDesExamens.Length; i++)
        {
            listeDesExamens[i].toString();
        }
    }

    // Récupération des Entreprises
    private static IEnumerator RecupEntreprise()
    {
        string urlComp = url + "RecupEntreprise.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesEntreprises = new Entreprise[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesEntreprises[i] = new Entreprise((int)Node["msg"][i]["IDCompany"], Node["msg"][i]["CompanyName"].Value, (int)Node["msg"][i]["Size"]);
            }
        }

        for (int i = 0; i < listeDesEntreprises.Length; i++)
        {
            listeDesEntreprises[i].toString();
        }

        // SES EMPLACEMENTS
        for (int i = 0; i < listeDesEntreprises.Length; ++i)
        {
            urlComp = url + "RecupEmpEntreprise.php?id=" + listeDesEntreprises[i].IDEntreprise;

            dl = new WWW(urlComp);
            yield return dl;

            if (VerifierStatusScript(dl))
            {
                JSONNode Node = RenvoiJSONScript(dl);
                if (Node["msg"].Count > 1)
                {
                    listeDesEntreprises[i].SesEmplacements = new Quartier[Node["msg"].Count];
                    for (int j = 0; j < Node["msg"].Count; ++j)
                    {
                        listeDesEntreprises[i].SesEmplacements[j] = Quartier.trouverSonQuartier((int)Node["msg"][j]["IDDistrict"]);
                    }
                }
                else if (Node["msg"].Count == 1)
                {
                    listeDesEntreprises[i].SesEmplacements = new Quartier[Node["msg"].Count];
                    for (int j = 0; j < Node["msg"].Count; ++j)
                    {
                        listeDesEntreprises[i].SesEmplacements[0] = Quartier.trouverSonQuartier((int)Node["msg"][j]["IDDistrict"]);
                    }
                }
                else
                {
                    listeDesEntreprises[i].SesEmplacements = new Quartier[1];
                    listeDesEntreprises[i].SesEmplacements[0] = new Quartier(0, "0");
                }
            }

            Debug.Log("Emplacements des entreprises");
            for (int j = 0; j < listeDesEntreprises[i].SesEmplacements.Length; j++)
            {
                listeDesEntreprises[i].SesEmplacements[j].toString();
            }

            /*
            int total = 0;
            requete = "SELECT Count(*) AS Total,IDDistrict from association_company_district WHERE IDCompany=" + listeDesEntreprises[i].IDEntreprise + ";";
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            while (lien.Read())
            {
                total = Int32.Parse(lien["Total"].ToString());
            }
            lien.Close();
            if (total > 1)
            {
                listeDesEntreprises[i].SesEmplacements = new Quartier[total];
                for (int j = 0; j < total;)
                {
                    requete = "SELECT * from association_company_district WHERE IDCompany=" + listeDesEntreprises[i].IDEntreprise + ";";
                    commande = new MySqlCommand(requete, Connexion.connexion);
                    lien = commande.ExecuteReader();
                    while (lien.Read())
                    {
                        listeDesEntreprises[i].SesEmplacements[j] = Quartier.trouverSonQuartier((int)lien["IDDistrict"]);
                        ++j;
                    }
                    lien.Close();
                }
            }
            else if (total == 1)
            {
                listeDesEntreprises[i].SesEmplacements = new Quartier[total];
                requete = "SELECT * from association_company_district WHERE IDCompany=" + listeDesEntreprises[i].IDEntreprise + ";";
                commande = new MySqlCommand(requete, Connexion.connexion);
                lien = commande.ExecuteReader();
                while (lien.Read())
                {
                    listeDesEntreprises[i].SesEmplacements[0] = Quartier.trouverSonQuartier((int)lien["IDDistrict"]);
                }
                lien.Close();
            }
            else
            {
                listeDesEntreprises[i].SesEmplacements = new Quartier[1];
                listeDesEntreprises[i].SesEmplacements[0] = new Quartier(0, "0");
            }
            */

            //Ses spécialités



            /*
            total = 0;
            requete = "SELECT Count(*) AS Total,IDDistrict from company_specialization WHERE IDCompany=" + listeDesEntreprises[i].IDEntreprise + ";";
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            while (lien.Read())
            {
                total = Int32.Parse(lien["Total"].ToString());
            }
            lien.Close();
            if (total > 1)
            {
                listeDesEntreprises[i].SesSpécialisations = new Quartier[total];
                for (int j = 0; j < total;)
                {
                    requete = "SELECT * from company_specialization WHERE IDCompany=" + listeDesEntreprises[i].IDEntreprise + ";";
                    commande = new MySqlCommand(requete, Connexion.connexion);
                    lien = commande.ExecuteReader();
                    while (lien.Read())
                    {
                        listeDesEntreprises[i].SesSpécialisations[j] = Quartier.trouverSonQuartier((int)lien["IDDistrict"]);
                        ++j;
                    }
                    lien.Close();
                }
            }
            else if (total == 1)
            {
                listeDesEntreprises[i].SesSpécialisations = new Quartier[total];
                requete = "SELECT * from company_specialization WHERE IDCompany=" + listeDesEntreprises[i].IDEntreprise + ";";
                commande = new MySqlCommand(requete, Connexion.connexion);
                lien = commande.ExecuteReader();
                while (lien.Read())
                {
                    listeDesEntreprises[i].SesSpécialisations[0] = Quartier.trouverSonQuartier((int)lien["IDDistrict"]);
                }
                lien.Close();
            }
            else
            {
                listeDesEntreprises[i].SesSpécialisations = new Quartier[1];
                listeDesEntreprises[i].SesSpécialisations[0] = new Quartier(0, "0");
            }
            */
        }
    }

    // Récupération des Artéfacts
    private static IEnumerator RecupArtefact()
    {
        string urlComp = url + "RecupArtefact.php";

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            listeDesArtefacts = new Artefact[Node["msg"].Count];
            Joueur.MesArtefacts = new bool[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                listeDesArtefacts[i] = new Artefact((int)Node["msg"][i]["IDArtefact"], Node["msg"][i]["ArtefactName"].Value, (int)Node["msg"][i]["IDBonus"]);
            }
        }

        for (int i = 0; i < listeDesArtefacts.Length; i++)
        {
            listeDesArtefacts[i].toString();
        }
    }

    // Récupération des Objets du Magasin
    public static IEnumerator RecupObjetMagasin()
    {
        string urlComp = url + "RecupObjetMagasin.php?id=" + Joueur.IDJoueur;
        Debug.Log(urlComp);

        WWW dl = new WWW(urlComp);
        yield return dl;

        if (VerifierStatusScript(dl))
        {
            JSONNode Node = RenvoiJSONScript(dl);
            LeMagasin = new ObjetPrésent[Node["msg"].Count];
            for (int i = 0; i < Node["msg"].Count; ++i)
            {
                LeMagasin[i] = new ObjetPrésent((int)Node["msg"][i]["IDItem"]);
            }
        }

        for (int i=0; i< LeMagasin.Length; i++)
        {
            Debug.Log(LeMagasin[i].SonObjet.Nom);
        }
    }

    public static void RecupArtefactJouable()
    {
        string requete = "SELECT count(*) AS Total, IDArtefact from artefact WHERE IDArtefact NOT IN(SELECT IDArtefact FROM artefact_used WHERE IDPCharacter=" + Joueur.IDJoueur + ") AND IDArtefact IN(SELECT IDArtefact from artefact_pc WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesArtefactsJouables = new Artefact[(int)total];
        }
        lien.Close();
        requete = "SELECT * from artefact WHERE IDArtefact NOT IN(SELECT IDArtefact FROM artefact_used WHERE IDPCharacter=" + Joueur.IDJoueur + ") AND IDArtefact IN(SELECT IDArtefact from artefact_pc WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesArtefactsJouables.Length;)
        {
            while (lien.Read())
            {
                listeDesArtefactsJouables[i] = new Artefact((int)lien["IDArtefact"], lien["ArtefactName"].ToString(), (int)lien["IDBonus"]);
                ++i;
            }
        }
        lien.Close();
    }

    private static void RecupPNJ()
    {
        string requete = "SELECT count(*) AS Total, IDNPCharacter from np_character";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesPNJ = new PNJ[(int)total];
        }
        lien.Close();
        requete = "SELECT * from np_character";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesPNJ.Length;)
        {
            while (lien.Read())
            {
                listeDesPNJ[i] = new PNJ((int)lien["IDNPCharacter"], lien["NPCName"].ToString(), (int)lien["IDArtefact"]);
                ++i;
            }
        }
        lien.Close();
        for (int i = 0; i < listeDesPNJ.Length; ++i)
        {
            requete = "SELECT * from association_district_npc WHERE IDNPCharacter=" + listeDesPNJ[i].IDPNJ + ";";
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            while (lien.Read())
            {
                listeDesPNJ[i].associerSonQuartier((int)lien["IDDistrict"]);
            }
            lien.Close();
        }
    }

    private static void RecupDivert()
    {
        string requete = "SELECT count(*) AS Total, IDEntertainment from entertainment";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesDivertissements = new MissionDivertissement[(int)total];
        }
        lien.Close();
        requete = "SELECT * from entertainment";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesDivertissements.Length;)
        {
            while (lien.Read())
            {
                listeDesDivertissements[i] = new MissionDivertissement((int)lien["IDEntertainment"], lien["EntertainmentName"].ToString(), (int)lien["IDRank"]);
                ++i;
            }
        }
        lien.Close();
    }

    private static void RecupComp()
    {
        string requete = "SELECT count(*) AS Total, IDSkill from skill";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesCompétences = new Compétence[(int)total];
            Joueur.MesValeursCompetences = new int[(int)total];
        }
        lien.Close();
        requete = "SELECT * from skill";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesCompétences.Length;)
        {
            while (lien.Read())
            {
                listeDesCompétences[i] = new Compétence((int)lien["IDSkill"], lien["SkillName"].ToString(), lien["SkillDescription"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    private static void RecupRess()
    {
        string requete = "SELECT count(*) AS Total, IDRessource from ressource";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesRessources = new Ressource[(int)total];
            Joueur.MesRessources = new int[(int)total];
        }
        lien.Close();
        requete = "SELECT * from ressource";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesRessources.Length;)
        {
            while (lien.Read())
            {
                listeDesRessources[i] = new Ressource((int)lien["IDRessource"], lien["RessourceName"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    static void RecupDiplome()
    {
        string requete = "SELECT count(*) AS Total, IDDiplom from diplom";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesDiplomes = new Diplome[(int)total];
            Joueur.MesDiplomes = new bool[(int)total];
        }
        lien.Close();
        requete = "SELECT * from diplom";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesDiplomes.Length;)
        {
            while (lien.Read())
            {
                listeDesDiplomes[i] = new Diplome((int)lien["IDDiplom"], lien["DiplomName"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    
    static void RecupMission()
    {
        string requete = "SELECT count(*) AS Total, IDMission from mission";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesMissions = new Mission[(int)total];
        }
        lien.Close();
        requete = "SELECT * from mission";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesMissions.Length;)
        {
            while (lien.Read())
            {
                int[] tableauidskill = new int[5];
                for (int j = 1; j < 6; ++j)
                { 
                    string nomdansbdd = "IDSkill" + j;
                    //Debug.Log(lien[nomdansbdd].ToString());
                    if (lien[nomdansbdd].ToString() != "" && lien[nomdansbdd].ToString() != null && Int32.Parse(lien[nomdansbdd].ToString()) != 0)
                    {
                        int idskill = Int32.Parse(lien[nomdansbdd].ToString());
                        tableauidskill[j - 1] = idskill;
                    }
                    else
                    {
                        tableauidskill[j - 1] = 0;
                    }
                }
                listeDesMissions[i] = new Mission((int)lien["IDMission"], lien["MissionName"].ToString(), (int)lien["IDRank"], tableauidskill[0], tableauidskill[1], tableauidskill[2], tableauidskill[3], tableauidskill[4], lien["AssociatedJob"].ToString());
                ++i;
            } 
        }
        lien.Close();
    }
    public static IEnumerator recupMissionJouable()
    {
        stoprecupmission = false;
        while (!stoprecupmission)
        {
            int tempsattente = 610 - ((Joueur.DateActuelMinute % 10) * 60 + Joueur.DateActuelSeconde);
            string requete = "SELECT COUNT(IDMission) AS Total, IDDistrict FROM present_missions WHERE IDMission NOT IN (SELECT IDMission from present_missions_done WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
            MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
            MySqlDataReader lien = commande.ExecuteReader();
            try
            {
                while (lien.Read())
                {
                    int total = Int32.Parse(lien["Total"].ToString());
                    listeDesMissionsPrésentes = new MissionPrésente[(int)total];
                    Debug.Log("Total de mission jouables : " + total);
                }
            }
            catch
            {

                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Impossible d'accéder à la base de données.";
            }
            lien.Close();

            requete = "SELECT * FROM present_missions WHERE IDMission NOT IN (SELECT IDMission from present_missions_done WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            for (int i = 0; i < listeDesMissionsPrésentes.Length;)
            {
                while (lien.Read())
                {
                    listeDesMissionsPrésentes[i] = new MissionPrésente((int)lien["IDDistrict"], (int)lien["IDMission"], (int)lien["IDCompany"]);
                    ++i;
                }
            }
            lien.Close();
            for (int i = 0; i < listeDesMissionsPrésentes.Length;++i)
            {
                listeDesMissionsPrésentes[i].SaMission = testMissionSpecialise(listeDesMissionsPrésentes[i].SaMission, listeDesMissionsPrésentes[i].SonQuartier);
            }
            yield return new WaitForSeconds(tempsattente);
            ChargerLieu chargement = new ChargerLieu();
            chargement.Recharger();
            StopInteractionBG.interactionbg = true;
            StopInteractionSupp.interactionsupp = true;
        }
    }

    public static void recupMissionJouableNow()
    {
        string requete = "SELECT COUNT(IDMission) AS Total, IDDistrict FROM present_missions WHERE IDMission NOT IN (SELECT IDMission from present_missions_done WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesMissionsPrésentes = new MissionPrésente[(int)total];
            Debug.Log("Total de mission jouables : " + total);
        }
        lien.Close();
        requete = "SELECT * FROM present_missions WHERE IDMission NOT IN (SELECT IDMission from present_missions_done WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesMissionsPrésentes.Length;)
        {
            while (lien.Read())
            {
                listeDesMissionsPrésentes[i] = new MissionPrésente((int)lien["IDDistrict"], (int)lien["IDMission"], (int)lien["IDCompany"]);
                ++i;
            }
        }
        lien.Close();
        for (int i = 0; i < listeDesMissionsPrésentes.Length; ++i)
        {
            listeDesMissionsPrésentes[i].SaMission = testMissionSpecialise(listeDesMissionsPrésentes[i].SaMission, listeDesMissionsPrésentes[i].SonQuartier);
        }
    }

    // Récupère les topics de l'Aide
    public static void RecupTopicsAide()
    {
        string requete = "SELECT COUNT(*) AS Total FROM topic WHERE category='aide'";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesTopicsAide = new Topic[(int)total];
        }
        lien.Close();

        requete = "SELECT * FROM topic WHERE category='aide' ORDER BY datePublication, idTopic DESC";
        
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();

        for (int i = 0; i < listeDesTopicsAide.Length;)
        {
            while (lien.Read())
            {
                listeDesTopicsAide[i] = new Topic(Int32.Parse(lien["idTopic"].ToString()), lien["title"].ToString(), lien["body"].ToString());
                ++i;
            }
        }
        lien.Close();
    }

    static public void DestroyListeMission()
    {
        listeDesMissionsPrésentes = new MissionPrésente[0];
        listeDesPNJPrésents = new PNJPrésent[0];
    }

    static public void DestroyListeDivertissement()
    {
        listeDesDivertissementsPrésents = new MissionDivertissementPrésente[0];
    }

    static Mission testMissionSpecialise(Mission mission, Quartier quartier)
    {
        bool test = false;
        foreach (Quartier q in mission.MissionEntreprise.SesSpécialisations)
        {
            if (quartier.IDQuartier == q.IDQuartier)
            {
                test = true;
            }
        }
        if (test)
        {
            mission.RangMission = Rang.trouverSonRang(mission.RangMission.IDRang + 4);
            ++mission.MissionEntreprise.TailleEntreprise;
        }
        else
        {

        }
        //mission.voirMission();
        return mission;
    }

    public static void recupExamJouable()
    {
        string requete = "SELECT count(*) AS Total,IDExam from association_place_exam where IDExam NOT IN(Select IDExam from exam where IDDiplom IN (SELECT IDDiplom from diplom_pc where IDPCharacter = " + Joueur.IDJoueur + ")) AND(IDExam IN(Select IDExam from exam where IDDiplom IN(SELECT IDDiplom from association_diploms WHERE IDDiplomRequiered IN(SELECT IDDiplom from diplom_pc WHERE IDPCharacter = " + Joueur.IDJoueur + ")))OR IDExam IN(SELECT IDExam FROM exam WHERE IDDiplom NOT IN(Select IDDiplom from association_diploms) AND IDDiplom NOT IN(Select IDDiplom FROM diplom_pc WHERE IDPCharacter = " + Joueur.IDJoueur + "))); ";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesExamensPrésents = new ExamenPrésent[(int)total];
        }
        lien.Close();
        requete = "SELECT * from association_place_exam where IDExam NOT IN(Select IDExam from exam where IDDiplom IN (SELECT IDDiplom from diplom_pc where IDPCharacter = " + Joueur.IDJoueur + ")) AND(IDExam IN(Select IDExam from exam where IDDiplom IN(SELECT IDDiplom from association_diploms WHERE IDDiplomRequiered IN(SELECT IDDiplom from diplom_pc WHERE IDPCharacter = " + Joueur.IDJoueur + ")))OR IDExam IN(SELECT IDExam FROM exam WHERE IDDiplom NOT IN(Select IDDiplom from association_diploms) AND IDDiplom NOT IN(Select IDDiplom FROM diplom_pc WHERE IDPCharacter = " + Joueur.IDJoueur + ")));";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesExamensPrésents.Length;)
        {
            while (lien.Read())
            {
                listeDesExamensPrésents[i] = new ExamenPrésent((int)lien["IDPlace"], (int)lien["IDExam"]);
                ++i;
            }
        }
        lien.Close();
    }

    public static void recupExamensNonJouables()
    {
        // On réinitialise notre tableau d'examens
        listeDesExamensNonJouables = new int[0];

        // Requête permettant de définir une taille à notre tableau d'examens jouables
        string requete = "SELECT count(*) AS Total, IDExam FROM exam WHERE IDDiplom IN (SELECT IDDiplom FROM diplom_pc WHERE IDPCharacter = '" + Joueur.IDJoueur + "')";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesExamensNonJouables = new int[(int)total];
        }
        lien.Close();

        // Requête permettant de récupérer les ID des examens jouables (ceux pas déjà passés par le joueur)
        requete = "SELECT IDExam FROM exam WHERE IDDiplom IN (SELECT IDDiplom FROM diplom_pc WHERE IDPCharacter = '" + Joueur.IDJoueur + "')";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        int i = 0;
        while (lien.Read())
        {
            listeDesExamensNonJouables[i] = (int)lien["IDExam"];
            ++i;
        }
        lien.Close();
    }

    public static void recupDivertJouable()
    {
        listeDesDivertissementsPrésents = new MissionDivertissementPrésente[0];
        string requete = "SELECT count(*) AS Total,IDEntertainment from association_district_entertainment WHERE IDEntertainment NOT IN (Select IDEntertainment From entertainment_done WHERE IDPCharacter ="+Joueur.IDJoueur+");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesDivertissementsPrésents = new MissionDivertissementPrésente[(int)total];
        }
        lien.Close();
        requete = "SELECT * from association_district_entertainment WHERE IDEntertainment NOT IN (Select IDEntertainment From entertainment_done WHERE IDPCharacter =" + Joueur.IDJoueur + ");";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesDivertissementsPrésents.Length;)
        {
            while (lien.Read())
            {
                listeDesDivertissementsPrésents[i] = new MissionDivertissementPrésente((int)lien["IDDistrict"], (int)lien["IDEntertainment"]);
                ++i;
            }
        }
        lien.Close();
    }
    public static void recupPNJJouable()
    {
        listeDesPNJPrésents = new PNJPrésent[0];
        //if (testSiPNJJouable())
        //{
            string requete = "SELECT count(*) AS Total,IDNPCharacter FROM npc_present " +
            "WHERE IDNPCharacter NOT IN(SELECT IDNPCharacter FROM np_character WHERE IDArtefact IN (SELECT IDArtefact FROM artefact_pc WHERE IDPCharacter = " + Joueur.IDJoueur + "));";
            MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
            MySqlDataReader lien = commande.ExecuteReader();
            while (lien.Read())
            {
                int total = Int32.Parse(lien["Total"].ToString());
                listeDesPNJPrésents = new PNJPrésent[(int)total];
            }
            lien.Close();
            requete = "SELECT * FROM npc_present WHERE IDNPCharacter NOT IN" +
            "(SELECT IDNPCharacter FROM np_character WHERE IDArtefact IN (SELECT IDArtefact FROM artefact_pc WHERE IDPCharacter = " + Joueur.IDJoueur + "));";
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            for (int i = 0; i < listeDesPNJPrésents.Length;)
            {
                while (lien.Read())
                {
                    listeDesPNJPrésents[i] = new PNJPrésent((int)lien["IDNPCharacter"], (int)lien["IDMission"]);
                    ++i;
                }
            }
            lien.Close();
        /*}
        else
        {
            //Debug.Log("Aucun PNJ jouable ?");
        }*/
    }

    public static bool testSiPNJJouable()
    {
        //Debug.Log("Liste des diplomes : " + listeDesDiplomes);
        bool test = false;

        for (int i = 0; i < RessourcesBdD.listeDesDiplomes.Length; ++i)
        {
            if (RessourcesBdD.listeDesDiplomes[i].NomDiplome != null)
            {
                if (Joueur.MesDiplomes[i] == true && RessourcesBdD.listeDesDiplomes[i].SonRang.NomRang == "B")
                {
                    test = true;
                    break;
                }
            }
            else
            {
                //Debug.Log("Aucun diplome trouvé");
            }
        }
        return test;
    }

    public static Objet randomObjetAvecRang(string NomRang)
    {
        Objet objetrandom = null;
        int compteur = 0;
        foreach (Objet objet in listeDesObjets)
        {
            if (objet.RangObjet.NomRang == NomRang)
            {
                ++compteur;
            }
        }
        Objet[] tableau = new Objet[compteur];
        int inc = 0;
        foreach (Objet objet in listeDesObjets)
        {
            if (objet.RangObjet.NomRang == NomRang)
            {
                tableau[inc] = objet;
                ++inc;
            }
        }
        int randomObjet = UnityEngine.Random.Range(0,tableau.Length);
        for(int i = 0; i<tableau.Length; ++i)
        {
            if (i == randomObjet)
            {
                objetrandom = tableau[i];
                break;
            }
        }
        return objetrandom;
    }
    public static void RecupMesAmis()
    {
        Joueur.MesAmis= new AutreJoueur[0];
        string requete = "SELECT count(*) AS Total,IDPCharacter from friend WHERE IDFriend ="+Joueur.IDJoueur+ " OR IDPCharacter=" + Joueur.IDJoueur + ";";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            Joueur.MesAmis= new AutreJoueur[(int)total];
        }
        lien.Close();
        requete = "SELECT DISTINCT IDPCharacter,PCName from p_character WHERE IDPCharacter IN(SELECT IDPCharacter from friend WHERE IDFriend =" + Joueur.IDJoueur + ") OR IDPCharacter IN(SELECT IDFriend from friend WHERE IDPCharacter =" + Joueur.IDJoueur + ") AND IDPCharacter !="+Joueur.IDJoueur+";";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < Joueur.MesAmis.Length;)
        {
            while (lien.Read())
            {
                Joueur.MesAmis[i] = new AutreJoueur((int)lien["IDPCharacter"], lien["PCName"].ToString());
                ++i;
            }
        }
        lien.Close();
        foreach (AutreJoueur ami in Joueur.MesAmis)
        {
            ami.trouverToutesInformations();
        }
    }
    public static void RecupDeLaListeDesJoueurs()
    {
        listeDesJoueurs = new AutreJoueur[0];
        string requete = "SELECT count(*) AS Total,IDPCharacter from p_character WHERE IDPCharacter!=" + Joueur.IDJoueur + ";";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesJoueurs = new AutreJoueur[(int)total];
        }
        lien.Close();
        requete = "SELECT IDPCharacter,PCName from p_character WHERE IDPCharacter!=" + Joueur.IDJoueur + ";";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesJoueurs.Length;)
        {
            while (lien.Read())
            {
                listeDesJoueurs[i] = new AutreJoueur((int)lien["IDPCharacter"], lien["PCName"].ToString());
                ++i;
            }
        }
        lien.Close();
    }

    public static void RecupActionsSociales()
    {
        // On initialise les tableaux
        Joueur.MesActionsSocialesObjet = new bool[Joueur.MesAmis.Length];
        Joueur.MesActionsSocialesSkill = new bool[Joueur.MesAmis.Length];

        // Actions sociales d'objet
        string requete = "SELECT * FROM action_sociale WHERE Type = 'ITEM' AND IDPCharacter=" + Joueur.IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < Joueur.MesAmis.Length; ++i)
            {
                if ((int)lien["IDPFriend"] == Joueur.MesAmis[i].SonID)
                {
                    Debug.Log("Ami ayant action sociale Objet : " + Joueur.MesAmis[i].SonNom);
                    Joueur.MesActionsSocialesObjet[i] = true;
                }
                else
                {
                    Joueur.MesActionsSocialesObjet[i] = false;
                }
            }
        }
        lien.Close();

        // Actions sociales d'objet
        requete = "SELECT * FROM action_sociale WHERE Type = 'SKILL' AND IDPCharacter=" + Joueur.IDJoueur;
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < Joueur.MesAmis.Length; ++i)
            {
                if ((int)lien["IDPFriend"] == Joueur.MesAmis[i].SonID)
                {
                    Debug.Log("Ami ayant action sociale Skill : " + Joueur.MesAmis[i].SonNom);
                    Joueur.MesActionsSocialesSkill[i] = true;
                }
                else
                {
                    Joueur.MesActionsSocialesSkill[i] = false;
                }
            }
        }
        lien.Close();
    }
}
