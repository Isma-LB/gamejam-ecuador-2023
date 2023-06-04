using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicialScript : MonoBehaviour
{
    public void IrAMenuInicial()
    {
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("MenuInicio");
    }
}
