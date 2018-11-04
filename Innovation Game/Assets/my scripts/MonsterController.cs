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
    public bool spottedPlayer = false;
    public Transform playerSpottedLocation = null;
    public GameObject rayStart, rayEnd1, rayEnd2, rayEnd3, monster, prefabObject;
    public Transform randomPos;
    //Movement Varaibles
    public float moveSpeed = 10.0f;
    public float minDistance = 0.4f;
    public float monsterTimer;
    public float distance;
    //bools
    public bool locationFound = false;

    public Physics chargeCast;

    public int fixertest = 0;

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

        //find waypoint graph
        graphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WaypointGraph>();

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
                break;
            case MonsterBehaviours.Charge:
                Charge();
                break;
        }

        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        gameObject.GetComponent<EnemySight>().target = target;
        gameObject.GetComponent<QuerySearch>().target = target;
    }

    //pseudo-randomly pick from each of the lists and set the chosen node as its destination target.
    private void Patrol() {
        agent.speed = 3.5f;
        monsterTimer = 5;
        playerSpottedLocation = null;
        gameObject.GetComponent<QuerySearch>().playerSpottedLocation = null;
        gameObject.GetComponent<EnemySight>().playerSpottedLocation = null;
        locationFound = false;
        gameObject.GetComponent<QuerySearch>().locationFound = false;
        gameObject.GetComponent<EnemySight>().locationFound = false;
        int randomness = Random.Range(0, 70);

        if (fixertest == 1) {
            gameObject.GetComponent<EnemySight>().target = target;
            fixertest = 0;
        }
        if (Vector3.Distance(transform.position, target.position) <= minDistance)
        {
            print("hit");
            int randomNodeSelector = Random.Range(0,99);
            int randomNodepositionfinder;

            //player locations
            if ((randomNodeSelector <= 39) && (gameObject.GetComponent<QuerySearch>().playerLocations.Count > 0)) 
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().playerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().playerLocations[randomNodepositionfinder].transform;
            }
            //nodes close to player locations
            else if ((randomNodeSelector <= 79) && (gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations.Count > 0)) 
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations[randomNodepositionfinder].transform;
            }
            //nodes near to player locations
            else if ((randomNodeSelector <= 94) && (gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations.Count > 0)) 
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations[randomNodepositionfinder].transform;
            }
            //nodes not near to player locations
            else if ((randomNodeSelector <= 99)  && (gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations.Count > 0)) {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations[randomNodepositionfinder].transform;
            }
            target = targetTransform;
        }
    }

    //increase speed and set the player as the destination target.
    private void Charge() {
        randomPos = gameObject.transform;
        target = player.transform;
        agent.speed = 14f;
    }
}