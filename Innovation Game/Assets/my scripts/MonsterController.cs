using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : NavigationAgent {

    public Transform target;
    NavMeshAgent agent;
    Transform playerStart, monsterStart;
    //player reference
    public PlayerController player;

    public GameObject rayStart, rayEnd, monster;

    //Movement Varaibles
    public float moveSpeed = 10.0f;
    public float minDistance = 0.4f;

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
                print("hittyu");
                break;
        }
    }

    private void Patrol() {
        agent.speed = 3.5f;
        //print(Vector3.Distance(transform.position, target.position));
        if (Vector3.Distance(transform.position, target.position) <= minDistance)
        {
            //print("hit");
            //randomly select new waypoint
            int randomNode = Random.Range(0, graphNodes.graphNodes.Length);

            target = graphNodes.graphNodes[randomNode].transform;
            //target = player.transform;
        }
    }

    private void Charge() {
        print("Charge");
        target = player.transform;
        agent.speed = 11f;
    }
}
