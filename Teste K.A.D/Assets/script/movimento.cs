using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento : MonoBehaviour
{
    private float velocidade = 30f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    float rotationSpeed = 120f;

    private Animator anim;

    private float gravidade = 9.8f;
    private float velocidadeVertical = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("transition", 0);

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            anim.SetInteger("transition", 1);
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)){
            velocidade = 60f;
            anim.SetInteger("transition", 2);
        }
        else{
            velocidade = 30f;
        }

        //Movimentacao-------------------------------
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical")) * velocidade;
        moveDirection = transform.TransformDirection(moveDirection);

        //Gravidade-----------------------------
        if (controller.isGrounded)
        {
            velocidadeVertical = 0f; // Reseta a velocidade vertical se estiver no ch�o
        }
        else
        {
            velocidadeVertical -= gravidade * Time.deltaTime; // Aplica a gravidade
        }

        moveDirection.y = velocidadeVertical;
        //FIM Gravidade-----------------------------

        controller.Move(moveDirection * Time.deltaTime);
        //FIM Movimentacao-------------------------------

        //Rotacaoo-------------------------------
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, -rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        //FIM Rotacaoo-------------------------------
    }
}
