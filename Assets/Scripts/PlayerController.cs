using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform leftHand = null;    
    [SerializeField] Transform rightHand = null;

    [Header("Detection circle")]
    [SerializeField] LayerMask handTargetLayer = 0;
    [SerializeField] Vector3 circlePos = Vector3.zero;
    [SerializeField] float circleRadius = 5;


    Collider2D[] targets; 
    bool wasRightHand = false;
    HandTarget nextTarget = null;

    [SerializeField] GenerarLetras scriptLetras;
    bool letraCorrecta = false;

    // Update is called once per frame
    void Update()
    {
        DetectTargets();
        if(Input.anyKeyDown){
            string currentKey = GetCurrentKey();
            if(scriptLetras.ValidKey(currentKey) || Input.GetKeyDown(KeyCode.Space)){
                bool secondOption = nextTarget.key != currentKey;
                MoveHandToNext(secondOption);
                EnergyScript.Instancia.IncreaseEnergy(0.05f);
            }
        }
    }

    private string GetCurrentKey()
    {
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(key)){
                return key.ToString();
            }
        }
        return "No key was pressed";
    }

    void MoveHandToNext(bool secondOption){
        if(nextTarget == null) return;

        if(wasRightHand){
            rightHand.position = secondOption?  nextTarget.secondOption.transform.position : nextTarget.transform.position;
        }
        else{
            leftHand.position = secondOption?  nextTarget.secondOption.transform.position : nextTarget.transform.position;
        }
        wasRightHand = !wasRightHand;
        nextTarget.Visit();
        nextTarget = null;
    }
    void DetectTargets(){
        targets = Physics2D.OverlapCircleAll(transform.position + circlePos, circleRadius, handTargetLayer);
        
        // no target at reach just skip
        if(targets.Length <= 0) return;
        
        
        // get the closest target
        float minDistance = float.MaxValue;
        int targetIndex = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector3.SqrMagnitude(transform.position + circlePos - targets[i].transform.position);
            if(distance < minDistance){
                minDistance = distance;
                targetIndex = i;
            }
        }
        
        nextTarget = targets[targetIndex].GetComponent<HandTarget>();
        
        if(nextTarget.key == ""){
            nextTarget.key = scriptLetras.GenerarLetra();
            if(nextTarget.secondOption != null && nextTarget.secondOption.key == ""){
                nextTarget.secondOption.key = scriptLetras.GenerarLetra();
            }
        }
        UpdateUI(nextTarget);
    }
    void UpdateUI(HandTarget target){
        scriptLetras.CleanLetras();
        if(target.up){
            scriptLetras.letraUp = target.key;
        }
        else if(nextTarget.down){
            scriptLetras.letraDown = target.key;
        }
        else {
            scriptLetras.letraForward = target.key;
        }
        if(target.secondOption){
            if(target.secondOption.up){
                scriptLetras.letraUp = target.secondOption.key;
            }
            else if(target.secondOption.down){
                scriptLetras.letraDown = target.secondOption.key;
            }
            else {
                scriptLetras.letraForward = target.secondOption.key;
            }
        }
        //Debug.Log("Letras up:" + scriptLetras.letraUp + " down:" + scriptLetras.letraDown + "fwr: " + scriptLetras.letraForward);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + circlePos, circleRadius);
    }
    // private void OnGUI()
    // {
    //     Event e = Event.current;
    //     if (Input.anyKeyDown && e.keyCode.ToString() != "None")
    //     {
    //             Debug.Log("Second Target"+nextTarget.secondOption);
    //         if(e.keyCode.ToString() == scriptLetras.letraActual)
    //         {
    //             letraCorrecta = true;
    //             Debug.Log(scriptLetras.tiempoTranscurridoDesdeSpawn);
    //             scriptLetras.GenerarLetra();
    //             //EnergyScript.Instancia.IncreaseEnergy(0.1f);
    //         }
    //     }
    // }
}
