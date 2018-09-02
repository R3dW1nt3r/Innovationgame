using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : GameManager {

    Rigidbody rigidbody;
    Camera viewCamera;

    public GameObject rayStart,rayEnd;
    //public Quaternion playerRotation, monsterRotation;
    //Transform playerStart, monsterStart;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        //player = GameObject.FindGameObjectWithTag("Player");
        //monster = GameObject.FindGameObjectWithTag("Monster");
        //playerRotation = player.transform.rotation;
        //monsterRotation = monster.transform.rotation;
        //playerStart = GameObject.Find("Game Manager").GetComponent<GameManager>().playerStart;
        //monsterStart = GameObject.Find("Game Manager").GetComponent<GameManager>().monsterStart;
        //Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit);
    }
	
	// Update is called once per frame
	void Update () {
        //Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 50f);
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 50f)) {
            if (hit.transform.tag == "Player") {
                print("hitty");
                player.transform.position = playerStart.position;
                monster.transform.position = monsterStart.position;
                player.transform.rotation = playerRotation;
                monster.transform.rotation = monsterRotation;
            }
                
        }
            
    }
}
