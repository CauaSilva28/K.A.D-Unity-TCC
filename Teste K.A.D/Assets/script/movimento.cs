using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private float velocidade = 30f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private float rotationSpeed = 2f;

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
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)){
            velocidade = 60f;
        }
        else{
            velocidade = 30f;
        }

        //Movimentacao-------------------------------
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * velocidade;
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
        float mouseXInput = Input.GetAxis("Mouse X");

        transform.Rotate(0f, mouseXInput * rotationSpeed, 0f);
        //FIM Rotacaoo-------------------------------
    }
}
