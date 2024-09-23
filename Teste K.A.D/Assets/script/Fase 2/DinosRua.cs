using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosRua : MonoBehaviour
{
    public EntrarMercado areaMercado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Inimigos>().vidaInimigo <= 0){
            areaMercado.dinosRua.Remove(gameObject);
        }
    }
}
