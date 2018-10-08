using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuerySearch : MonoBehaviour {

    public List<GameObject> playerLocations = new List<GameObject>();
    public GameObject playerLocation = null;
    public List<GameObject> nodesClosetoPlayerLocations = new List<GameObject>();
    public List<GameObject> nodesNeartoPlayerLocations = new List<GameObject>();
    public List<GameObject> nodesNotNeartoPlayerLocations = new List<GameObject>();

    //need to rename this
    public GameObject[] graphNodes;

	// Use this for initialization
	void Start () {
        //grabs waypoint graph
        graphNodes = GameObject.Find("WaypointGraph").GetComponent<WaypointGraph>().graphNodes;

        //HardCodeQuerySearch();
        //sets all locations in the query search 
        for (int i = 0; i < graphNodes.Length; i++) {
            nodesNotNeartoPlayerLocations.Add(graphNodes[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when a new player location is added search through all lists and correctly move graphnodes between lists
    public void EnvironementalQuerySearch() {
        for (int i = 0; i < playerLocations.Count; i++) {
            nodesClosetoPlayerLocations.Remove(playerLocations[i]);
            nodesNeartoPlayerLocations.Remove(playerLocations[i]);
            nodesNotNeartoPlayerLocations.Remove(playerLocations[i]);
            for (int j = 0; j < graphNodes.Length; j++) {
                if ((Vector3.Distance(playerLocations[i].transform.position, graphNodes[j].transform.position) < 5 /*look into if this distance is reasonable*/) && (nodesClosetoPlayerLocations.Contains(graphNodes[j]) != true))
                {
                    nodesClosetoPlayerLocations.Add(graphNodes[j]);
                    nodesNeartoPlayerLocations.Remove(graphNodes[j]);
                    nodesNotNeartoPlayerLocations.Remove(graphNodes[j]);
                } else if ((Vector3.Distance(playerLocations[i].transform.position, graphNodes[j].transform.position) < 10 /*look into if this distance is reasonable*/) && (nodesNeartoPlayerLocations.Contains(graphNodes[j]) != true))
                {
                    nodesNeartoPlayerLocations.Add(graphNodes[j]);
                    nodesNotNeartoPlayerLocations.Remove(graphNodes[j]);
                } else
                    Debug.Log("May be a problem with " + graphNodes[j].name);
            }
        }
    }

    public void HardCodeQuerySearch() {//only for vid
        //player locations
        playerLocations.Add(GameObject.Find("node 0"));                 
        playerLocations.Add(GameObject.Find("node 6"));                 

        //close to player locations
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 55"));    //55
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 70"));    //70
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 32"));    
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 35"));    
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 2"));     
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 20"));    
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 5"));     
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 4"));     
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 10"));    
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 11"));    
        nodesClosetoPlayerLocations.Add(GameObject.Find("node 7"));     

        //near to player locations
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 56"));     //56
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 61"));     //61
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 38"));     
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 50"));     
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 1"));      
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 14"));     
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 8"));      
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 12"));     
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 15"));     
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 3"));      
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 27"));     
        nodesNeartoPlayerLocations.Add(GameObject.Find("node 24"));     

        //not near to player locations  //0,1,2,3,4,5,6,7,8,10,11,12,14,15,20,24,27,32,35,38,50,55,56,61,70
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 9"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 13"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 16"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 17"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 18"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 19"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 21"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 22"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 23"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 25"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 26"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 28"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 29"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 30"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 31"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 33"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 34"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 36"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 37"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 39"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 40"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 41"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 42"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 43"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 44"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 45"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 46"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 47"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 48"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 49"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 51"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 52"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 53"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 54"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 57"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 58"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 59"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 60"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 62"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 63"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 64"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 65"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 66"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 67"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 68"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 69"));
        nodesNotNeartoPlayerLocations.Add(GameObject.Find("node 71"));
    }
}
