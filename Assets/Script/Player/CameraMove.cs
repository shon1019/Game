using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float High = 1;//相機高度
    public float XSpeed = 1;//調整視角左右速度
    public float Distance = 1;//背後相機多遠

    private float _x;
    private Quaternion rotationEugler;
    private Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        //死亡固定俯視相機
        if(!target.gameObject.active)
        {        
            this.transform.position = new Vector3(0, 70, 0);
            this.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.J))
                _x -= XSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.K))
                _x += XSpeed * Time.deltaTime;

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
