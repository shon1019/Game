using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject _deathWall;
    public int _totalTime = 210;    // 180 120 60 縮圈
    public int _currentTime = 210;
    private Vector2 rand_pos;
    private Vector3 pos;
	void Start () {
        StartCoroutine(Discount());
	}
	
	void Update ()
    {
        if (_currentTime == 180 || _currentTime == 120 || _currentTime == 60) {
            rand_pos = RandomCenter(20);
            pos = _deathWall.transform.position;
        }

        if (_currentTime <= 180 && _currentTime >= 150)     //  一階段縮圈 scale(11000,11000) -> scale(8000,8000)  
        {
            float delta = (_currentTime - 150) / 30f;
            _deathWall.transform.localScale = Vector3.Lerp(new Vector3(8000, 3500, 8000), new Vector3(11000, 3500, 11000), delta);
            _deathWall.transform.position = Vector3.Lerp(new Vector3(rand_pos.x, 0, rand_pos.y), pos, delta);
        }
        else if (_currentTime <= 120 && _currentTime >= 90) //  二階段縮圈 scale(8000,8000) -> scale(6000,6000)
        {
            float delta = (_currentTime - 90) / 30f;
            _deathWall.transform.localScale = Vector3.Lerp(new Vector3(6000, 3500, 6000), new Vector3(8000, 3500, 8000), delta);
            _deathWall.transform.position = Vector3.Lerp(new Vector3(rand_pos.x, 0, rand_pos.y), pos, delta);
        }
        else if (_currentTime <= 60 && _currentTime >= 30)  //  三階段縮圈 scale(6000,6000) -> scale(4000,4000)
        {
            float delta = (_currentTime - 30) / 30f;
            _deathWall.transform.localScale = Vector3.Lerp(new Vector3(4000, 3500, 4000), new Vector3(6000, 3500, 6000), delta);
            _deathWall.transform.position = Vector3.Lerp(new Vector3(rand_pos.x, 0, rand_pos.y), pos, delta);
        }
        else if (_currentTime == 0) //  結算面板
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

    private Vector2 RandomCenter(int length) {
        int x = Random.Range(-length, length);
        int z = Random.Range(-length, length);
        return new Vector2(x, z);
    }
}
