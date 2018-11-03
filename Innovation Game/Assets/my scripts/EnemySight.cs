using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonsterController {

    Rigidbody rigidbody;
    
    Camera viewCamera;
    Transform playerStart, monsterStart;
    Quaternion playerRotation, monsterRotation;
    int loseInt, roundInt;

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
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit)) {
            if (hit.transform.tag == "Player") {

                //create an empty object for the player spotted location
                Instantiate(prefabObject, player.transform.position, player.transform.rotation);

                this.monsterBehaviour = MonsterBehaviours.Charge;
                gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterBehaviours.Charge;
                gameObject.GetComponent<QuerySearch>().monsterBehaviour = MonsterBehaviours.Charge;
            }
        }
        //this is the kill the player raycast
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 2f))
        {
            if (hit.transform.tag == "Player")
            {
                player.transform.position = playerStart.position;
                monster.transform.position = monsterStart.position;
                player.transform.rotation = playerRotation;
                monster.transform.rotation = monsterRotation;
                loseInt++;
                roundInt++;
            } 
        }
        if ((this.monsterBehaviour == MonsterBehaviours.Charge)/* && (hit.distance > 10f)*/) {
            monsterTimer = monsterTimer - Time.deltaTime;

            //if timer finishes this code searches for the waypoint closest to the location the player has been spotted and adds both adds it to the player locations list and sees if it is in any other list and removes it from those
            if (monsterTimer <= 0)
            {
                gameObject.GetComponent<QuerySearch>().EnvironementalQuerySearch();
                gameObject.GetComponent<MonsterController>().target = transform;
                gameObject.GetComponent<MonsterController>().target = gameObject.GetComponent<MonsterController>().transform;
                gameObject.GetComponent<MonsterController>().target = gameObject.GetComponent<EnemySight>().transform;
                monsterBehaviour = MonsterBehaviours.Patrol;
                gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterController.MonsterBehaviours.Patrol;
                gameObject.GetComponent<EnemySight>().monsterBehaviour = EnemySight.MonsterBehaviours.Patrol;
                //this is a very hardcodey attempt which works to stop the monster keeping the player as its target. it does this in enemysight
                fixertest = 1;
            }
        }  
    }

    public bool PlayerLocations() {

        //this will skip a node. to be redone //maybe not look into it
        for (int i = 0; i < gameObject.GetComponent<QuerySearch>().queryGraphNodes.Length; i++)
        {
            if ((gameObject.GetComponent<QuerySearch>().playerLocation != null) &&
            (Vector3.Distance(gameObject.GetComponent<QuerySearch>().queryGraphNodes[i].transform.position, playerSpottedLocation.position) <
            Vector3.Distance(gameObject.GetComponent<QuerySearch>().playerLocation.transform.position, playerSpottedLocation.position)))
            {
                gameObject.GetComponent<QuerySearch>().playerLocation = gameObject.GetComponent<QuerySearch>().queryGraphNodes[i].transform;
            } else
                gameObject.GetComponent<QuerySearch>().playerLocation = gameObject.GetComponent<QuerySearch>().queryGraphNodes[i].transform;
        }

        locationFound = true;
        return locationFound;
    }

    public void SpotPlayer(Transform playerLocation) {
        if (spottedPlayer == false)
        {
            playerSpottedLocation = playerLocation;
            print(playerLocation);
            gameObject.GetComponent<QuerySearch>().playerSpottedLocation = playerLocation;
            print(playerLocation);
            gameObject.GetComponent<MonsterController>().playerSpottedLocation = playerLocation;
            print(playerLocation);
            print(playerLocation);
            spottedPlayer = true;
        }
    }
}
