using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Destroy(other.gameObject);
        }
    }
}
