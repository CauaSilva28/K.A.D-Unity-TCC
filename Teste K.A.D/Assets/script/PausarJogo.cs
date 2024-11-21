using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PausarJogo : MonoBehaviour
{
    public GameObject pauseMenu;

    public AudioMixer mixer;

    private AudioSource[] allAudioSources;

    public AudioSource somClick;

    public GameObject telaControles;

    public Slider barraVolumeMusica;

    public bool pausado = false;
    public bool perdendo = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtém todos os AudioSource no objeto (e seus filhos)
        allAudioSources = GetComponentsInChildren<AudioSource>();
    }

    public void volumeMusica(){
        float volume = barraVolumeMusica.value;
        mixer.SetFloat("musica", Mathf.Log10(volume)*20);
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
        mixer.SetFloat("efeitoSonoro", -80);
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
        mixer.SetFloat("efeitoSonoro", 0);
        somClick.Play();
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
        somClick.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void abrirControles()
    {
        somClick.Play();
        telaControles.SetActive(true);
    }

    public void voltarPause()
    {
        somClick.Play();
        telaControles.SetActive(false);
    }

    public void voltarMenu()
    {
        somClick.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
