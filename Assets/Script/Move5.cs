using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move5 : MonoBehaviour
{

    public Transform Camera;
    public Power PlayerPower;
    public float PowerUpSpeed = 1;//集氣速度
    public float PowerDownSpeed = 1;//移動時氣下降速度
    public float ForwardSpeed = 1;//向前移動速度
    public float RightSpeed = 1;//左右&煞車速度
    public float MaxSpeed = 1;//最高速度
    public float StopThreshold = 2;//停下來多少速度可再次集氣
    public float SprintSpeed = 20;//衝刺速度
    public float SprintCountDown = 10;//集氣CD
    public float SprintPower = 10;//衝刺消耗POWER
    public float ShadowShowSpeed = 10;//出現影分身的速度

    private bool _cansprint = true;
    private bool _isPowering = false;//是否集氣中(集氣中不能控制)
    private bool _sprinting = false;//是否衝刺CD
    private bool _speedDown = true;//是否正在限速
    private bool _collision = false;//是否被撞飛(被撞到不能控制)
    private bool _flying = false;//是否離地(離地不能控制)

    private RaycastHit hit;
    private Rigidbody rigidbody;
    private AfterImageEffects _afterImageEffects;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _afterImageEffects = GetComponent<AfterImageEffects>();
        StartCoroutine(SprintingCountDown(1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;//判斷離地
        if(Physics.Raycast(transform.position, new Vector3(0,-1,0), out hit, 2))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _flying = false;
            }
            else
            {
                _flying = true;
            }
        }
        
        Vector3 dir = Camera.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;

        //集氣
        if (Input.GetButton("Jump"))
        {
            if (rigidbody.velocity.magnitude < StopThreshold)//集氣
            {
                PlayerPower.Add(PowerUpSpeed * Time.deltaTime);
                _isPowering = true;   
            }
            else if (_sprinting && PlayerPower.CurrentPower >= SprintPower&& _cansprint)//移動中可衝刺
            {
                rigidbody.AddForce(-dir * Time.deltaTime * SprintSpeed);
                PlayerPower.Sub(SprintPower);
                StartCoroutine(SprintingCountDown(SprintCountDown));
                StartCoroutine(SpeedDownCountDown(1));
            }
        }

        //操作
        if (PlayerPower.CurrentPower > 0 && !_isPowering && !_collision&!_flying)
        {
            _cansprint = true;
            bool _isMoving = false;
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.AddForce(-dir * Time.deltaTime * ForwardSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _cansprint = false;
                rigidbody.AddForce(dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rigidbody.AddForce(-dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
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
        if (rigidbody.velocity.magnitude >= MaxSpeed)
        {
            if (!_speedDown)
            {
                //出現影分身
                if(rigidbody.velocity.magnitude >=ShadowShowSpeed)
                    _afterImageEffects.Open = true;
                else
                    _afterImageEffects.Open = false;
            }
            else
            {
                rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed;
                _afterImageEffects.Open = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(SpeedDownCountDown(2));//解除限速
            StartCoroutine(CollisionCountDown(2));//不能控制
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

    IEnumerator SprintingCountDown(float _time)//衝刺CD
    {
        _sprinting = false;
        yield return new WaitForSeconds(_time);
        _sprinting = true;
       
    }

    IEnumerator CollisionCountDown(float _time)//collisoin
    {
        _collision = true;
        yield return new WaitForSeconds(_time);
        _collision = false;

    }

}
