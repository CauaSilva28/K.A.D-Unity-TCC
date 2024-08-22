using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJogo : MonoBehaviour
{
    public GameObject pauseMenu;

    private AudioSource[] allAudioSources;

    public GameObject player;

    private bool pausado = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtém todos os AudioSource no objeto (e seus filhos)
        allAudioSources = GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetComponent<Movimento>().perdendo){
            if(pausado){
                if(Input.GetKeyDown(KeyCode.Escape)){
                    Despausar();
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
        player.GetComponent<Movimento>().pausado = true;
        pauseMenu.gameObject.SetActive(true);
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
        player.GetComponent<Movimento>().pausado = false;
        pauseMenu.gameObject.SetActive(false);
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

    public void voltarMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
