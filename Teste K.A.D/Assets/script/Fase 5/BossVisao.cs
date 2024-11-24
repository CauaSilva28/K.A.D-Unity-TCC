using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisao : MonoBehaviour
{
    [Header("Referências")]
    private BossSeguePlayer bossSeguePlayer; // Referência ao script que controla o movimento do vilão
    public float detectionRange; // Alcance de detecção do jogador

    void Start(){
        bossSeguePlayer = GetComponent<BossSeguePlayer>();
    }

    void Update()
    {
        if(bossSeguePlayer.vidaPerso.value > 0){
            // Calcula a distância entre o vilão e o jogador
            float distanceToPlayer = Vector3.Distance(transform.position, bossSeguePlayer.player[bossSeguePlayer.selecaoPerso.numPerso].position);

            // Verifica se o jogador está dentro do alcance de detecção, se o vilão não está carregando uma investida e se o vilão ainda tem vida
            if (distanceToPlayer < detectionRange && !bossSeguePlayer.isCharging && bossSeguePlayer.vidaBoss > 0)
            {
                bossSeguePlayer.IniciarImpulso();
            }
        }
    }
}