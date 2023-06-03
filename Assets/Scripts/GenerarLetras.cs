using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarLetras : MonoBehaviour
{
    private List<string> letras = new List<string>() {"A","S","D"};
    [SerializeField] float periodo = 2f;
    public string letraActual = null;
    public bool aplastado = false;
    public float tiempoTranscurridoDesdeSpawn = 2f;

    // Start is called before the first frame update
    void Start()
    {
        GenerarLetra();
    }

    // Update is called once per frame
    void Update()
    {
        tiempoTranscurridoDesdeSpawn = tiempoTranscurridoDesdeSpawn - Time.deltaTime;
    }

    public void GenerarLetra()
    {
        tiempoTranscurridoDesdeSpawn = 2f;
        aplastado = false;
        letraActual = letras[Random.Range(0, letras.Count)];
        Debug.Log(letraActual);
        StartCoroutine(TiempoParaGenerar());
    }

    private IEnumerator TiempoParaGenerar()
    {
        yield return new WaitForSeconds(periodo);
        GenerarLetra();
    }
}
