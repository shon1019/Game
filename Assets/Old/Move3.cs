using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3 : MonoBehaviour
{
    public Transform camera;
    public float Speed = 0.5f;
    public float maxRotate = 1;
    public float jump = 10;
    Rigidbody rigidbody;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = camera.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;
        
        Debug.Log(Input.GetAxis("Jump"));
        if (Input.GetKey(KeyCode.W))
        {

            rigidbody.AddForce(-dir * Time.deltaTime * Speed);

        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(dir * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
            rigidbody.AddForce(-dir * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
            rigidbody.AddForce(dir * Time.deltaTime * Speed);
        }

        if(Input.GetButton("Jump"))
        {
            rigidbody.AddForce(0,jump * Time.deltaTime , 0);
        }
      
    }
}
