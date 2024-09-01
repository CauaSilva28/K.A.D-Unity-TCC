using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJogo : MonoBehaviour
{
    public GameObject pauseMenu;

    private AudioSource[] allAudioSources;

    public GameObject telaControles;

    public bool pausado = false;
    public bool perdendo = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtém todos os AudioSource no objeto (e seus filhos)
        allAudioSources = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!perdendo){
            if(pausado){
                if(Input.GetKeyDown(KeyCode.Escape)){
                    Despausar();
                    voltarPause();
                }
            }
            else{
                if(Input.GetKeyDown(KeyCode.Escape)){
                    PauseJogo();
                }
            }
        }
    }

    void PauseJogo()
    {
        pausado = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        // Pausa todos os áudios
        for (int i = 0; i < allAudioSources.Length; i++)
        {
            if (allAudioSources[i] != null)
            {
                allAudioSources[i].Pause();
            }
        }
    }

    public void Despausar()
    {
        pausado = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        // Retoma todos os áudios
        for (int i = 0; i < allAudioSources.Length; i++)
        {
            if (allAudioSources[i] != null)
            {
                allAudioSources[i].UnPause();
            }
        }
    }

    public void ReiniciarFase(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void abrirControles()
    {
        telaControles.SetActive(true);
    }

    public void voltarPause()
    {
        telaControles.SetActive(false);
    }

    public void voltarMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
