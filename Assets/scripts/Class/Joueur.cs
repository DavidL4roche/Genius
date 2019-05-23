using MySql.Data.MySqlClient;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour {
    public static int IDJoueur;
    public static string NomJoueur;
    public static int[] MesRessources;
    public static int[] MesValeursCompetences;
    public static int[] MesObjets;
    public static bool[] MesDiplomes;
    public static bool[] MesTrophees;
    public static bool[] MesArtefacts;
    public static AutreJoueur[] MesAmis;
    public static bool[] MesActionsSocialesSkill;
    public static bool[] MesActionsSocialesObjet;
    public static int attenteMaj;
    public static DateTime dateDerniereCo;
    public static DateTime DateActuel = System.DateTime.Now;
    public static int DateActuelMinute = System.DateTime.Now.Minute;
    public static int DateActuelSeconde = System.DateTime.Now.Second;
    
    private string url = Configuration.url + "scripts/UpdateDateCo.php?id=" + IDJoueur;
    private WWW download;
    private string monJson;
    private JSONNode monNode;

    // Use this for initialization
    public void Start() {
        Debug.Log("Instanciation du joueur en cours");
        DontDestroyOnLoad(gameObject);
        majdepuisBDD();
        PendantAbsence();
        RessourcesBdD.recupExamJouable();
        RessourcesBdD.recupDivertJouable();
        RessourcesBdD.recupPNJJouable();
        RessourcesBdD.RecupArtefactJouable();
        RessourcesBdD.RecupObjetMagasin();
        RessourcesBdD.RecupDeLaListeDesJoueurs();
        RessourcesBdD.RecupMesAmis();
        RessourcesBdD.RecupActionsSociales();
        StartCoroutine(IncrementationRessources());
        StartCoroutine(RessourcesBdD.recupMissionJouable());
    }
	
	// Update is called once per frame
	void Update () {
        DateActuelMinute = System.DateTime.Now.Minute;
        DateActuelSeconde = System.DateTime.Now.Second;
        StartCoroutine(UpdateDateDerniereCoEnBase());
        StartCoroutine(transfertRessourcesEnBaseScript());
    }

    public IEnumerator UpdateDateDerniereCoEnBase()
    {
        download = new WWW(url);
        yield return download;
    }

    public IEnumerator transfertRessourcesEnBaseScript()
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            string urlRessource = Configuration.url + "scripts/ChangeRessource.php?idRessource=" + (i+1) + "&idJoueur=" + IDJoueur + "&value=" + RessourcesBdD.listeDesRessources[i];
            download = new WWW(urlRessource);
            yield return download;
        }
    }

    // Fais la mise à jour depuis la base de toutes les données importantes
    void majdepuisBDD()
    { 
        majRessources();
        majComp();
        majObjet();
        majDiplome();
        majTrophee();
        majArtefact();
    }

    void majObjet()
    {
        string requete = "SELECT IDItem, Quantity FROM item_pc WHERE IDPCharacter=" + IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for(int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
            {
                if ((int)lien["IDItem"] == RessourcesBdD.listeDesObjets[i].ID)
                {
                    MesObjets[i] = (int)lien["Quantity"];
                    //Debug.Log("Je possède "+MesObjets[i]+ " " + RessourcesBdD.listeNomObjets[i]);
                }
            }
        }
        lien.Close();
    }

    void majComp()
    {
        string requete = "SELECT * FROM skill_pc WHERE IDPCharacter=" + IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for(int i = 0; i < MesValeursCompetences.Length; ++i)
            {
                if(Int32.Parse(lien["IDSkill"].ToString()) == RessourcesBdD.listeDesCompétences[i].ID)
                {
                    MesValeursCompetences[i] = Int32.Parse(lien["SkillLevel"].ToString());
                }
                else
                {
                    continue;
                }
            }
        }
        lien.Close();
    }

    void majRessources()
    {
        string requete = "SELECT * FROM association_ressource_pc WHERE IDPCharacter=" + IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for(int i = 0; i<RessourcesBdD.listeDesRessources.Length; ++i)
            {
                if ((int)lien["IDRessource"] == RessourcesBdD.listeDesRessources[i].ID)
                {
                    MesRessources[i] = (int)lien["Value"];
                }
                else
                {
                    continue;
                }
            }
        }
        lien.Close();
    }

    void majDiplome()
    {
        string requete = "SELECT * FROM diplom_pc WHERE IDPCharacter=" + IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < RessourcesBdD.listeDesDiplomes.Length; ++i)
            {
                if ((int)lien["IDDiplom"] == RessourcesBdD.listeDesDiplomes[i].IDDiplome)
                {
                    MesDiplomes[i] = true;
                }
                else
                {
                    continue;
                }
            }
        }
        lien.Close();
    }
    void majTrophee()
    {
        string requete = "SELECT * FROM trophy_pc WHERE IDPCharacter=" + IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < RessourcesBdD.listeDesTrophees.Length; ++i)
            {
                if ((int)lien["IDTrophy"] == RessourcesBdD.listeDesTrophees[i].IDTrophee)
                {
                    MesTrophees[i] = true;
                }
                else
                {
                    continue;
                }
            }
        }
        lien.Close();
    }
    void majArtefact()
    {
        string requete = "SELECT * FROM artefact_pc WHERE IDPCharacter=" + IDJoueur;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < RessourcesBdD.listeDesArtefacts.Length; ++i)
            {
                if ((int)lien["IDArtefact"] == RessourcesBdD.listeDesArtefacts[i].IDArtefact)
                {
                    MesArtefacts[i] = true;
                }
                else
                {
                    continue;
                }
            }
        }
        lien.Close();
    }

    public static void transfertEnBase()
    {
        // Les compétences
        transfertCompetencesEnBase();

        // Les ressources
        transfertRessourcesEnBase();

        //date de dernière connexion
        string requete2 = "UPDATE `p_character` SET `LastConnection`=LOCALTIMESTAMP WHERE `IDPCharacter`=" + Joueur.IDJoueur+";";
        MySqlCommand commande2 = new MySqlCommand(requete2, Connexion.connexion);
        MySqlDataReader lien2 = commande2.ExecuteReader();
        lien2.Close();

        //Objets
        transfertObjetsEnBase();
    }

    // Transfert des ressources en base
    public static void transfertRessourcesEnBase()
    {
        //Ressources
        for (int i = 0; i < MesRessources.Length; ++i)
        {
            int Total = 0;
            string requete = "SELECT Count(*) AS Total, IDRessource from association_ressource_pc WHERE IDPCharacter=" + IDJoueur + " AND IDRessource=" + RessourcesBdD.listeDesRessources[i].ID;
            MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
            MySqlDataReader lien = commande.ExecuteReader();
            while (lien.Read())
            {
                Total = Int32.Parse(lien["Total"].ToString());
            }
            lien.Close();
            if (Total == 0)
            {
                requete = "INSERT INTO association_ressource_pc VALUES (" + RessourcesBdD.listeDesRessources[i].ID + "," + IDJoueur + "," + MesRessources[i] + ");";
            }
            else
            {
                requete = "UPDATE association_ressource_pc SET Value=" + MesRessources[i] + " WHERE IDPCharacter=" + IDJoueur + " AND IDRessource=" + RessourcesBdD.listeDesRessources[i].ID;
            }
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            lien.Close();
        }
    }

    // Transfert des compétences en base
    public static void transfertCompetencesEnBase()
    {
        for (int i = 0; i < MesValeursCompetences.Length; ++i)
        {
            int Total = 0;
            string requete = "SELECT Count(*) AS Total, IDSkill from skill_pc WHERE IDPCharacter=" + IDJoueur + " AND IDSkill=" + RessourcesBdD.listeDesCompétences[i].ID;
            MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
            MySqlDataReader lien = commande.ExecuteReader();
            while (lien.Read())
            {
                Total = Int32.Parse(lien["Total"].ToString());
            }
            lien.Close();
            if (Total == 0)
            {
                requete = "INSERT INTO skill_pc VALUES (" + RessourcesBdD.listeDesCompétences[i].ID + "," + IDJoueur + "," + MesValeursCompetences[i] + ");";
            }
            else
            {
                requete = "UPDATE skill_pc SET SkillLevel=" + MesValeursCompetences[i] + " WHERE IDPCharacter=" + IDJoueur + " AND IDSkill=" + RessourcesBdD.listeDesCompétences[i].ID + ";";
            }
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            lien.Close();
        }
    }

    // Transfert des objets en base
    public static void transfertObjetsEnBase()
    {
        for (int i = 0; i < MesObjets.Length; ++i)
        {
            int Total = 0;
            string requete = "SELECT Count(*) AS Total, IDItem AS Total from item_pc WHERE IDPCharacter=" + IDJoueur + " AND IDItem=" + RessourcesBdD.listeDesObjets[i].ID;
            MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
            MySqlDataReader lien = commande.ExecuteReader();
            while (lien.Read())
            {
                Total = Int32.Parse(lien["Total"].ToString());
            }
            lien.Close();
            if (Total == 0)
            {
                requete = "INSERT INTO item_pc VALUES (" + RessourcesBdD.listeDesObjets[i].ID + "," + IDJoueur + "," + MesObjets[i] + ");";
            }
            else
            {
                requete = "UPDATE item_pc SET Quantity=" + MesObjets[i] + " WHERE IDPCharacter=" + IDJoueur + " AND IDItem=" + RessourcesBdD.listeDesObjets[i].ID;
            }
            commande = new MySqlCommand(requete, Connexion.connexion);
            lien = commande.ExecuteReader();
            lien.Close();
        }
    }
    
    // Transfert des actions sociales en base
    public static void transfertActionsSocialesEnBase()
    {
        for (int i = 0; i < Joueur.MesAmis.Length; ++i)
        {
            // Si l'action Sociale (ITEM) du joueur avec cet ami est vrai (effectuée)
            if (Joueur.MesActionsSocialesObjet[i])
            {
                // On vérifie si le Joueur apparait dans la base (table action_sociale) : ITEM
                int Total = 0;
                string requete = "SELECT Count(*) AS Total from action_sociale WHERE IDPCharacter=" + IDJoueur + " AND IDPFriend=" + Joueur.MesAmis[i].SonID + " AND Type = 'ITEM'";
                Debug.Log(requete);
                MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
                MySqlDataReader lien = commande.ExecuteReader();
                while (lien.Read())
                {
                    Total = Int32.Parse(lien["Total"].ToString());
                }
                lien.Close();
            
                if (Total == 0)
                {
                    requete = "INSERT INTO action_sociale VALUES (" + IDJoueur + "," + Joueur.MesAmis[i].SonID + ",'ITEM');";
                    Debug.Log(requete);
                    commande = new MySqlCommand(requete, Connexion.connexion);
                    lien = commande.ExecuteReader();
                    lien.Close();
                }
            }

            // Si l'action Sociale (SKILL) du joueur avec cet ami est vrai (effectuée)
            if (Joueur.MesActionsSocialesSkill[i])
            {
                // On vérifie si le Joueur apparait dans la base (table action_sociale) : SKILL
                int Total = 0;
                string requete = "SELECT Count(*) AS Total from action_sociale WHERE IDPCharacter=" + IDJoueur + " AND IDPFriend=" + Joueur.MesAmis[i].SonID + " AND Type = 'SKILL'";
                Debug.Log(requete);
                MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
                MySqlDataReader lien = commande.ExecuteReader();
                while (lien.Read())
                {
                    Total = Int32.Parse(lien["Total"].ToString());
                }
                lien.Close();

                if (Total == 0)
                {
                    requete = "INSERT INTO action_sociale VALUES (" + IDJoueur + "," + Joueur.MesAmis[i].SonID + ",'SKILL');";
                    Debug.Log(requete);
                    commande = new MySqlCommand(requete, Connexion.connexion);
                    lien = commande.ExecuteReader();
                    lien.Close();
                }
            }
        }
    }

    IEnumerator IncrementationRessources()
    {
        bool stop = false;
        while (!stop)
        {
            //long soustract = DateActuel.ToFileTimeUtc();
            yield return new WaitForSeconds(60*6);
            for (int i = 0; i < MesRessources.Length; ++i)
            {
                switch (RessourcesBdD.listeDesRessources[i].NomRessource)
                {
                    case "Social":
                    case "Divertissement":
                        if (MesRessources[i] < 100)
                        {
                            MesRessources[i] += 1;
                        }
                        else
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
            
        }   
    }
    void PendantAbsence()
    {
        TimeSpan tempsABS = DateActuel.Subtract(dateDerniereCo);
        for(int i = 0; i< RessourcesBdD.listeDesRessources.Length;++i)
        {
            if(RessourcesBdD.listeDesRessources[i].NomRessource == "Social" || RessourcesBdD.listeDesRessources[i].NomRessource == "Divertissement")
            {
                MesRessources[i] += (int)(tempsABS.TotalSeconds/360);
                if (MesRessources[i] > 100)
                {
                    MesRessources[i] = 100;
                }
            }
        }
    }
    static public void VoirStat()
    {
        Debug.Log("Joueur numéro ->" +IDJoueur);
        for(int i = 0 ; i<RessourcesBdD.listeDesRessources.Length ; ++i)
        {
            Debug.Log("Ressources ->" + RessourcesBdD.listeDesRessources[i].NomRessource + " = " + MesRessources[i]);
        }
        for (int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            Debug.Log("Compétence ->" + RessourcesBdD.listeDesCompétences[i].ID + " = " + MesValeursCompetences[i]);
        }
    }
}
