using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float High = 1;//相機高度
    public float XSpeed = 1;//調整視角左右速度
    public float Distance = 1;//背後相機多遠
    public string[] Control;

    private float _x;
    private Quaternion rotationEugler;
    private Vector3 cameraPosition;
    private bool dead = false;
    private bool lookDeadParticle = false;

    // Update is called once per frame
    void Update()
    {
        //死亡固定俯視相機
        if (!target.gameObject.active && !dead)
        {
            StartCoroutine(DeadCamera());
        }
        else if (target.gameObject.active)
        {
            dead = false;
        }

        if (!lookDeadParticle)
        {


            if (Input.GetButton(Control[0]))
                _x -= XSpeed * Time.deltaTime;
            if (Input.GetButton(Control[1]))
                _x += XSpeed * Time.deltaTime;

            if (_x > 360)
                _x -= 360;
            else if (_x < 0)
                _x += 360;
            if (dead && !lookDeadParticle)
            {
                rotationEugler = Quaternion.Euler(High + 15, _x, 0);
            }
            else
            {
                rotationEugler = Quaternion.Euler(High, _x, 0);
            }
            cameraPosition = rotationEugler * new Vector3(0, 0, -Distance) + target.position;
            transform.rotation = rotationEugler;
            transform.position = cameraPosition;

        }
    }

    IEnumerator DeadCamera()
    {
        dead = true;
        lookDeadParticle = true;
        yield return new WaitForSeconds(1f);
        lookDeadParticle = false;
    }
}

