using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnarInimigos : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject dinoPrefab;

    public Transform persoPosicao;
    public Transform notPersoPosicao;
    public Slider vidaPerso;
    public Slider barraVidaKombi;

    public Movimento movePerso;

    private void Start(){
        InvokeRepeating("SpawnDino", 1, 10);
    }

    void SpawnDino(){
        Quaternion valorRotacao = Quaternion.Euler(0f, 90f, 0f);
        int r = Random.Range(0, spawnpoints.Length);
        GameObject dinoInstance = Instantiate(dinoPrefab, spawnpoints[r].position, valorRotacao);
        dinoInstance.tag = "inimigo";
        
        Dino scriptDino = dinoInstance.GetComponent<Dino>();
        scriptDino.personagem = persoPosicao;
        scriptDino.notPersonagem = notPersoPosicao;
        scriptDino.vidaPerso = vidaPerso;
        scriptDino.movePerso = movePerso;

        InimigoColidiKombi scriptDino2 = dinoInstance.GetComponent<InimigoColidiKombi>();
        scriptDino2.barraVidaKombi = barraVidaKombi;
    }
}
