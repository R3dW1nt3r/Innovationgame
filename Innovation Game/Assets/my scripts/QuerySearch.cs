﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuerySearch : MonsterController {

    public List<Transform> playerLocations = new List<Transform>();
    public Transform playerLocation = null;
    public List<Transform> nodesClosetoPlayerLocations = new List<Transform>();
    public List<Transform> nodesNeartoPlayerLocations = new List<Transform>();
    public List<Transform> nodesNotNeartoPlayerLocations = new List<Transform>();

    //need to rename this
    public GameObject[] queryGraphNodes;

	// Use this for initialization
	void Start () {
        base.Start();
        queryGraphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>().graphNodes;
        //sets all locations in the query search - grabs their transforms
        for (int i = 0; i < queryGraphNodes.Length; i++) {
            nodesNotNeartoPlayerLocations.Add(queryGraphNodes[i].transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (playerSpottedLocation != null)
            print(playerSpottedLocation.position);
	}

    //when a new player location is added search through all lists and correctly move graphnodes between lists
    public void EnvironementalQuerySearch() {
        if (locationFound == false) {
            
            SearchForPlayerLocation(GameObject.Find("monster").GetComponent<EnemySight>().playerSpottedLocation);
            playerLocations.Add(playerLocation);
            nodesNotNeartoPlayerLocations.Remove(playerLocation);
            for (int i = 0; i < playerLocations.Count; i++)
            {
                nodesClosetoPlayerLocations.Remove(playerLocations[i]);
                nodesNeartoPlayerLocations.Remove(playerLocations[i]);
                nodesNotNeartoPlayerLocations.Remove(playerLocations[i]);
                for (int j = 0; j < queryGraphNodes.Length; j++)
                {
                    if ((Vector3.Distance(playerLocations[i].transform.position, queryGraphNodes[j].transform.position) <  20/*look into if this distance is reasonable*/) && (nodesClosetoPlayerLocations.Contains(queryGraphNodes[j].transform) != true))
                    {
                        nodesClosetoPlayerLocations.Add(queryGraphNodes[j].transform);
                        nodesNeartoPlayerLocations.Remove(queryGraphNodes[j].transform);
                        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[j].transform);
                    } 
                    
                    else if ((Vector3.Distance(playerLocations[i].transform.position, queryGraphNodes[j].transform.position) < 50 /*look into if this distance is reasonable*/) && (nodesNeartoPlayerLocations.Contains(queryGraphNodes[j].transform) != true))
                    {
                        nodesNeartoPlayerLocations.Add(queryGraphNodes[j].transform);
                        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[j].transform);
                    } 
                }
            }
            locationFound = true;
        }
        monsterBehaviour = MonsterBehaviours.Patrol;
        gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterController.MonsterBehaviours.Patrol;
    }


    public Transform SearchForPlayerLocation(Transform playerSpottedLocation) {
        print("hitinside");
        //this will skip a node. to be redone //maybe not look into it
        for (int i = 0; i < queryGraphNodes.Length; i++)
        {
            if (playerLocation != null)
            {
                if (Vector3.Distance(queryGraphNodes[i].transform.position, playerSpottedLocation.position) < Vector3.Distance(playerLocation.position, playerSpottedLocation.position))
                {
                    playerLocation = queryGraphNodes[i].transform;
                }
            } else
                playerLocation = playerSpottedLocation;
        }
        return playerLocation;
    }

}
