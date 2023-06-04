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
        if(letraCorrecta){
            Debug.Log("next");
            if(wasRightHand){
                rightHand.position = nextTarget.transform.position;
            }
            else{
                leftHand.position = nextTarget.transform.position;
            }
            wasRightHand = !wasRightHand;
            nextTarget.Visit();
            letraCorrecta= false;
            EnergyScript.Instancia.IncreaseEnergy(0.05f);
            //Debug.Log(Time.time - scriptLetras.tiempoTranscurridoDesdeSpawn);
            //scriptLetras.tiempoTranscurridoDesdeSpawn = Time.time;

        }
    }
    
    void DetectTargets(){
        targets = Physics2D.OverlapCircleAll(transform.position + circlePos, circleRadius, handTargetLayer);
        if(targets.Length > 0){
            nextTarget = targets[0].GetComponent<HandTarget>();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + circlePos, circleRadius);
    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (Input.anyKeyDown && e.keyCode.ToString() != "None")
        {
            if(e.keyCode.ToString() == scriptLetras.letraActual && !scriptLetras.aplastado)
            {
                letraCorrecta = true;
                scriptLetras.aplastado = true;
                Debug.Log(scriptLetras.tiempoTranscurridoDesdeSpawn);
                scriptLetras.GenerarLetra();
                //EnergyScript.Instancia.IncreaseEnergy(0.1f);
            }
        }
    }
}
