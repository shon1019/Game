using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject _deathWall;

    public int _totalTime = 210;// 180 120 60 縮圈
    public int _currentTime = 210;
    public int _revivedTime = 5;

    public GameObject Player;
    public GameObject Treasure;
    public Transform []TreasurePoint;

    //玩家是否存活
    public bool _IsPlayerSurvived = true;
    private bool _treasureAppear = false;
    



    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Discount());
        StartCoroutine(TreasureAppear());

        _treasureAppear = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_treasureAppear)
        {
            int ran = Random.Range(0, TreasurePoint.Length);
            Vector3 tmpPoint = TreasurePoint[ran].position;
            GameObject.Instantiate(Treasure, tmpPoint, new Quaternion(-90, 0, 0, 0));
            _treasureAppear = false;
            StartCoroutine(TreasureAppear());
        }

        if (_currentTime <= 180 && _currentTime >= 150) //一階段縮圈 scale(10000,10000) -> scale(8000,8000)  
        {
            float delta = (_currentTime - 150) / 30f;
            _deathWall.transform.localScale = new Vector3(8000f + delta * 2000f, 3500f, 8000f + delta * 2000f);


        }
        else if (_currentTime <= 120 && _currentTime >= 90)//二階段縮圈 scale(8000,8000) -> scale(6000,6000)
        {
            float delta = (_currentTime - 90) / 30f;
            _deathWall.transform.localScale = new Vector3(6000f + delta * 2000f, 3500f, 6000f + delta * 2000f);

        }
        else if (_currentTime <= 60 && _currentTime >= 30)//三階段縮圈 scale(6000,6000) -> scale(4000,4000)
        {
            float delta = (_currentTime - 30) / 30f;
            _deathWall.transform.localScale = new Vector3(4000f + delta * 2000f, 3500f, 4000f + delta * 2000f);

        }
        else if (_currentTime == 0)//結算面板
        {

        }
	}

    //重生事件
    public void Revived()
    {
        StartCoroutine(Revive(_revivedTime));
    }


    IEnumerator Discount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _currentTime -= 1;
        }
    }

    IEnumerator TreasureAppear()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            _treasureAppear = true;
        }
    }

    //重生
    IEnumerator Revive(int t)
    {
        yield return new WaitForSeconds(t);
        Player.SetActive(true);
        Player.transform.position = new Vector3(0, 0f, 0); //設定重生點
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        _IsPlayerSurvived = true;
    }
}
