using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform playerStart, monsterStart;

	// Use this for initialization
	void Start () {

        playerStart = GameObject.FindGameObjectWithTag("Player").transform;
        monsterStart = GameObject.FindGameObjectWithTag("Monster").transform;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
