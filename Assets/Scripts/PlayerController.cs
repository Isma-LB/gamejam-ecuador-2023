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

    // Update is called once per frame
    void Update()
    {
        DetectTargets();
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("next");
            if(wasRightHand){
                rightHand.position = nextTarget.transform.position;
            }
            else{
                leftHand.position = nextTarget.transform.position;
            }
            wasRightHand = !wasRightHand;
            nextTarget.Visit();
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
}
