using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetScores : MonoBehaviour
{
    [System.Serializable]
    public class PlayerScore
    {
        public int id;
        public int score;
        public int position;
        public string name;
        public string createdAt;

        // method to deserialize the JSON data that contains an array of players
        public static PlayerScore[] FromJson(string json)
        {
            // use the JsonUtility class to convert the JSON data to a PlayerScore array
            return JsonUtility.FromJson<PlayerScore[]>(json);
        }
    }

    [System.Serializable]
    public class PlayerScoreList
    {
        public PlayerScore[] scores;

        // method to deserialize the JSON data that contains an array of players
        public static PlayerScoreList FromJson(string json)
        {
            // use the JsonUtility class to convert the JSON data to a PlayerScore array
            return JsonUtility.FromJson<PlayerScoreList>(json);
        }

    }

    public PlayerScore[] players = null;
    public Text scoresText;


    private void Start()
    {
        GetScoresApi();
    }
    [ContextMenu("Get Scores from APi")]
    public void GetScoresApi()
    {
        StartCoroutine(GetJsonData());
    }

    private IEnumerator GetJsonData()
    {
        // Define the URL
        string url = "https://score-game-jam-azure.vercel.app/api/scores";

        // Create the UnityWebRequest object
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("GET request failed: " + request.error);
        }
        else
        {
            // Parse the JSON data
            string jsonData = request.downloadHandler.text;
            Debug.Log(jsonData);
            players = PlayerScoreList.FromJson(jsonData).scores;


            string scoresString = "";

            foreach (PlayerScore player in players)
            {
                scoresString += player.position + " - " + player.name + " - " + player.score + "pts" + "\n";
            }

            scoresText.text = scoresString;

        }
    }

    public void IrAMenuInicial()
    {
        Debug.Log("Al menu inicial");
        SceneManager.LoadScene("MenuInicio");
    }

}
