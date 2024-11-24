using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPilar : MonoBehaviour
{
    private int vidaPilar = 3;

    void Update(){
        if(vidaPilar <= 0){
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("boss")){
            vidaPilar--;
        }
    }
}
