using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public Rigidbody rigidbody;
    //public GameObject camera;
    public float maxSpeed = 4;
    public float moveSpeed = 1;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //camera.transform.position = this.transform.position + new Vector3(-4, 3, 0);
        //camera.transform.LookAt(this.transform);

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
        }
        if (rigidbody.velocity.magnitude >= maxSpeed)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
    }
}
