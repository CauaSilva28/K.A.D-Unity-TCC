using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoCamera : MonoBehaviour
{
    float rotacaoY = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseYInput = Input.GetAxis("Mouse Y");

        rotacaoY += mouseYInput;

        rotacaoY = Mathf.Clamp(rotacaoY, -30, 15);

        transform.localEulerAngles = new Vector3(-rotacaoY, 0f, 0f);
    }
}
