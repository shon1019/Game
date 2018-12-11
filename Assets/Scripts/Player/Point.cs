using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    public GameObject Tartget
    {
        get { return _target; }
        set { _target = value; }
    }

    public int Number
    {
        get { return _number; }
        set { _number = value; }
    }

    private GameObject _target;
    private float _lerp;
    private int _number = 0;
    // Use this for initialization
    void Start()
    {
        _lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(_lerp>=1)
        {
            Vector3 targetPos = _target.transform.position;
            targetPos.y += 1.6f + 0.15f * _number;
            transform.position = targetPos;
        }
    }

    public IEnumerator ChangeTarget(GameObject target)
    {
        _lerp = 0;
        _target.GetComponent<PlayerController>().Points.Remove(this);//刪掉原本的玩冠
        int number = target.GetComponent<PlayerController>().Points.Count;//*******************
        target.GetComponent<PlayerController>().Points.Add(this);
       
        _number = number;
        _target = target;
        while (_lerp < 1)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y += 1.6f + 0.15f * number;
            transform.position = Vector3.Lerp(transform.position, targetPos, _lerp);
            yield return new WaitForSeconds(0.1f);
            _lerp += 0.1f;
        }

    }

}
