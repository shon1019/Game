using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Move : MonoBehaviour   
{
    public float speed = 3;
    public Rigidbody rigidbody;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rigidbody.AddForce(Vector3.right * y * speed);
    }
}