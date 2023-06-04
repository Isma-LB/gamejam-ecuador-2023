using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarLetras : MonoBehaviour
{
    private List<string> letras1 = new List<string>() {"Q","W","E","A","S","D","Z","X","C"};
    private List<string> letras2 = new List<string>() {"I","O","P","J","K","L","N","M"};
    private List<string> letras3 = new List<string>() { "U", "R", "T", "Y","F","G","H","V","B","N" };
    private List<string>[] listaDeListasSegunElNivelDeDificultad;

    private int nivel = 1;
    public string letraForward;
    public string letraUp;
    public string letraDown;

    MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        listaDeListasSegunElNivelDeDificultad = new List<string>[]
        {
            letras1,
            letras2,
            letras3
        };
        GenerarLetra();
        mapManager = FindObjectOfType<MapManager>();
    }

    void Update()
    {
        int score = mapManager.GetScores();
        nivel = Mathf.FloorToInt(Mathf.Lerp(0,9,score) * 3) ;
    }
    public string GenerarLetra()
    {
        int random = Random.Range(0, nivel);
        string letraNueva = listaDeListasSegunElNivelDeDificultad[random][Random.Range(0, listaDeListasSegunElNivelDeDificultad[random].Count)];
        return letraNueva;
    }
    public void CleanLetras(){
        letraForward = "";
        letraDown = "";
        letraUp = "";
    }

    internal bool ValidKey(string key)
    {
        return key == letraUp || key == letraForward || key == letraDown;
    }
}