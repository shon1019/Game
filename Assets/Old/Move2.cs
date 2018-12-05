using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour {
    //public GameObject camera;
    public float maxSpeed = 0.5f;
    public float maxRotate = 1;
    Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //camera.transform.position = this.transform.position + new Vector3(-4, 3, 0);
       // camera.transform.LookAt(this.transform);
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(maxSpeed*Time.deltaTime, 0, 0);
            transform.Rotate(0, 0, -maxRotate);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-maxSpeed * Time.deltaTime, 0, 0);
            transform.Rotate(0, 0, maxRotate);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, maxSpeed * Time.deltaTime);
            transform.Rotate(maxRotate, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, -maxSpeed * Time.deltaTime);
            transform.Rotate(-maxRotate, 0, 0);
        }


        rigidbody.AddForce(0, Input.GetAxis("Jump") * Time.deltaTime, 0);


    }
}
