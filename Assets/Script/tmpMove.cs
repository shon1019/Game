using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpMove : MonoBehaviour
{

    public Transform Camera;
    public Power PlayerPower;
    public float PowerUpSpeed = 1;
    public float PowerDownSpeed = 1;
    public float ForwardSpeed = 1;
    public float RightSpeed = 1;
    public float MaxSpeed = 1;
    public float StopThreshold = 2;
    public float SprintSpeed = 20;
    public float SprintCountDown = 10;
    public float SprintPower = 10;

    private bool _isPowering = false;
    private bool _sprinting = false;
    private bool _speedDown = true;
    private bool _collision = false;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(SprintingCountDown(1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = Camera.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;

        //集氣
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            if (rigidbody.velocity.magnitude < StopThreshold)
            {
                PlayerPower.Add(PowerUpSpeed * Time.deltaTime);
                _isPowering = true;

            }
            else if (_sprinting && PlayerPower.CurrentPower >= SprintPower)
            {
                rigidbody.AddForce(-dir * Time.deltaTime * SprintSpeed, ForceMode.Force);
                PlayerPower.Sub(SprintPower);
                StartCoroutine(SprintingCountDown(SprintCountDown));
                StartCoroutine(SpeedDownCountDown(1));

            }
        }

        //操作
        if (PlayerPower.CurrentPower > 0 && !_isPowering)
        {
            bool _isMoving = false;
            if (Input.GetKey(KeyCode.Keypad8))
            {
                rigidbody.AddForce(-dir * Time.deltaTime * ForwardSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.Keypad5))
            {
                rigidbody.AddForce(dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rigidbody.AddForce(-dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rigidbody.AddForce(dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (_isMoving)
            {
                PlayerPower.Sub(PowerDownSpeed * Time.deltaTime);
            }

        }
        _isPowering = false;
        Debug.Log(_speedDown);
        if (rigidbody.velocity.magnitude >= MaxSpeed)
        {
            if (!_speedDown)
            {
              
            }
            else
            {
                rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(SpeedDownCountDown(2));//解除限速
            StartCoroutine(CollisionCountDown(2));//所玩家操作
            Debug.Log("start" + _speedDown);
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            float rate = collision.rigidbody.velocity.magnitude / (rigidbody.velocity.magnitude + collision.rigidbody.velocity.magnitude);
            rigidbody.AddForce(-direction * rate * 4, ForceMode.Force);  
        }
    }

    IEnumerator SpeedDownCountDown(float _time)//限速
    {

        _speedDown = false;
        yield return new WaitForSeconds(_time);
        _speedDown = true;
    }

    IEnumerator SprintingCountDown(float _time)//限速
    {
        _sprinting = false;
        yield return new WaitForSeconds(_time);
        _sprinting = true;
    }

    IEnumerator CollisionCountDown(float _time)//限速
    {
        _collision = true;
        yield return new WaitForSeconds(_time);
        _collision = false;

    }
}
