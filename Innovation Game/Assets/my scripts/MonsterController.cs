using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : NavigationAgent {

    public Transform target;
    NavMeshAgent agent;
    Transform playerStart, monsterStart,targetTransform;
    //player reference
    public PlayerController player;
    public Transform playerSpottedLocation = null;
    public GameObject rayStart, rayEnd, monster;
    public Transform randomPos;
    //Movement Varaibles
    public float moveSpeed = 10.0f;
    public float minDistance = 0.4f;
    public float monsterTimer;

    //bools
    public bool locationFound = false;

    public Physics chargeCast;

    //monster FSMs
    public enum MonsterBehaviours {
        Patrol,
        Charge
    }

    public MonsterBehaviours monsterBehaviour;

    public RaycastHit hit;

    // Use this for initialization
    public void Start () {
        agent = GetComponent<NavMeshAgent>();
        monster = gameObject;
        monsterTimer = 10f;
        //chargeCast.Raycast() = Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 50f);

        //find waypoint graph
        graphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>();
        //Initial node index to move to

        //Establish reference to player game object
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        target = graphNodes.graphNodes[currentNodeIndex].transform;

        

    }
	
	// Update is called once per frame
	public void Update () {
        agent.SetDestination(target.position);

        //FSMs switching
        switch (monsterBehaviour) {
            case MonsterBehaviours.Patrol:
                Patrol();
                //print("hitter");
                break;
            case MonsterBehaviours.Charge:
                Charge();
                //print("hittyu");
                break;
        }
    }

    private void Patrol() {
        //print("hitpatrol");
        agent.speed = 3.5f;
        monsterTimer = 5;
        locationFound = false;
        //target = randomPos;
        //print(Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position, target.position) <= minDistance)
        {
            //print("hit");
            //randomly select new waypoint
            //int randomNode = Random.Range(0, graphNodes.graphNodes.Length);

            //Hardcoded stuff needs to be redone
            //randomNode;
            //Transform targetTransform;
            int randomNodeSelector = Random.Range(0,99);
            int randomNodepositionfinder;
            if (randomNodeSelector <= 39) //player locations
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().playerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().playerLocations[randomNodepositionfinder].transform;
            } else if (randomNodeSelector <= 79) //nodes close to player locations
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations[randomNodepositionfinder].transform;
            } else if (randomNodeSelector <= 94) //nodes near to player locations
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations.Count);
                target = gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations[randomNodepositionfinder].transform;
            } else if (randomNodeSelector <= 99) {//nodes not near to player locations
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations[randomNodepositionfinder].transform;
            }

            // target = graphNodes.graphNodes[randomNode].transform;
            //target = player.transform;
            target = targetTransform;
        }
    }

    private void Charge() {
        randomPos = gameObject.transform;
        //print("Charge");
        target = player.transform;
        agent.speed = 6f;
    }
}
