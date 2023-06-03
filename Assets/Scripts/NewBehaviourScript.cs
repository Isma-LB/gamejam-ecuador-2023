using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NewBehaviourScript : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            CameraShakeManager.instance.CameraShake(impulseSource);

        }
    }
}
