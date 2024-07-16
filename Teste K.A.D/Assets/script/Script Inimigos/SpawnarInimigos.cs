using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnarInimigos : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject dinoPrefab;
    public GameObject punkPrefab;

    public Transform persoPosicao;
    public Transform notPersoPosicao;
    public Slider vidaPerso;
    public Slider barraVidaKombi;

    private void Start() {
        InvokeRepeating("SpawnInimigo", 1, 10);
    }

    void SpawnInimigo() {
        Quaternion valorRotacao = Quaternion.Euler(0f, 90f, 0f);
        int r = Random.Range(0, spawnpoints.Length);

        // Seleciona aleatoriamente entre dinoPrefab e punkPrefab
        GameObject inimigoPrefab = Random.Range(0, 4) == 0 ? dinoPrefab : punkPrefab;
        
        GameObject inimigoInstance = Instantiate(inimigoPrefab, spawnpoints[r].position, valorRotacao);
        inimigoInstance.tag = "inimigo";
        
        Inimigos scriptInimigo = inimigoInstance.GetComponent<Inimigos>();
        scriptInimigo.personagem = persoPosicao;
        scriptInimigo.notPersonagem = notPersoPosicao;
        scriptInimigo.vidaPerso = vidaPerso;

        InimigoColidiKombi scriptInimigo2 = inimigoInstance.GetComponent<InimigoColidiKombi>();
        scriptInimigo2.barraVidaKombi = barraVidaKombi;
    }
}
