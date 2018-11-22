using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject _floor;
    public GameObject[] _deathWall;
    public int _totalTime = 210;// 180 120 60 縮圈
    public int _currentTime = 210;


	// Use this for initialization
	void Start () {
        StartCoroutine(Discount());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_currentTime <= 180 && _currentTime >= 150) //一階段縮圈 scale(10,10) -> scale(7.5,7.5)  
        {
            float delta = (_currentTime - 150) / 30f;
            _floor.transform.localScale = new Vector3(7.5f + delta * 2.5f, 1f, 7.5f + delta * 2.5f);

            _deathWall[0].transform.localPosition = new Vector3(0f, 50f, -38f + delta * -12.5f);
            _deathWall[1].transform.localPosition = new Vector3(0f, 50f, 38f + delta * 12.5f);
            _deathWall[2].transform.localPosition = new Vector3(38f + delta * 12.5f, 50f, 0f);
            _deathWall[3].transform.localPosition = new Vector3(-38f + delta * -12.5f, 50f, 0f);
        }
        else if (_currentTime <= 120 && _currentTime >= 90)//二階段縮圈 scale(7.5,7.5) -> scale(5,5)
        {
            float delta = (_currentTime - 90) / 30f;
            _floor.transform.localScale = new Vector3(5f + delta * 2.5f, 1f, 5f + delta * 2.5f);

            _deathWall[0].transform.localPosition = new Vector3(0f, 50f, -25.5f + delta * -12.5f);
            _deathWall[1].transform.localPosition = new Vector3(0f, 50f, 25.5f + delta * 12.5f);
            _deathWall[2].transform.localPosition = new Vector3(25.5f + delta * 12.5f, 50f, 0f);
            _deathWall[3].transform.localPosition = new Vector3(-25.5f + delta * -12.5f, 50f, 0f);
        }
        else if (_currentTime <= 60 && _currentTime >= 30)//三階段縮圈 scale(5,5) -> scale(3,3)
        {
            float delta = (_currentTime - 30) / 30f;
            _floor.transform.localScale = new Vector3(3f + delta * 2f, 1f, 3f + delta * 2f);

            _deathWall[0].transform.localPosition = new Vector3(0f, 50f, -15.5f + delta * -10f);
            _deathWall[1].transform.localPosition = new Vector3(0f, 50f, 15.5f + delta * 10f);
            _deathWall[2].transform.localPosition = new Vector3(15.5f + delta * 10f, 50f, 0f);
            _deathWall[3].transform.localPosition = new Vector3(-15.5f + delta * -10f, 50f, 0f);
        }
        else if (_currentTime == 0)//結算面板
        {

        }


	}

    IEnumerator Discount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _currentTime -= 1;
        }
    }

}
