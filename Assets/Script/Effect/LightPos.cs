using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPos : MonoBehaviour {

    public Transform BoxPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
=======
        //print(BoxPos.position);
>>>>>>> 4c37fd7d3b1b7ddda4dcbfa60fdbe310d9cb65f7
        transform.position = new Vector3(BoxPos.position.x, -3, BoxPos.position.z);
	}
}
