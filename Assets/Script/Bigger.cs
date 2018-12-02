using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigger : MonoBehaviour {

    public float MaxSize=1;
    public float BiggerSpeed=1;
    public float Force=1;
    public float Destorytime = 2;

    private float _nowSize = 1;
    private bool _isDestorying = false;


    private void Start()
    {
        _isDestorying = false;
    }

    // Update is called once per frame
    void Update () {
        
        if (_nowSize>=MaxSize&& !_isDestorying)
        {
            StartCoroutine(destory(Destorytime));
            _isDestorying = true;
        }
        else if(!_isDestorying)
        {
            float tmpBiggerSpead = BiggerSpeed * Time.deltaTime;
            transform.localScale += new Vector3(tmpBiggerSpead, tmpBiggerSpead, tmpBiggerSpead);
            _nowSize += BiggerSpeed * Time.deltaTime;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(direction.normalized * Force);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(direction.normalized * Force);
        }
    }

    IEnumerator destory(float _time)//自行銷毀
    {
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
