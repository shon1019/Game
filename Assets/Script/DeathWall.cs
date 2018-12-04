using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

    public GameManager gm;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            gm._IsPlayerSurvived = false;
            gm.Revived();
            other.gameObject.SetActive(false);
        }
    }
}
