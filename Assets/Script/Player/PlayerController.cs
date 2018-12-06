using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    //Player Info
    public int PlayerId; // 0 1 2 3

    //Kill Info :　LastCollisionPlayer若為自身　或者　LastCollisionTime = -1  算是自殺
    public int LastCollisionPlayer;
    public float LastCollisionTime = 10f;

    public Transform Camera;
    public float ForwardSpeed = 1;//向前移動速度
    public float MaxSpeed = 1;//最高速度
    public float SprintSpeed = 20;//衝刺速度
    public float SprintCountDown = 10;//衝刺CD時間
    public ParticleSystem ParticleSprint;
    public ParticleSystem ParticlePower;
    public bool DontCollision
    {
        get { return _dontCollision; }
        set { _dontCollision = value; }
    }
    public Point point;//point prefeb
    [HideInInspector]
    public List<Point> Points;

    private bool _dontCollision = false;//會不會被撞飛 true : 不會被撞飛
    private bool _cansprint = true;   //是否可以衝刺(後退時不可衝刺)
    private bool _sprinting = false;//是否衝刺CD
    private bool _sprintspeedDown = true;//是否正在限速
    private bool _collisionspeedDown = true;//是否正在碰撞限速
    private bool _collision = false;//是否被撞飛(被撞到不能控制)
    private bool _flying = false;//是否離地(離地不能控制)


    private RaycastHit hit;
    private Rigidbody rby;

    void Start()
    {
        rby = GetComponent<Rigidbody>();
        StartCoroutine(SprintingCountDown(1));
        LastCollisionPlayer = PlayerId;//初始化為自己
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        RaycastHit hit;//判斷離地
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 0.9f))
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
            if (_sprinting && _cansprint)//移動中可衝刺
            {
                rby.AddForce(-dir * Time.deltaTime * SprintSpeed);
                StartCoroutine(SprintingCountDown(SprintCountDown));
                StartCoroutine(SprintSpeedDownCountDown(1));
            }
        }
        else
        {
            ParticlePower.Stop();
        }
        //操作
        if (!_collision & !_flying)
        {
            _cansprint = true;

            if (Input.GetKey(KeyCode.W))
            {
                rby.AddForce(-dir * Time.deltaTime * ForwardSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _cansprint = false;
                rby.AddForce(dir * Time.deltaTime * ForwardSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rby.AddForce(-dir * Time.deltaTime * ForwardSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
                rby.AddForce(dir * Time.deltaTime * ForwardSpeed);
            }

        }
        if (rby.velocity.magnitude >= MaxSpeed)
        {

            if (!_collisionspeedDown)
            {

            }
            else if (!_sprintspeedDown)
            {
                //particle pos
                Vector3 tmpdir = rby.velocity.normalized;
                ParticleSprint.transform.position = transform.position + tmpdir.normalized * 0.5f;
                ParticleSprint.transform.forward = -tmpdir;
                if (rby.velocity.magnitude >= MaxSpeed * 2.5f)
                    rby.velocity = rby.velocity.normalized * MaxSpeed * 2.5f;
            }
            else
            {
                rby.velocity = rby.velocity.normalized * MaxSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_dontCollision)
        {
            StartCoroutine(CollisionSpeedDownCountDown(1));//解除限速
            StartCoroutine(CollisionCountDown(1));//不能控制
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            float rate = collision.rigidbody.velocity.magnitude / (rby.velocity.magnitude + collision.rigidbody.velocity.magnitude);
            rby.AddForce(-direction * rate * 4, ForceMode.Force);

            //更新碰撞紀錄
            if (collision.gameObject.GetComponent<PlayerController>().PlayerId != this.LastCollisionPlayer)
            {
                this.LastCollisionPlayer = collision.gameObject.GetComponent<PlayerController>().PlayerId;
                StartCoroutine(LastCollisonTimeDiscount());
            }
        }
       
    }

    //增加幾個point(王冠)
    public void AddPoint(int _point)
    {
        for (int i = 0; i < _point; i++)
        {
            Point tmp = GameObject.Instantiate(point);
            tmp.Tartget = this.gameObject;
            tmp.Number = Points.Count;
            Points.Add(tmp);
        }
    }

    IEnumerator SprintSpeedDownCountDown(float _time)//限速
    {
        _sprintspeedDown = false;
        if (!ParticleSprint.isPlaying)
            ParticleSprint.Play();
        yield return new WaitForSeconds(_time);
        ParticleSprint.Stop();
        _sprintspeedDown = true;
    }
    IEnumerator CollisionSpeedDownCountDown(float _time)//限速
    {
        _collisionspeedDown = false;
        yield return new WaitForSeconds(_time);
        _collisionspeedDown = true;
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

    IEnumerator LastCollisonTimeDiscount()
    {
        yield return new WaitForSeconds(2f);
        this.LastCollisionPlayer = -1;
    }

}
