using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuerySearch : EnemySight {

    public List<GameObject> playerLocations = new List<GameObject>();
    public GameObject playerLocation = null;
    public List<GameObject> nodesClosetoPlayerLocations = new List<GameObject>();
    public List<GameObject> nodesNeartoPlayerLocations = new List<GameObject>();
    public List<GameObject> nodesNotNeartoPlayerLocations = new List<GameObject>();

    //need to rename this
    public GameObject[] queryGraphNodes;

	// Use this for initialization
	void Start () {
        //grabs waypoint graph
        //queryGraphNodes = GameObject.FindObjectWithTag("waypoint graph").GetComponent<WaypointGraph>().graphNodes;
        queryGraphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>().graphNodes;
        //HardCodeQuerySearch();
        //sets all locations in the query search 
        for (int i = 0; i < queryGraphNodes.Length; i++) {
            nodesNotNeartoPlayerLocations.Add(queryGraphNodes[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
        base.Start();
	}

    //when a new player location is added search through all lists and correctly move graphnodes between lists
    public void EnvironementalQuerySearch() {
        if (locationFound == false) {
            print("initialhit");
            
            SearchForPlayerLocation(GameObject.Find("monster").GetComponent<EnemySight>().playerSpottedLocation);
            print("hit to prove it doesn't stop at search for playerlocation");
            for (int i = 0; i < playerLocations.Count; i++)
            {
                print("hitfirstloop");
                nodesClosetoPlayerLocations.Remove(playerLocations[i]);
                nodesNeartoPlayerLocations.Remove(playerLocations[i]);
                nodesNotNeartoPlayerLocations.Remove(playerLocations[i]);
                for (int j = 0; j < queryGraphNodes.Length; j++)
                {
                    print("hittyhit");
                    if ((Vector3.Distance(playerLocations[i].transform.position, queryGraphNodes[j].transform.position) <  1000/*look into if this distance is reasonable*/) && (nodesClosetoPlayerLocations.Contains(queryGraphNodes[j]) != true))
                    {
                        nodesClosetoPlayerLocations.Add(queryGraphNodes[j]);
                        nodesNeartoPlayerLocations.Remove(queryGraphNodes[j]);
                        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[j]);
                    } else if ((Vector3.Distance(playerLocations[i].transform.position, queryGraphNodes[j].transform.position) < 10000 /*look into if this distance is reasonable*/) && (nodesNeartoPlayerLocations.Contains(queryGraphNodes[j]) != true))
                    {
                        nodesNeartoPlayerLocations.Add(queryGraphNodes[j]);
                        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[j]);
                    } else
                        Debug.Log("May be a problem with " + queryGraphNodes[j].name);
                }
            }
            locationFound = true;
        }

        print("playerlocations length = "+playerLocations.Count);
        print("nodesclosetoplayerlocations length  = "+ nodesClosetoPlayerLocations.Count);
        print("nodesneartoplayerlocations length = "+ nodesNeartoPlayerLocations.Count);

        //print statements at end
        /*for (int i = 0; i < playerLocations.Count; i++) {
            print("Player location"+i+" = "+playerLocations[i].name);
        }

        for (int i = 0; i < nodesClosetoPlayerLocations.Count; i++) {
            print("Node "+i+" close to player locations = " + nodesClosetoPlayerLocations[i].name);
        }

        for (int i = 0; i < nodesNeartoPlayerLocations.Count; i++) {
            print("Node " + i + " near to player locations = " + nodesNeartoPlayerLocations[i].name);
        }*/
        monsterBehaviour = MonsterBehaviours.Patrol;
    }


    public GameObject SearchForPlayerLocation(Transform playerSpottedLocation) {
        print("hitinside");
        //this will skip a node. to be redone //maybe not look into it
        for (int i = 0; i < queryGraphNodes.Length; i++)
        {
            if ((playerLocation != null) && (Vector3.Distance(queryGraphNodes[i].transform.position, playerSpottedLocation.position) 
                < Vector3.Distance(playerLocation.transform.position, playerSpottedLocation.position)))
            {
                playerLocation = queryGraphNodes[i];
            } else
                playerLocation = queryGraphNodes[i];
        }
        playerLocations.Add(playerLocation);
        nodesNotNeartoPlayerLocations.Remove(playerLocation);
        return playerLocation;
    }

}
