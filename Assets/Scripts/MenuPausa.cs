using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GenerarLetras scriptLetras;
    [SerializeField] private TextMeshProUGUI letraTM;

    bool pausado = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausado)

                Pausa();
            else
                Reanudar();
        }
        letraTM.SetText(scriptLetras.letraActual);
    }
    public void Pausa()
    {
        if (!gameOverCanvas.activeSelf)
        {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        pausado = true; 
        }
    }

    public void Reanudar()
    {
        if (!gameOverCanvas.activeSelf)
        {
            Time.timeScale = 1f;
            botonPausa.SetActive(true);
            menuPausa.SetActive(false);
            pausado = false;
        }

    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu() 
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MenuInicio");
    }
}
