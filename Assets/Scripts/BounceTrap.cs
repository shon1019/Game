using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrap : MonoBehaviour {

    public float _power = 5;
    public bool _isRandom = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Vector3 direction = Vector3.zero;
            if (_isRandom)
            {
                direction = (transform.position - other.transform.position).normalized;
                float upAngle = Random.Range(4, 9) / 10.0f;
                direction = Vector3.Slerp(direction, Vector3.up, upAngle);
                float flatAngle = Random.Range(0, 360);
                direction = Quaternion.Euler(0, flatAngle, 0) * direction;
            }
            else
            {
                direction = (transform.position - other.transform.position).normalized;
                direction = Vector3.Slerp(direction, Vector3.up, 0.5f);
            }

            other.GetComponent<Rigidbody>().AddForce(direction * _power, ForceMode.Force);
        }
    }
}
