using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuerySearch : MonsterController {

    public List<Transform> playerLocations = new List<Transform>();
    public Transform playerLocation = null;
    public List<Transform> nodesClosetoPlayerLocations = new List<Transform>();
    public List<Transform> nodesNeartoPlayerLocations = new List<Transform>();
    public List<Transform> nodesNotNeartoPlayerLocations = new List<Transform>();

    public GameObject[] queryGraphNodes;
    public List<Transform> queryGraphNodeTransforms;

	// Use this for initialization
	void Start () {
        base.Start();
        queryGraphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>().graphNodes;
        //sets all locations in the query search - grabs their transforms
        for (int i = 0; i < queryGraphNodes.Length; i++) {
            nodesNotNeartoPlayerLocations.Add(queryGraphNodes[i].transform);
        }

        for (int i = 0; i < queryGraphNodes.Length; i++) {
            queryGraphNodeTransforms.Add(queryGraphNodes[i].transform);
        }

        //playtesting fixes to make the AI stick around bottlenecks at the beginning
        //playerLocations additions
        playerLocations.Add(queryGraphNodes[0].transform);
        playerLocations.Add(queryGraphNodes[1].transform);
        playerLocations.Add(queryGraphNodes[28].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[0].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[1].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[28].transform);

        //nodesClosetoplayerlocations additions
        playerLocations.Add(queryGraphNodes[32].transform);
        playerLocations.Add(queryGraphNodes[35].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[32].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[35].transform);
        
        //nodesneartoplayerlocations additions
        nodesNeartoPlayerLocations.Add(queryGraphNodes[40].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[50].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[70].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[55].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[2].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[14].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[23].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[29].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[26].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[71].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[24].transform);
        nodesNeartoPlayerLocations.Add(queryGraphNodes[27].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[40].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[50].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[70].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[55].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[2].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[14].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[23].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[29].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[26].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[71].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[24].transform);
        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[27].transform);
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
                    if ((Vector3.Distance(playerLocations[i].transform.position, queryGraphNodes[j].transform.position) <  20) && (nodesClosetoPlayerLocations.Contains(queryGraphNodes[j].transform) != true))
                    {
                        nodesClosetoPlayerLocations.Add(queryGraphNodes[j].transform);
                        nodesNeartoPlayerLocations.Remove(queryGraphNodes[j].transform);
                        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[j].transform);
                    } 
                    
                    else if ((Vector3.Distance(playerLocations[i].transform.position, queryGraphNodes[j].transform.position) < 50) && (nodesNeartoPlayerLocations.Contains(queryGraphNodes[j].transform) != true))
                    {
                        nodesNeartoPlayerLocations.Add(queryGraphNodes[j].transform);
                        nodesNotNeartoPlayerLocations.Remove(queryGraphNodes[j].transform);
                    } 
                }
            }

            for (int i = 0; i < playerLocations.Count; i++) {
                if (!queryGraphNodeTransforms.Contains(playerLocations[i])) {
                    playerLocations.Remove(playerLocations[i]);
                }
            }

            locationFound = true;
        }
        monsterBehaviour = MonsterBehaviours.Patrol;
        gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterController.MonsterBehaviours.Patrol;
    }

    //determine the node closest to the empty object and return the node.
    public Transform SearchForPlayerLocation(Transform playerSpottedLocation) {
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
