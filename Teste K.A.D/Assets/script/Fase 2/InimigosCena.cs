using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigosCena : MonoBehaviour
{
    public EntrarMercado areaMercado;
    public FalaComMirela falaMirela;

    public bool falandoMirela;

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Inimigos>().vidaInimigo <= 0){
            if(!falandoMirela){
                areaMercado.dinosRua.Remove(gameObject);
            }
            else{
                falaMirela.inimigosMercado.Remove(gameObject);
            }
        }
    }
}
