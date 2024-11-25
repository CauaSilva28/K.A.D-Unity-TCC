using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPilar : MonoBehaviour
{
    public int vidaPilar = 3;

    void Update(){
        if(vidaPilar <= 0){
            Destroy(gameObject);
        }
    }
}
