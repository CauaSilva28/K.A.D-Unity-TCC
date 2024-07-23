using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoCameraX : MonoBehaviour
{
    private float rotacaoX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXInput = Input.GetAxis("Mouse X");

        rotacaoX += mouseXInput * 1.2f;

        transform.localEulerAngles = new Vector3(0f, -rotacaoX, 0f);
    }
}
