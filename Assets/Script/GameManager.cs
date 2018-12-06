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
    public ParticleSystem Reviveparticle;

    //玩家是否存活
    private bool _treasureAppera = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        //生成寶箱
        if (_treasureAppera)
        {
            Vector3 tmpPoint;
            if (_currentTime <= 180 && _currentTime >= 150)
                tmpPoint = new Vector3(Random.Range(40, 90), 300, Random.Range(40, 90));
            else if (_currentTime <= 120 && _currentTime >= 90)
                tmpPoint = new Vector3(Random.Range(40, 65), 300, Random.Range(40, 65));
            else 
                tmpPoint = new Vector3(Random.Range(0, 30), 300, Random.Range(0, 30));
            GameObject.Instantiate(Treasure, tmpPoint, new Quaternion(0, 0, 0, 0));
            _treasureAppera = false;
            StartCoroutine(TreasureAppear());
        }


        if (_currentTime <= 180 && _currentTime >= 150) //一階段縮圈 scale(10000,10000) -> scale(8000,8000)  
        {
            float delta = (_currentTime - 150) / 30f;
            _deathWall.transform.localScale = new Vector3(8000f + delta * 2000f, 3500f, 8000f + delta * 2000f);
            BackGroundMusic.pitch = 1.1f;

        }
        else if (_currentTime <= 120 && _currentTime >= 90)//二階段縮圈 scale(8000,8000) -> scale(6000,6000)
        {
            float delta = (_currentTime - 90) / 30f;
            _deathWall.transform.localScale = new Vector3(6000f + delta * 2000f, 3500f, 6000f + delta * 2000f);
            BackGroundMusic.pitch = 1.3f;
        }
        else if (_currentTime <= 60 && _currentTime >= 30)//三階段縮圈 scale(6000,6000) -> scale(4000,4000)
        {
            float delta = (_currentTime - 30) / 30f;
            _deathWall.transform.localScale = new Vector3(4000f + delta * 2000f, 3500f, 4000f + delta * 2000f);
            BackGroundMusic.pitch = 1.5f;
        }
        else if (_currentTime == 0)//結算面板
        {

        }


    }

    //重生事件
    public void Revived(int playerID)
    {
        StartCoroutine(Revive(_revivedTime, playerID));
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
        int RanX = Random.Range(-40, 40);
        int RanZ = Random.Range(-40, 40);
        Player[playerID].transform.position = new Vector3(RanX, 2f, RanZ); //設定重生點
        Player[playerID].GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player[playerID].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        Vector3 RevivePos = new Vector3(RanX, -1f, RanZ);
        ParticleSystem tmp = GameObject.Instantiate(Reviveparticle, RevivePos,new Quaternion(-90,0,0,90));
        tmp.Play();

        yield return new WaitForSeconds(t);
        Destroy(tmp.gameObject);
        Player[playerID].SetActive(true);
        
    }

}
