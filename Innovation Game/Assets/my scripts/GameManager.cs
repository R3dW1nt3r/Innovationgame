using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform playerStart, monsterStart;
    public Text win, lose, roundNo;
    public int winInt, loseInt, roundInt, totalRounds;

    public GameObject player, monster;

    public Quaternion playerRotation, monsterRotation;

    // Use this for initialization
    void Start () {
        winInt = 0;
        loseInt = 0;
        roundInt = 0;
        totalRounds = 5;
        playerStart = GameObject.FindGameObjectWithTag("Player").transform;
        monsterStart = GameObject.FindGameObjectWithTag("Monster").transform;

        player = GameObject.FindGameObjectWithTag("Player");
        monster = GameObject.FindGameObjectWithTag("Monster");
        playerRotation = player.transform.rotation;
        monsterRotation = monster.transform.rotation;
        print(win.text);
        win.text = "player won: " + winInt + "/" + totalRounds + " rounds";
        lose.text = "player lose: " + loseInt + "/" + totalRounds + " rounds";
        roundNo.text = "Round number " + roundInt + "/" + totalRounds;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q")) {
            win.text = "player won: " + winInt + "/" + totalRounds + " rounds";
            lose.text = "player lose: " + loseInt + "/" + totalRounds + " rounds";
            roundNo.text = "Round number " + roundInt + "/" + totalRounds;
        }
        
    }
}
