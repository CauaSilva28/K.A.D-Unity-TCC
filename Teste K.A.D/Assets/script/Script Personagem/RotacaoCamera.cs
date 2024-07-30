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

        if(Input.GetMouseButton(1)){
            GetComponent<Camera>().fieldOfView -= Time.deltaTime * 30;

            if(GetComponent<Camera>().fieldOfView <= 50){
                GetComponent<Camera>().fieldOfView = 50;
            }
        }
        else{
            GetComponent<Camera>().fieldOfView += Time.deltaTime * 30;

            if(GetComponent<Camera>().fieldOfView >= 67){
                GetComponent<Camera>().fieldOfView = 67;
            }
        }
    }
}
