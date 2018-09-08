using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonsterController {

    Rigidbody rigidbody;
    Camera viewCamera;
    Transform playerStart, monsterStart;
    Quaternion playerRotation, monsterRotation;
    int loseInt, roundInt;
    //public Quaternion playerRotation, monsterRotation;
    //Transform playerStart, monsterStart;
    

    // Use this for initialization
    void Start () {
        base.Start();
        playerStart = GameObject.Find("Game Manager").GetComponent<GameManager>().playerStart;
        monsterStart = GameObject.Find("Game Manager").GetComponent<GameManager>().monsterStart;
        playerRotation = GameObject.Find("Game Manager").GetComponent<GameManager>().playerRotation;
        monsterRotation = GameObject.Find("Game Manager").GetComponent<GameManager>().monsterRotation;
        loseInt = GameObject.Find("Game Manager").GetComponent<GameManager>().loseInt;
        roundInt = GameObject.Find("Game Manager").GetComponent<GameManager>().roundInt;
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
        Debug.DrawRay(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized);

        //this is the charge the player raycast
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 50f)) {
            if (hit.transform.tag == "Player") {
                print("CHARGE!@!!!!");
                this.monsterBehaviour = MonsterBehaviours.Charge;
            }
            
        }
        //this is the kill the player raycast
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 10f)) {
            if (hit.transform.tag == "Player") {
                player.transform.position = playerStart.position;
                monster.transform.position = monsterStart.position;
                player.transform.rotation = playerRotation;
                monster.transform.rotation = monsterRotation;
                loseInt++;
                roundInt++;
                //print("KILL!!!!!!");
            }
                
        }

            
    }
}
