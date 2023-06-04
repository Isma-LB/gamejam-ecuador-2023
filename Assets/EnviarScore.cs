using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;

public class EnviarScore : MonoBehaviour
{

    public GameObject nombreInput;
    public MapManager mapManagerScript;


    // Update is called once per frame

    [ContextMenu("Enviar score a api")]
    public void EnviarScoreApi()
    {
        StartCoroutine(SendPostRequest());
    }
    private IEnumerator SendPostRequest()
    {
        // Define the URL and JSON data
        string url = "https://score-game-jam-azure.vercel.app/api/scores";

        string name = nombreInput.GetComponent<TMP_InputField>().text;
        int score = mapManagerScript.GetScores();

        string jsonData = $"{{\"name\": \"{name}\", \"score\": {score}}}";

        var bytes = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        
        webRequest.uploadHandler = new UploadHandlerRaw(bytes);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
           
        webRequest.SetRequestHeader("accept", "application/json");
        webRequest.SetRequestHeader("Content-Type", "application/json");


         // Send the request
        yield return webRequest.SendWebRequest();


        // Check for errors
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST request failed: " + webRequest.error);
        }
        else
        {
            Debug.Log("POST request successful");
            SceneManager.LoadScene("PlayerScores");
        }
    }
}
