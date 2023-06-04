using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCapsUI : MonoBehaviour
{
    [SerializeField] GenerarLetras letrasScript;
    [SerializeField] GameObject keyCapUp;
    [SerializeField] GameObject keyCapDown;
    [SerializeField] GameObject keyCapForward;
    [SerializeField] TextMeshProUGUI keyCapTextUp;
    [SerializeField] TextMeshProUGUI keyCapTextDown;
    [SerializeField] TextMeshProUGUI keyCapTextForward;

    void Update()
    {
        // activate objects
        keyCapUp.SetActive(letrasScript.letraUp != "");
        keyCapDown.SetActive(letrasScript.letraDown != "");
        keyCapForward.SetActive(letrasScript.letraForward != "");
        // update text
        keyCapTextUp.text = letrasScript.letraUp;
        keyCapTextDown.text = letrasScript.letraDown;
        keyCapTextForward.text = letrasScript.letraForward;
    }
}
