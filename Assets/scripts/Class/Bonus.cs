using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
    public int IDBonus;
    public string NomBonus;

    public Bonus(int id, string nom)
    {
        IDBonus = id;
        NomBonus = nom;
    }
    public Bonus(Bonus bonus)
    {
        IDBonus = bonus.IDBonus;
        NomBonus = bonus.NomBonus;
    }
    public static Bonus trouverSonBonus(int id)
    {
        foreach (Bonus bonus in RessourcesBdD.listeDesBonus)
        {
            if(id == bonus.IDBonus)
            {
                return new Bonus(bonus);
            }
        }
        return null;
    }
}
