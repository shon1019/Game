using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPos : MonoBehaviour {

    public GameObject Target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Target.transform.position+new Vector3(0,10,0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
