using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarLetras : MonoBehaviour
{
    private List<string> letras1 = new List<string>() {"Q","W","E","A","S","D","Z","X","C"};
    private List<string> letras2 = new List<string>() {"I","O","P","J","K","L","N","M"};
    private List<string> letras3 = new List<string>() { "U", "R", "T", "Y","F","G","H","V","B","N" };
    private List<string>[] listaDeListasSegunElNivelDeDificultad;

    private int nivel = 3;

    [SerializeField] float periodo = 2f;
    public string letraActual = null;
    public bool aplastado = false;
    public float tiempoTranscurridoDesdeSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        listaDeListasSegunElNivelDeDificultad = new List<string>[]
        {
            letras1,
            letras2,
            letras3
};
        tiempoTranscurridoDesdeSpawn = Time.time;
        GenerarLetra();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerarLetra()
    {
        aplastado = false;
        int random = Random.Range(0, nivel);
        letraActual = listaDeListasSegunElNivelDeDificultad[random][Random.Range(0, listaDeListasSegunElNivelDeDificultad[random].Count)];
        Debug.Log(letraActual);
        
    }
}
