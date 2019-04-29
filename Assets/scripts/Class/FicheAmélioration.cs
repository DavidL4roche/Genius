using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FicheAmélioration : MonoBehaviour {
    public static bool Concentration = false;
    public static int IDObjetUtilise = 0;
    public static int BonusOOrcus = 0;
    public static int BonusODuree = 0;
    public static int BonusOComp = 0;
    public static float Optimisation = 1.0F;
    // Use this for initialization
    void Start () {
        attribuerBonusObjets();
	}
    public static void attribuerBonusObjets()
    {
        if (IDObjetUtilise == 0)
        {
            BonusOOrcus = 0;
            BonusODuree = 0;
            BonusOComp = 0;
        }
        else
        {
            for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
            {
                if (IDObjetUtilise == RessourcesBdD.listeDesObjets[i].ID)
                {
                    switch (RessourcesBdD.listeDesObjets[i].Bonus.NomBonus)
                    {
                        case "Orcus":
                            BonusOOrcus = RessourcesBdD.listeDesObjets[i].Valeur;
                            break;
                        case "Compétence":
                            BonusOComp = RessourcesBdD.listeDesObjets[i].Valeur;
                            break;
                        case "Temps":
                            BonusODuree = RessourcesBdD.listeDesObjets[i].Valeur;
                            break;
                    }
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
