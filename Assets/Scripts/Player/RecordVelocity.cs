using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordVelocity : MonoBehaviour {
    public GameObject _player;

    private float _playerVelocity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RecordVelocity")
        {
            _playerVelocity = _player.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }

    public float GetPlayerVelocity()
    {
        return _playerVelocity;
    }
}
