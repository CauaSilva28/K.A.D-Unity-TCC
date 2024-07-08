using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoColidiKombi : MonoBehaviour
{
    public Slider barraVidaKombi;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("DestruidorInimigo")){
            Destroy(gameObject);
            barraVidaKombi.value -= 0.1f;
        }
    }
}
