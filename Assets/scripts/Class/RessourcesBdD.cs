using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.SceneManagement;

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

    public static AutreJoueur[] listeDesJoueurs;
    public static bool stoprecupmission = false;
    // Use this for initialization
    public static void Recup()
    {
        RecupLieu();
        RecupGain();
        RecupPerte();
        RecupBonus();
        RecupDuree();
        RecupRang();
        RecupQuartier();
        RecupDivert();
        RecupComp();
        RecupRess();
        RecupObjet();
        RecupDiplome();
        RecupTrophees();
        RecupExam();
        RecupEntreprise();
        RecupMission();
        RecupArtefact();
        RecupPNJ();
    }
    public static void RecupObjetMagasin()
    {
        string requete = "SELECT count(*) AS Total, IDItem from association_shop_item WHERE IDItem NOT IN(SELECT IDItem FROM item_bought WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            LeMagasin = new ObjetPrésent[(int)total];
        }
        lien.Close();
        requete = "SELECT * from association_shop_item WHERE IDItem NOT IN(SELECT IDItem FROM item_bought WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < LeMagasin.Length;)
        {
            while (lien.Read())
            {
                LeMagasin[i] = new ObjetPrésent((int)lien["IDItem"]);
                ++i;
            }
        }
        lien.Close();
    }
    public static void RecupArtefactJouable(){
        string requete = "SELECT count(*) AS Total, IDArtefact from artefact WHERE IDArtefact NOT IN(SELECT IDArtefact FROM artefact_used WHERE IDPCharacter=" + Joueur.IDJoueur+ ") AND IDArtefact IN(SELECT IDArtefact from artefact_pc WHERE IDPCharacter=" + Joueur.IDJoueur + ");";
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
    private static void RecupArtefact(){
        string requete = "SELECT count(*) AS Total, IDArtefact from artefact";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesArtefacts = new Artefact[(int)total];
            Joueur.MesArtefacts = new bool[(int)total];
        }
        lien.Close();
        requete = "SELECT * from artefact";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesArtefacts.Length;)
        {
            while (lien.Read())
            {
                listeDesArtefacts[i] = new Artefact((int)lien["IDArtefact"], lien["ArtefactName"].ToString(), (int)lien["IDBonus"]);
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
            requete = "SELECT * from association_district_npc WHERE IDNPCharacter="+listeDesPNJ[i].IDPNJ+";";
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
                listeDesDivertissements[i] = new MissionDivertissement((int)lien["IDEntertainment"], lien["EntertainmentName"].ToString(),(int)lien["IDRank"]);
                ++i;
            }
        }
        lien.Close();
    }
    private static void RecupLieu()
    {
        string requete = "SELECT count(*) AS Total, IDPlace from place";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesLieux = new Lieu[(int)total];
        }
        lien.Close();
        requete = "SELECT * from place";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesLieux.Length;)
        {
            while (lien.Read())
            {
                listeDesLieux[i] = new Lieu((int)lien["IDPlace"], lien["PlaceName"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    private static void RecupDuree()
    {
        string requete = "SELECT count(*) AS Total, IDDuration from duration";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesDurees = new Duree[(int)total];
        }
        lien.Close();
        requete = "SELECT * from duration";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesDurees.Length;)
        {
            while (lien.Read())
            {
                listeDesDurees[i] = new Duree((int)lien["IDDuration"], lien["DurationName"].ToString(),(int)lien["DurationValue"]);
                ++i;
            }
        }
        lien.Close();
    }
    private static void RecupGain()
    {
        string requete = "SELECT count(*) AS Total, IDGain from gain";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesGains = new Gain[(int)total];
        }
        lien.Close();
        requete = "SELECT * from gain";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesGains.Length;)
        {
            while (lien.Read())
            {
                listeDesGains[i] = new Gain((int)lien["IDGain"], lien["GainName"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    private static void RecupPerte()
    {
        string requete = "SELECT count(*) AS Total, IDLoss from loss";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesPertes = new Perte[(int)total];
        }
        lien.Close();
        requete = "SELECT * from loss";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesPertes.Length;)
        {
            while (lien.Read())
            {
                listeDesPertes[i] = new Perte((int)lien["IDLoss"], lien["LossName"].ToString());
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
    private static void RecupQuartier()
    {
        string requete = "SELECT count(*) AS Total, IDDistrict from district";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesQuartiers = new Quartier[(int)total];
        }
        lien.Close();
        requete = "SELECT * from district";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesQuartiers.Length;)
        {
            while (lien.Read())
            {
                listeDesQuartiers[i] = new Quartier((int)lien["IDDistrict"], lien["DistrictName"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    private static void RecupObjet()
    {
        string requete = "SELECT count(*) AS Total, IDItem from item";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesObjets = new Objet[total];
        }
        lien.Close();
        requete = "SELECT * from item";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesObjets.Length;)
        {
            while (lien.Read())
            {
                listeDesObjets[i] = new Objet((int)lien["IDItem"], lien["ItemName"].ToString(), (int)lien["IDRank"], (int)lien["IDBonus"], (int)lien["BonusGain"]);
                //listeDesObjets[i].voirObjet();
                ++i;
            }
        }
        lien.Close();
        Joueur.MesObjets = new int[listeDesObjets.Length];
    }
    static void RecupRang()
    {
        string requete = "SELECT count(*) AS Total, IDRank from rank";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesRangs = new Rang[(int)total];
        }
        lien.Close();
        requete = "SELECT * from rank";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesRangs.Length;)
        {
            while (lien.Read())
            {
                listeDesRangs[i] = new Rang((int)lien["IDRank"],lien["RankName"].ToString());
                //listeDesRangs[i].voirRang();
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
    static void RecupTrophees()
    {
        string requete = "SELECT count(*) AS Total, IDTrophy from trophy";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesTrophees = new Trophee[(int)total];
            Joueur.MesTrophees = new bool[(int)total];
        }
        lien.Close();
        requete = "SELECT * from trophy";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesTrophees.Length;)
        {
            while (lien.Read())
            {
                listeDesTrophees[i] = new Trophee((int)lien["IDTrophy"], (int)lien["IDRank"], lien["Description"].ToString());
                ++i;
            }
        }
        lien.Close();
    }
    static void RecupExam()
    {
        string requete = "SELECT count(*) AS Total, IDExam from exam";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesExamens = new Examen[(int)total];
        }
        lien.Close();
        requete = "SELECT * from exam";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesExamens.Length;)
        {
            while (lien.Read())
            {
                listeDesExamens[i] = new Examen((int)lien["IDExam"], lien["ExamName"].ToString(), (int)lien["IDDiplom"], (int)lien["IDSkillSlot1"], (int)lien["IDSkillSlot2"], (int)lien["IDSkillSlot3"], (int)lien["IDSkillSlot4"], (int)lien["IDSkillSlot5"], (int)lien["IDRank"]);
                ++i;
            }
        }
        lien.Close();
    }
    static void RecupEntreprise()
    {
        string requete = "SELECT count(*) AS Total, IDCompany from company";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesEntreprises = new Entreprise[(int)total];
        }
        lien.Close();
        requete = "SELECT * from company";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesEntreprises.Length;)
        {
            while (lien.Read())
            {
                listeDesEntreprises[i] = new Entreprise((int)lien["IDCompany"], lien["CompanyName"].ToString(), (int)lien["Size"]);
                ++i;
            }
        }
        lien.Close();


        // SES EMPLACEMENTS




        for (int i = 0; i < listeDesEntreprises.Length;++i)
        {
            int total = 0;
            requete = "SELECT Count(*) AS Total,IDDistrict from association_company_district WHERE IDCompany="+listeDesEntreprises[i].IDEntreprise+";";
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
                for (int j = 0; j<total;)
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
            else if(total == 1)
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
                listeDesEntreprises[i].SesEmplacements[0] = new Quartier(0,"0");
            }


            //Ses spécialités




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
        }
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
    static void RecupBonus()
    {
        string requete = "SELECT count(*) AS Total, IDBonus from bonus";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            int total = Int32.Parse(lien["Total"].ToString());
            listeDesBonus = new Bonus[(int)total];
        }
        lien.Close();
        requete = "SELECT * from bonus";
        commande = new MySqlCommand(requete, Connexion.connexion);
        lien = commande.ExecuteReader();
        for (int i = 0; i < listeDesBonus.Length;)
        {
            while (lien.Read())
            {
                listeDesBonus[i] = new Bonus((int)lien["IDBonus"], lien["BonusName"].ToString());
                ++i;
            }
        }
        lien.Close();
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
            requete = "SELECT * from npc_present WHERE IDNPCharacter NOT IN (Select IDFriend From friend WHERE IDPCharacter =" + Joueur.IDJoueur + ");";
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
