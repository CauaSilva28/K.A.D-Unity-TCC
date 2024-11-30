using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoCameraX : MonoBehaviour
{
    private float rotacaoX = -90f;
    
    public PausarJogo pauseJogo;

    // Update is called once per frame
    void Update()
    {
        if(!pauseJogo.pausado){
            float mouseXInput = Input.GetAxis("Mouse X");

            rotacaoX += mouseXInput * 1.2f;

            transform.localEulerAngles = new Vector3(0f, -rotacaoX, 0f);
        }
    }
}
