using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager One;
    public int _totalTime = 210;// 180 120 60 縮圈
    public int _currentTime = 210;
    public int _revivedTime = 5;

    public GameObject _deathWall;

    public GameObject[] Player;

    public AudioSource BackGroundMusic;

    public float TreasureAppearTime = 1;
    public GameObject Treasure;
    public GameObject Barrel;
    public ParticleSystem[] Reviveparticle;
    public GameObject ChampionUI;
    public GameObject ScoreBoard;
    public int Champion
    {
        get { return _champion; }
        set { _champion = value; }
    }
    public GameObject[] PlayerUI;

    private int _champion = 0;
    //玩家是否存活
    private bool _treasureAppera = false;
    private PlayerController[] _playerControllers;
    private Vector2 rand_pos;
    private Vector3 pos;
    private void Awake()
    {
        One = this;
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Discount());
        _treasureAppera = false;
        StartCoroutine(TreasureAppear());
        _playerControllers = new PlayerController[Player.Length];
        for (int i = 0; i < Player.Length; i++)
            _playerControllers[i] = Player[i].GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //排名
        
        int championPoint = 0;
        for (int i=0;i< _playerControllers.Length;i++)
        {
            //Debug.Log("player" + i + "point " + _playerControllers[i].Points.Count+"ch  "+ _champion);
            if (_playerControllers[i].Points.Count > championPoint)
            {
                _champion = i;
            }
               
        }
        ChampionUI.transform.position = Player[_champion].transform.position + new Vector3(0, 10, 0);
 
        for(int i=0;i< PlayerUI.Length;i++)
        {

            if (i == _champion)
            {
                PlayerUI[i].SetActive(false);
            }
                
            else
                PlayerUI[i].SetActive(true);
        }

       


        if (_currentTime == 180 || _currentTime == 120 || _currentTime == 60)
        {
            rand_pos = RandomCenter(20);
            pos = _deathWall.transform.position;
        }

        if (_currentTime <= 180 && _currentTime >= 150)     //  一階段縮圈 scale(11000,11000) -> scale(8000,8000)  
        {
            float delta = (_currentTime - 150) / 30f;
            _deathWall.transform.localScale = Vector3.Lerp(new Vector3(8000, 10000, 8000), new Vector3(10324, 10000, 10324), delta);
            _deathWall.transform.position = Vector3.Lerp(new Vector3(rand_pos.x, 0, rand_pos.y), pos, delta);
        }
        else if (_currentTime <= 120 && _currentTime >= 90) //  二階段縮圈 scale(8000,8000) -> scale(6000,6000)
        {
            float delta = (_currentTime - 90) / 30f;
            _deathWall.transform.localScale = Vector3.Lerp(new Vector3(6000, 10000, 6000), new Vector3(8000, 10000, 8000), delta);
            _deathWall.transform.position = Vector3.Lerp(new Vector3(rand_pos.x, 0, rand_pos.y), pos, delta);
        }
        else if (_currentTime <= 60 && _currentTime >= 30)  //  三階段縮圈 scale(6000,6000) -> scale(4000,4000)
        {
            float delta = (_currentTime - 30) / 30f;
            _deathWall.transform.localScale = Vector3.Lerp(new Vector3(4000, 10000, 4000), new Vector3(6000, 10000, 6000), delta);
            _deathWall.transform.position = Vector3.Lerp(new Vector3(rand_pos.x, 0, rand_pos.y), pos, delta);
        }
        else if (_currentTime == 0) //  結算面板
        {
            //  製作傳遞積分資料
            int[] input = new int[4];
            for (int i = 0; i < 4; i++)
                input[i] = _playerControllers[i].Points.Count;
            //  開啟結算面板
            ScoreBoard.SetActive(true);
            ScoreBoard.GetComponent<Scroeboard>().ShowRecord(input);//  輸入資料
            _currentTime--;                                         //  防呆機制(因為避免重複輸入0)

            /// 停止其他動作等待UI(待辦)
        }

        //生成寶箱
        if (_treasureAppera)
        {
            Vector3 tmpPoint = new Vector3(0, 0, 0);
            float dis;
            if (_currentTime >= 180)
                dis = Random.Range(60, 75);
            else if (_currentTime <= 180 && _currentTime >= 120)
                dis = Random.Range(50, 65);
            else if (_currentTime <= 120 && _currentTime >= 90)
                dis = Random.Range(40, 55);
            else
                dis = Random.Range(0, 30);

            float angle = Random.Range(0, 360);

            tmpPoint.x = Mathf.Cos(angle);
            tmpPoint.z = Mathf.Sin(angle);
            tmpPoint *= dis;
            tmpPoint.y = 200;
            StartCoroutine(TreasureAppear());
            InstanceTreasure(tmpPoint + _deathWall.transform.position);
        }
    }

    //重生事件
    public void Revived(int playerID)
    {
        StartCoroutine(Revive(_revivedTime, playerID));
    }

    //生成寶箱|油桶
    public void InstanceTreasure(Vector3 pos)
    {
        int ran = Random.Range(0, 4);
        if(ran<3)
        {
            GameObject.Instantiate(Treasure, pos, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            GameObject.Instantiate(Barrel, pos, new Quaternion(0, 0, 0, 0));   
        }
        _treasureAppera = false;
    }

    IEnumerator Discount()
    {
        while (_currentTime>0)
        {
            yield return new WaitForSeconds(1f);
            _currentTime -= 1;
        }
    }

    IEnumerator TreasureAppear()
    {

        yield return new WaitForSeconds(TreasureAppearTime);
        _treasureAppera = true;
    }

    //重生
    IEnumerator Revive(int t,int playerID)
    {

        //************************************tmp
        float dis;
        Vector3 tmpPoint = new Vector3(0, 0, 0);
        dis = Random.Range(30, 30);
        float angle = Random.Range(0, 360);
        tmpPoint.x = Mathf.Cos(angle);
        tmpPoint.z = Mathf.Sin(angle);
        tmpPoint *= dis;
        tmpPoint += _deathWall.transform.position;
        tmpPoint.y = 3;
        Player[playerID].transform.position = tmpPoint; //設定重生點
        Player[playerID].GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player[playerID].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        Vector3 RevivePos = tmpPoint;
        ParticleSystem tmp = GameObject.Instantiate(Reviveparticle[playerID], RevivePos,new Quaternion(-90,0,0,90));
        tmp.Play();

        yield return new WaitForSeconds(t);
        Destroy(tmp.gameObject);
        Player[playerID].SetActive(true);
        
    }

    private Vector2 RandomCenter(int length)
    {
        int x = Random.Range(-length, length);
        int z = Random.Range(-length, length);
        return new Vector2(x, z);
    }

}
