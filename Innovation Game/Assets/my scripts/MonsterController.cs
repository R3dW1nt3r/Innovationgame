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
    public GameObject rayStart, rayEnd, monster, prefabObject;
    public Transform randomPos;
    //Movement Varaibles
    public float moveSpeed = 10.0f;
    public float minDistance = 0.4f;
    public float monsterTimer;
    public float distance;
    //bools
    public bool locationFound = false;

    public Physics chargeCast;

    //this is a very hardcodey attempt which works to stop the monster keeping the player as its target. it does this in enemysight
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

        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        gameObject.GetComponent<EnemySight>().target = target;
    }

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

        //this is a very hardcodey attempt which works to stop the monster keeping the player as its target. it does this in enemysight
        if (fixertest == 1) {
            gameObject.GetComponent<EnemySight>().target = target;
            fixertest = 0;
        }
        if (Vector3.Distance(transform.position, target.position) <= minDistance)
        {
            print("hit");
            int randomNodeSelector = Random.Range(0,99);
            int randomNodepositionfinder;
            if ((randomNodeSelector <= 39) && (gameObject.GetComponent<QuerySearch>().playerLocations.Count > 0)) //player locations
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().playerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().playerLocations[randomNodepositionfinder].transform;
            } else if ((randomNodeSelector <= 79) && (gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations.Count > 0)) //nodes close to player locations
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesClosetoPlayerLocations[randomNodepositionfinder].transform;
            } else if ((randomNodeSelector <= 94) && (gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations.Count > 0)) //nodes near to player locations
            {
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesNeartoPlayerLocations[randomNodepositionfinder].transform;
            } else if ((randomNodeSelector <= 99)  && (gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations.Count > 0)) {//nodes not near to player locations
                randomNodepositionfinder = Random.Range(0, gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations.Count);
                targetTransform = gameObject.GetComponent<QuerySearch>().nodesNotNeartoPlayerLocations[randomNodepositionfinder].transform;
            }
            target = targetTransform;
        }
    }

    private void Charge() {
        randomPos = gameObject.transform;
        target = player.transform;
        agent.speed = 6f;
    }
}
