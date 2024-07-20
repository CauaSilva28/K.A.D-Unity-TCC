using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncoesBotoes : MonoBehaviour
{
    public void ReiniciarFase(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
