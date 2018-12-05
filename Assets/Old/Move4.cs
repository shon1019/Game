using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move4 : MonoBehaviour {

    public Transform camera;
    public float Power = 0;
    public int MaxPower = 100;
    public int MinPower = 0;
    public float StopThreshhold = 1;
    public float ControlSpeed = 0.001f;
    public float SpeedDown = 1;
    private float Force = 0;
    private bool state = false;//上升||下降
    private Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = this.transform.position - camera.position ;
        dir.y = 0;
        dir = dir.normalized;
        Debug.Log(rigidbody.velocity.magnitude);
        if (rigidbody.velocity.magnitude< StopThreshhold)
        {
            if (Input.GetButton("Jump"))
            {
                if (!state)
                    Force += Power * Time.deltaTime;
                else
                    Force -= Power * Time.deltaTime;
                if (Force > MaxPower)
                    state = true;
                else if (Force < MinPower)
                {
                    state = false;
                    Force = 0;
                }

                Debug.Log(Force);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                Debug.Log("Go");
                rigidbody.AddForce(Force * dir * Time.deltaTime);
            }
            //強制速度遞減
            rigidbody.velocity -= rigidbody.velocity.normalized * SpeedDown * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                rigidbody.AddForce(dir * Time.deltaTime * ControlSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rigidbody.AddForce(dir * Time.deltaTime * ControlSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rigidbody.AddForce(-dir * Time.deltaTime * ControlSpeed);
            }

        }
    }
}
