using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnique : MonoBehaviour {

    GameObject goal;

	// Use this for initialization
	void Start () {
        //goal = GameObject.FindGameObjectWithTag("Goal").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        print("win");
    }

}
