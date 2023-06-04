using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    const string PLAYER_PREFS_KEY = "tutorial";
    void Awake()
    {
        bool tutorialPassed = PlayerPrefs.GetInt(PLAYER_PREFS_KEY, 0) != 0;
        if(tutorialPassed){
            LoadLevel();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        LoadLevel();
    }

    void LoadLevel(){
        //load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
