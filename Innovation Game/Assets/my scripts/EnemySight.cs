using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    Rigidbody rigidbody;
    Camera viewCamera;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
