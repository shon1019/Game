using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Info
    public int PlayerId; // 0 1 2 3
    public int Money = 0;

    //Kill Info :　LastCollisionPlayer若為自身　或者　LastCollisionTime = -1  算是自殺
    public int LastCollisionPlayer;
    public float LastCollisionTime = 10f;

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

    public bool  DontCollision
    {
        get{ return _dontCollision; }
        set { _dontCollision = value; }
    }

    private bool _dontCollision = true;//會不會被撞飛 true : 不會被撞飛
    private bool _cansprint = true;   //是否可以衝刺(後退時不可衝刺)
    private bool _isPowering = false;//是否集氣中(集氣中不能控制)
    private bool _sprinting = false;//是否衝刺CD
    private bool _sprintspeedDown = true;//是否正在限速
    private bool _collisionspeedDown = true;//是否正在限速
    private bool _collision = false;//是否被撞飛(被撞到不能控制)
    private bool _flying = false;//是否離地(離地不能控制)

    private RaycastHit hit;
    private Rigidbody rby;
    private AfterImageEffects _afterImageEffects;

    void Start()
    {
        rby = GetComponent<Rigidbody>();
        _afterImageEffects = GetComponent<AfterImageEffects>();
        StartCoroutine(SprintingCountDown(1));

        LastCollisionPlayer = PlayerId;//初始化為自己
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Flying" + _flying);
        RaycastHit hit;//判斷離地
        if (Physics.Raycast(transform.position - new Vector3(0, 0.1f, 0), -new Vector3(0, 1, 0), out hit, 0.5f))
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
        else
        {
            _flying = true;
        }


        Vector3 dir = Camera.position - this.transform.position;
        dir.y = 0;
        dir = dir.normalized;

        //集氣
        if (Input.GetButton("Jump"))
        {
            if (rby.velocity.magnitude < StopThreshold)//集氣
            {
                PlayerPower.Add(PowerUpSpeed * Time.deltaTime);
                _isPowering = true;
            }
            else if (_sprinting && PlayerPower.CurrentPower >= SprintPower && _cansprint)//移動中可衝刺
            {
                rby.AddForce(-dir * Time.deltaTime * SprintSpeed);
                PlayerPower.Sub(SprintPower);
                StartCoroutine(SprintingCountDown(SprintCountDown));
                StartCoroutine(SprintSpeedDownCountDown(1));
            }
        }

        //操作
        if (PlayerPower.CurrentPower > 0 && !_isPowering && !_collision & !_flying)
        {
            _cansprint = true;
            bool _isMoving = false;
            if (Input.GetKey(KeyCode.W))
            {
                rby.AddForce(-dir * Time.deltaTime * ForwardSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _cansprint = false;
                rby.AddForce(dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rby.AddForce(-dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rby.AddForce(dir * Time.deltaTime * RightSpeed);
                _isMoving = true;
            }
            if (_isMoving)
            {
                PlayerPower.Sub(PowerDownSpeed * Time.deltaTime);
            }

        }
        _isPowering = false;
        if (rby.velocity.magnitude >= MaxSpeed)
        {

            if (!_collisionspeedDown)
            {
              
            }
            else if (!_sprintspeedDown)
            {
                if (rby.velocity.magnitude >= MaxSpeed * 2.5f)
                    rby.velocity = rby.velocity.normalized * MaxSpeed * 2.5f;
                //出現影分身
                if (rby.velocity.magnitude >= ShadowShowSpeed)
                    _afterImageEffects.Open = true;
                else
                    _afterImageEffects.Open = false;
            }
            else
            {
                rby.velocity = rby.velocity.normalized * MaxSpeed;
                _afterImageEffects.Open = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"&& !_dontCollision)
        {
            StartCoroutine(CollisionSpeedDownCountDown(1));//解除限速
            StartCoroutine(CollisionCountDown(1));//不能控制
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            float rate = collision.rigidbody.velocity.magnitude / (rby.velocity.magnitude + collision.rigidbody.velocity.magnitude);
            rby.AddForce(-direction * rate * 4, ForceMode.Force);

            //更新碰撞紀錄
            if (collision.gameObject.GetComponent<PlayerController>().PlayerId != this.LastCollisionPlayer) {
                this.LastCollisionPlayer = collision.gameObject.GetComponent<PlayerController>().PlayerId;
                this.LastCollisionTime = 10f;
            }
        }
    }

    //限速
    IEnumerator SprintSpeedDownCountDown(float _time)
    {
        _sprintspeedDown = false;
        yield return new WaitForSeconds(_time);
        _sprintspeedDown = true;
    }

    //限速
    IEnumerator CollisionSpeedDownCountDown(float _time)
    {
        _collisionspeedDown = false;
        yield return new WaitForSeconds(_time);
        _collisionspeedDown = true;
    }

    //衝刺CD
    IEnumerator SprintingCountDown(float _time)
    {
        _sprinting = false;
        yield return new WaitForSeconds(_time);
        _sprinting = true;

    }

    //collision
    IEnumerator CollisionCountDown(float _time)
    {
        _collision = true;
        yield return new WaitForSeconds(_time);
        _collision = false;

    }


    IEnumerator LastCollisonTimeDiscount()
    {
        while (LastCollisionTime >= 0)
        {
            yield return new WaitForSeconds(1f);
            LastCollisionTime -= 1f;
        }
    }

}
