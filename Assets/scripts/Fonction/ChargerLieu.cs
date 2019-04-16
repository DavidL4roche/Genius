using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ChargerLieu : MonoBehaviour {
    //Use this for initialization
    public void Charger(string SceneName) {
        // Debug.Log(SceneName);
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        return;
    }
    public IEnumerable Recharger()
    {
        StopCoroutine(RessourcesBdD.recupMissionJouable());
        SpawnerMission.DestroySpawn();
        SpawnerExam.DestroySpawn();
        RessourcesBdD.listeDesExamensPrésents = null;
        RessourcesBdD.DestroyListeMission();     
        RessourcesBdD.recupExamJouable();
        RessourcesBdD.recupDivertJouable();
        RessourcesBdD.recupPNJJouable();
        yield return StartCoroutine(RessourcesBdD.recupMissionJouable());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void rechargerArtefact()
    {
        RessourcesBdD.listeDesArtefactsJouables = new Artefact[0];
        RessourcesBdD.RecupArtefactJouable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
