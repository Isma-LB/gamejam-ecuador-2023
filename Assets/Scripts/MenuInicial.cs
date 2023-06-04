using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    AudioManager audioManager;

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayTheme()
    {
        if (audioManager != null)
        {
            audioManager.Play("Intro");
        }
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
