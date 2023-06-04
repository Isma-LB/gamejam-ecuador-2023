using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Vector3 input;
    [SerializeField] float speed = 10;
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        transform.position += input * speed * Time.deltaTime;
    }
}
