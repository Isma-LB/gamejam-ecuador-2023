using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTarget : MonoBehaviour
{
    Collider2D col2D;
    void Awake()
    {
        col2D = GetComponent<Collider2D>();
        if(visited){
            col2D.enabled = false;
        }
    }
    public bool up = false;
    public bool down = false;
    public HandTarget secondOption;
    public string key = "";
    [SerializeField] bool visited = false;
    public void Visit(){
        visited = true;
        col2D.enabled = false;
    }  
}
