using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : NavigationAgent {

    public Transform target;
    NavMeshAgent agent;
    Transform playerStart, monsterStart;
    //player reference
    PlayerController player;

    //Movement Varaibles
    public float moveSpeed = 10.0f;
    public float minDistance = 0.4f;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

        //find waypoint graph
        graphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>();
        //Initial node index to move to

        //Establish reference to player game object
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        target = graphNodes.graphNodes[currentNodeIndex].transform;

        

    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);

        //Patrol pathing
        Patrol();
    }

    private void Patrol() {
        //print(Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position, target.position) <= minDistance)
        {
            //print("hit");
            //randomly select new waypoint
            int randomNode = Random.Range(0, graphNodes.graphNodes.Length);

            target = graphNodes.graphNodes[randomNode].transform;
        }
    }
}
