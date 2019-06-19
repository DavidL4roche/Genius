using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueDiplomeAmi : MonoBehaviour
{
    AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];

    public GameObject LeTuple;
    public RawImage Icone;
    public Text TexteDuNom;
    GameObject instance;
    public Text LeTexteQuantite;
    public Texture textu;

    private void Start()
    {
        LeTexteQuantite.text = "";
        Icone.texture = textu;

        for (int i = 0; i < RessourcesBdD.listeDesDiplomes.Length; ++i)
        {
            // TODO: Ajouter les diplomes de l'ami et non du joueur
            if (joueur.SesDiplomes[i] == 1)
            {
                Debug.Log(RessourcesBdD.listeDesDiplomes[i].NomDiplome);
                TexteDuNom.text = RessourcesBdD.listeDesDiplomes[i].NomDiplome.ToUpper();
                TexteDuNom.transform.name = "Texte" + (i + 1);
                instance = Instantiate(LeTuple, new Vector3(0.0F, 0.0F, 0.0F), LeTuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < RessourcesBdD.listeDesDiplomes.Length; ++i)
        {
            if (joueur.SesDiplomes[i] == 1)
            {
                int TailleTexte = GameObject.Find("Texte" + (i + 1)).GetComponent<Text>().text.Length;

                if (TailleTexte >= 17)
                {
                    int TailleZone = TailleTexte * 8;

                    if (GameObject.Find("Texte" + (i + 1)).transform.position.x <= 41 - TailleZone - 136)
                    {
                        GameObject.Find("Texte" + (i + 1)).transform.position = new Vector2(80,
                            GameObject.Find("Texte" + (i + 1)).transform.position.y);
                    }

                    GameObject.Find("Texte" + (i + 1)).transform.position = new Vector2(GameObject.Find("Texte" + (i + 1)).transform.position.x - 0.9f,
                        GameObject.Find("Texte" + (i + 1)).transform.position.y);
                }
            }
        }
    }
}
