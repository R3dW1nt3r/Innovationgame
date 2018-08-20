using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    //public Enemy[] enemies;
    public TriggerState enter;
    public TriggerState exit;
        
    public float enemy3LeavesTimer = 60.0f;
    public float enemy2AttacksTimer = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}


    //I can use this for a raycast to spot the player
    /*void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"){
            switch (enter) { 
                case TriggerState.Patrol:
                    ChangeEnemyStates(0);
                    break;
                case TriggerState.Hide:
                    ChangeEnemyStates(1);
                    break;
                case TriggerState.Attack:
                    ChangeEnemyStates(2);
                    break;
            }
        }
    }*/

    //Changes the state of an enemy
    /*private void ChangeEnemyStates(int state) { 
        foreach (Enemy enemy in enemies) {
            enemy.newState = state;
            enemy.currentState = enemy.newState; 
        }
    }*/

    //Trigger States for enemies
    public enum TriggerState { 
        Patrol,
        Hide,
        Attack
    }

    /*//Changes enemy state on exit transition
    void OnTriggerExit(Collider other) { 
        if (other.tag == "Player") { 
            switch (exit) { 
                case TriggerState.Patrol:
                    ChangeEnemyStates(0);
                    break;
                case TriggerState.Hide:
                    ChangeEnemyStates(1);
                    break;
                case TriggerState.Attack:
                    ChangeEnemyStates(2);
                    break;
            }
        }
    }*/
}
