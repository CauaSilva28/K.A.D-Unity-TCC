using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnarInimigos : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject dinoPrefab;

    public Transform persoPosicao;
    public Slider vidaPerso;

    private void Start(){
        InvokeRepeating("SpawnDino", 1, 10);
    }

    void SpawnDino(){
        Quaternion valorRotacao = Quaternion.Euler(0f, 90f, 0f);
        int r = Random.Range(0, spawnpoints.Length);
        GameObject dinoInstance = Instantiate(dinoPrefab, spawnpoints[r].position, valorRotacao);
        dinoInstance.tag = "inimigos";
        
        Dino scriptDino = dinoInstance.GetComponent<Dino>();
        scriptDino.personagem = persoPosicao;
        scriptDino.vidaPerso = vidaPerso;
    }
}
