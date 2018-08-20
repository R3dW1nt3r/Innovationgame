using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goaltrigger : GameManager {

    GameObject player, monster;

    Quaternion playerRotation, monsterRotation;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        monster = GameObject.FindGameObjectWithTag("Monster");
        playerRotation = player.transform.rotation;
        monsterRotation = monster.transform.rotation;
        //playerStart = GameManager.playerStart;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("goal reached");
            player.transform.position = playerStart.position;
            monster.transform.position = monsterStart.position;
            player.transform.rotation = playerRotation;
            monster.transform.rotation = monsterRotation;
        }
    }
}
