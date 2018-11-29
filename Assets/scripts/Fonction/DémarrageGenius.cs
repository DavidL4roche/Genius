using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DémarrageGenius : MonoBehaviour {

    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(0.02f);

        ChargerLieu charger = new ChargerLieu();
        charger.Charger("Index2");
    }
}
