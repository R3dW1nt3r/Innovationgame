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
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit)) {
            if (hit.transform.tag == "Player") {
                //print("CHARGE!@!!!!");
                this.monsterBehaviour = MonsterBehaviours.Charge;
                //print(hit.distance);
                //monsterTimer = 10f;
            }

            //if (hit.transform.tag == "wall")
            //print("Searching");
            //print("timer is "+monsterTimer);
            //print("behaviour is "+monsterBehaviour);
            //print(hit.distance);
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
                //print("KILL!!!!!!");
            } 
        }
        //print(hit.transform.tag);
        if ((this.monsterBehaviour == MonsterBehaviours.Charge) && (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 15f))) {//use vector3 distance instead of hit.distance calculations for hit point and
                                                                                              //worst case scenario if target.hit = player else target = null (have different target for nodes)
                                                                                              //use forward vector for raycast
            //print("hit");
            //readd 
            //print("hittytytytytyt");
            //player location dropping
            playerSpottedLocation = player.transform;
            //print("WTF?!?!?!?");
            monsterTimer = monsterTimer - Time.deltaTime;
            //print("timer is " + monsterTimer);

            //if timer finishes this code searches for the waypoint closest to the location the player has been spotted and adds both adds it to the player locations list and sees if it is in any other list and removes it from those
            if (monsterTimer <= 0)
            {
                //PlayerLocations();
                gameObject.GetComponent<QuerySearch>().EnvironementalQuerySearch();
                gameObject.GetComponent<MonsterController>().attackTarget = null;
                gameObject.GetComponent<MonsterController>().attackTarget = null;
                gameObject.GetComponent<MonsterController>().attackTarget = null;
                monsterBehaviour = MonsterBehaviours.Patrol;
                gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterController.MonsterBehaviours.Patrol;
                gameObject.GetComponent<EnemySight>().monsterBehaviour = EnemySight.MonsterBehaviours.Patrol;
                //this is a very hardcodey attempt which works to stop the monster keeping the player as its target. it does this in enemysight
                fixertest = 1;
            }

            /*if (hit.distance > 7f)
            {
                print("hittytytytytyt");
                //player location dropping
                playerSpottedLocation = player.transform;
                print("WTF?!?!?!?");
                monsterTimer = monsterTimer - Time.deltaTime;
                print("timer is " + monsterTimer);

                //if timer finishes this code searches for the waypoint closest to the location the player has been spotted and adds both adds it to the player locations list and sees if it is in any other list and removes it from those
                if (monsterTimer <= 0)
                {
                    PlayerLocations();
                }

                if (locationFound == true) {
                    print("location hit");
                    gameObject.GetComponent<QuerySearch>().playerLocations.Add(gameObject.GetComponent<QuerySearch>().playerLocation);
                    gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations.Remove(gameObject.GetComponent<QuerySearch>().playerLocation);
                    gameObject.GetComponent<QuerySearch>().EnvironementalQuerySearch();
                    monsterBehaviour = MonsterBehaviours.Patrol;
                }
            }*/
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
                gameObject.GetComponent<QuerySearch>().playerLocation = gameObject.GetComponent<QuerySearch>().queryGraphNodes[i];
            } else
                gameObject.GetComponent<QuerySearch>().playerLocation = gameObject.GetComponent<QuerySearch>().queryGraphNodes[i];
        }

        locationFound = true;
        return locationFound;
    }

}
