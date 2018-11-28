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
        print(BoxPos.position);
        transform.position = new Vector3(BoxPos.position.x, -3, BoxPos.position.z);
        
	}
}
