using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    //Player Info
    public int PlayerId; // 0 1 2 3

    //Kill Info :　LastCollisionPlayer若為自身　算是自殺
    public int LastCollisionPlayer;

    //撞擊時取得自身速度
    public RecordVelocity rv;

    public Transform Camera;
    public float BaseForce = 0;
    public float ForceRate = 7;
    public float ForwardSpeed = 1;//向前移動速度
    public float MaxSpeed = 1;//最高速度
    public float SprintSpeed = 20;//衝刺速度
    public float SprintCountDown = 10;//衝刺CD時間
    public ParticleSystem ParticleSprint;
    public bool DontCollision
    {
        get { return _dontCollision; }
        set { _dontCollision = value; }
    }
    public Point point;//point prefeb
    [HideInInspector]
    public List<Point> Points;

    public string[] control;

    public GameObject[] PlayerUI;


    private bool _dontCollision = false;//會不會被撞飛 true : 不會被撞飛
    private bool _cansprint = true;   //是否可以衝刺(後退時不可衝刺)
    private bool _sprintspeedDown = true;//是否正在限速
    private bool _collisionspeedDown = true;//是否正在碰撞限速
    private bool _collision = false;//是否被撞飛(被撞到不能控制)
    private bool _flying = false;//是否離地(離地不能控制)
    private float _sprintTime = 9;


    private RaycastHit hit;
    private Rigidbody rby;

    void Start()
    {
        rby = GetComponent<Rigidbody>();
        _sprintTime = 9;
        LastCollisionPlayer = PlayerId;//初始化為自己
    }

    public void Init()
    {
        _collision = false;//是否被撞飛(被撞到不能控制)
        _sprintTime = 9;    //  重設衝刺CD
        _collisionspeedDown = true;
        _sprintspeedDown = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _sprintTime += Time.deltaTime;

        RaycastHit hit;//判斷離地
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1f))
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


        Vector3 dir = this.transform.position - Camera.position;
        dir.y = 0;
        dir = dir.normalized;


        if (Input.GetButton(control[0]))
        {
            if (_sprintTime >= 10 && _cansprint)//移動中可衝刺
            {
                rby.AddForce(dir * Time.deltaTime * SprintSpeed);
                _sprintTime = 0;
                StartCoroutine(SprintSpeedDownCountDown(1));
            }
        }

        //操作
        if (!_collision & !_flying)
        {

            _cansprint = true;
            rby.AddForce(Input.GetAxis(control[2]) * dir * Time.deltaTime * ForwardSpeed);
            dir = Vector3.Cross(dir, new Vector3(0, 1, 0));
            rby.AddForce(Input.GetAxis(control[1]) * -dir * Time.deltaTime * ForwardSpeed);

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
            StartCoroutine(CollisionSpeedDownCountDown(1f));//解除限速
            StartCoroutine(CollisionCountDown(1));//不能控制
            Vector3 direction = (collision.transform.position - transform.position).normalized;

            float rate = collision.gameObject.GetComponent<PlayerController>().rv.GetPlayerVelocity();
            //float rate2 = (rv.GetPlayerVelocity() + collision.gameObject.GetComponent<PlayerController>().rv.GetPlayerVelocity());
            rby.AddForce(-direction * (BaseForce + rate / ForceRate), ForceMode.Force);



            //更新碰撞紀錄
            if (collision.gameObject.GetComponent<PlayerController>().PlayerId != this.LastCollisionPlayer)
            {
                this.LastCollisionPlayer = collision.gameObject.GetComponent<PlayerController>().PlayerId;
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
    public IEnumerator CollisionSpeedDownCountDown(float _time)//限速
    {
        _collisionspeedDown = false;
        yield return new WaitForSeconds(_time);
        _collisionspeedDown = true;
    }


    public IEnumerator CollisionCountDown(float _time)//collisoin
    {
        _collision = true;
        yield return new WaitForSeconds(_time);
        _collision = false;
    }


}
