using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : NavigationAgent {

    public Transform target;
    NavMeshAgent agent;

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
        //currentPath.Add(currentNodeIndex);

        //Establish reference to player game object
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        target = graphNodes.graphNodes[currentNodeIndex].transform;
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);

        //Patrol pathing
        Patrol();

        //move
        //Move();
        //print(graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position);
        //print(graphNodes.graphNodes.Length);
        //print(graphNodes.graphNodes[currentPath]);
    }

    //Move Enemy
    /*private void Move()
    {

        if (currentPath.Count > 0)
        {
            //Move towards next node in path
            transform.position = Vector3.MoveTowards(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position, moveSpeed * Time.deltaTime);

            //Increase path index
            if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) <= minDistance)
            {

                if (currentPathIndex < currentPath.Count - 1)
                    currentPathIndex++;
            }

            currentNodeIndex = graphNodes.graphNodes[currentPath[currentPathIndex]].GetComponent<LinkedNodes>().index;   //Store current node index
        }
    }*/

    private void Patrol() {
        //print(Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position, target.position) <= minDistance)
        {
            //print("hit");
            //randomly select new waypoint
            int randomNode = Random.Range(0, graphNodes.graphNodes.Length);

            //Reset current path and add first node - needs to be done here because of recursive function of greedy
            /*currentPath.Clear();
            greedyPaintList.Clear();
            currentPathIndex = 0;
            currentPath.Add(currentNodeIndex);

            //Greedy search - navigate towards randomNode
            currentPath = GreedySearch(currentPath[currentPathIndex], randomNode, currentPath);

            //Reverse path and remove final (i.e. initial) position
            currentPath.Reverse();
            currentPath.RemoveAt(currentPath.Count - 1);*/

            target = graphNodes.graphNodes[randomNode].transform;
        }
    }
}
