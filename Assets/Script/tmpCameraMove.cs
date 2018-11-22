using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpCameraMove : MonoBehaviour {

    public Transform target;
    public float High = 1;
    public float XSpeed = 1;
    public float Distance = 1;

    private float _x;
    private Quaternion rotationEugler;
    private Vector3 cameraPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            this.transform.position = new Vector3(0, 70, 0);
            this.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
                _x += XSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftArrow))
                _x -= XSpeed * Time.deltaTime;

            if (_x > 360)
                _x -= 360;
            else if (_x < 0)
                _x += 360;

            rotationEugler = Quaternion.Euler(High, _x, 0);
            cameraPosition = rotationEugler * new Vector3(0, 0, -Distance) + target.position;
            transform.rotation = rotationEugler;
            transform.position = cameraPosition;
        }
    }
}
