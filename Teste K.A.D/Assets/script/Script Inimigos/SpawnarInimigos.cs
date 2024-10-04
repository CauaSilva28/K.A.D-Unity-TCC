using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SpawnarInimigos : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject dinoPrefab;
    public GameObject punkPrefab;

    public GameObject[] ferramentaCena;

    public GameObject[] personagem;
    public Transform notPersoPosicao;
    public Slider vidaPerso;
    public Slider barraVidaKombi;

    public AudioSource[] SomDanoPlayer;

    public Movimento movePerso;

    private List<GameObject> inimigos = new List<GameObject>();

    private bool perdendo = false;

    public TelaSelecionarPerso persoSelecionado;

    private void Start() {
        InvokeRepeating("SpawnInimigo", 1, 6);
    }

    void Update(){
        if(vidaPerso.value <= 0){
            DesabilitarScriptsInimigos();
        }
    }

    void SpawnInimigo() {
        if(!perdendo){
            Quaternion valorRotacao = Quaternion.Euler(0f, 90f, 0f);
            int r = Random.Range(0, spawnpoints.Length);

            // Seleciona aleatoriamente entre dinoPrefab e punkPrefab
            GameObject inimigoPrefab = Random.Range(0, 4) == 0 ? dinoPrefab : punkPrefab;
            
            GameObject inimigoInstance = Instantiate(inimigoPrefab, spawnpoints[r].position, valorRotacao);
            inimigoInstance.tag = "inimigo";
            
            Inimigos scriptInimigo = inimigoInstance.GetComponent<Inimigos>();
            scriptInimigo.personagem = personagem;
            scriptInimigo.notPersonagem = notPersoPosicao;
            scriptInimigo.vidaPerso = vidaPerso;
            scriptInimigo.SomDanoPlayer = SomDanoPlayer;
            scriptInimigo.ferramentaCena = ferramentaCena;
            scriptInimigo.persoSelecionado = persoSelecionado;

            InimigoColidiKombi scriptInimigo2 = inimigoInstance.GetComponent<InimigoColidiKombi>();
            scriptInimigo2.barraVidaKombi = barraVidaKombi;

            inimigos.Add(inimigoInstance);
        }
    }

    public void DesabilitarScriptsInimigos() {
        perdendo = true;
        
        for (int i = inimigos.Count - 1; i >= 0; i--) {
            if (inimigos[i] == null) {
                inimigos.RemoveAt(i); // Remove objetos destruídos da lista
                continue;
            }

            Inimigos scriptInimigo = inimigos[i].GetComponent<Inimigos>();
            if (scriptInimigo != null) {
                scriptInimigo.anim.SetInteger("transition", 0);
                scriptInimigo.navMeshAgent.speed = 0;
            }
        }
    }
}
