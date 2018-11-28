using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject _deathWall;
    public int _totalTime = 210;// 180 120 60 縮圈
    public int _currentTime = 210;


	// Use this for initialization
	void Start () {
        StartCoroutine(Discount());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_currentTime <= 180 && _currentTime >= 150) //一階段縮圈 scale(7000,7000) -> scale(5750,5750)  
        {
            float delta = (_currentTime - 150) / 30f;
            _deathWall.transform.localScale = new Vector3(5750f + delta * 1250f, 3500f, 5750f + delta * 1250f);


        }
        else if (_currentTime <= 120 && _currentTime >= 90)//二階段縮圈 scale(5750,5750) -> scale(4500,4500)
        {
            float delta = (_currentTime - 90) / 30f;
            _deathWall.transform.localScale = new Vector3(4500f + delta * 1250f, 3500f, 4500f + delta * 1250f);

        }
        else if (_currentTime <= 60 && _currentTime >= 30)//三階段縮圈 scale(4500,4500) -> scale(3500,3500)
        {
            float delta = (_currentTime - 30) / 30f;
            _deathWall.transform.localScale = new Vector3(3500f + delta * 1000f, 3500f, 3500f + delta * 1000f);

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
