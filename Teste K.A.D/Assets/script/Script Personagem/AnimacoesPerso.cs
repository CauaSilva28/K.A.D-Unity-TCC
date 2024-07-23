using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesPerso : MonoBehaviour
{
    public Animator anim;

    public bool emAcao = false;
    public bool morrendo = false;

    private Movimento movePerso;

    public GameObject ferramenta;
    // Start is called before the first frame update
    void Start()
    {
        movePerso = GetComponent<Movimento>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!morrendo){
            if(!emAcao){
                anim.SetInteger("transition", 0);

                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
                    anim.SetInteger("transition", 1);
                }
                if(Input.GetKey(KeyCode.A)){
                    anim.SetInteger("transition", 3);
                }
                if(Input.GetKey(KeyCode.D)){
                    anim.SetInteger("transition", 6);
                }

                if(!movePerso.cansou){
                    if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)){
                        anim.SetInteger("transition", 2);
                    }
                }
            }

            
            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.W) && Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.S) && Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(0))
            {
                if(ferramenta.activeSelf){
                    StartCoroutine(AtaqueFerramenta());
                }
                else{
                    StartCoroutine(AtaqueSoco());
                }
            }
        }
    }

    IEnumerator AtaqueFerramenta(){
        emAcao = true;
        anim.SetTrigger("attack");
        anim.SetBool("semArma", false);
        anim.SetInteger("transition", 4);

        yield return new WaitForSeconds(1f);

        emAcao = false;
    }

    IEnumerator AtaqueSoco(){
        emAcao = true;
        anim.SetTrigger("attack");
        anim.SetBool("semArma", true);
        anim.SetInteger("transition", 8);

        yield return new WaitForSeconds(1f);

        emAcao = false;
    }
}
