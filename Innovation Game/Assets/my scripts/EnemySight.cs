using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonsterController {

    public bool enemySightPlayerSpotted;

    Rigidbody rigidbody;
    public List<GameObject> enemySightPlayerFoundLocations = new List<GameObject>();
    Camera viewCamera;
    Transform playerStart, monsterStart;
    Quaternion playerRotation, monsterRotation;
    int loseInt, roundInt;
    public Physics raycast;

    public GameObject gameManager;
    // Use this for initialization
    void Start () {
        base.Start();
        playerStart = GameObject.Find("Game Manager").GetComponent<GameManager>().playerStart;
        monsterStart = GameObject.Find("Game Manager").GetComponent<GameManager>().monsterStart;
        playerRotation = GameObject.Find("Game Manager").GetComponent<GameManager>().playerRotation;
        monsterRotation = GameObject.Find("Game Manager").GetComponent<GameManager>().monsterRotation;
        gameManager = GameObject.Find("Game Manager");
        enemySightPlayerSpotted = false;
    }

    // Update is called once per frame
    void Update () {
        base.Update();
        //if the player obstructs any one of the raycasts, create an empty object to be used for the transform, change to a charge state
        if ((Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd1.transform.position).normalized, out hit, 100f)) || (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd2.transform.position).normalized, out hit, 100f)) || (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd3.transform.position).normalized, out hit, 100f))) {
            if ((hit.transform.tag == "Player") && (this.monsterBehaviour != MonsterBehaviours.Charge))
            {
                monsterTimer = 5;
                //create an empty object for the player spotted location
                if (enemySightPlayerSpotted == false)
                {
                    playerSpottedLocation = Instantiate(prefabObject, player.transform.position, player.transform.rotation).transform;
                    enemySightPlayerSpotted = true;
                }


                this.monsterBehaviour = MonsterBehaviours.Charge;
                gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterBehaviours.Charge;
                gameObject.GetComponent<QuerySearch>().monsterBehaviour = MonsterBehaviours.Charge;
            }
            //else if the monster is currently in a charge state and the player hasn't been obstructing the raycast for more than 5 seconds set the target to the current location 
            //and reweigh the proabilities of going to locations  then revert to patrol state.
            else if ((hit.transform.tag != "Player") && (this.monsterBehaviour == MonsterBehaviours.Charge)) {
                monsterTimer = monsterTimer - Time.deltaTime;
                if (monsterTimer <= 0)
                {
                    enemySightPlayerSpotted = false;
                    gameObject.GetComponent<QuerySearch>().EnvironementalQuerySearch();
                    gameObject.GetComponent<MonsterController>().target = transform;
                    gameObject.GetComponent<MonsterController>().target = gameObject.GetComponent<MonsterController>().transform;
                    gameObject.GetComponent<MonsterController>().target = gameObject.GetComponent<EnemySight>().transform;
                    monsterBehaviour = MonsterBehaviours.Patrol;
                    gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterController.MonsterBehaviours.Patrol;
                    gameObject.GetComponent<EnemySight>().monsterBehaviour = EnemySight.MonsterBehaviours.Patrol;
                    fixertest = 1;
                }
            }
        }
    }

    //if the player collides with this object, the monster wins a round revert to a patrol state and send the player and monster to their start locations.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemySightPlayerSpotted = false;
            gameObject.GetComponent<QuerySearch>().EnvironementalQuerySearch();
            gameObject.GetComponent<MonsterController>().target = transform;
            gameObject.GetComponent<MonsterController>().target = gameObject.GetComponent<MonsterController>().transform;
            gameObject.GetComponent<MonsterController>().target = gameObject.GetComponent<EnemySight>().transform;
            player.transform.position = playerStart.position;
            monster.transform.position = monsterStart.position;
            player.transform.rotation = playerRotation;
            monster.transform.rotation = monsterRotation;
            gameManager.GetComponent<GameManager>().loseInt++;
            gameManager.GetComponent<GameManager>().roundInt++;
            monsterBehaviour = MonsterBehaviours.Patrol;
            gameObject.GetComponent<MonsterController>().monsterBehaviour = MonsterController.MonsterBehaviours.Patrol;
            gameObject.GetComponent<QuerySearch>().monsterBehaviour = EnemySight.MonsterBehaviours.Patrol;
            
        }
    }
}
