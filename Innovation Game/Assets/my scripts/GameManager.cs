using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform playerStart, monsterStart;
    public Text win, lose, roundNo, fin;
    public int winInt, loseInt, roundInt, totalRounds;

    public GameObject player, monster;

    public Quaternion playerRotation, monsterRotation;

    // Use this for initialization
    public void Start () {
        winInt = 0;
        loseInt = 0;
        roundInt = 0;
        totalRounds = 5;
        //playerStart = GameObject.FindGameObjectWithTag("PlayerStart").transform;
        //monsterStart = GameObject.FindGameObjectWithTag("MonsterStart").transform;

        win = GameObject.Find("win ratio").GetComponent<Text>();
        lose = GameObject.Find("lose ratio").GetComponent<Text>();
        roundNo = GameObject.Find("round Numbers").GetComponent<Text>();
        fin = GameObject.Find("fin").GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("Player");
        monster = GameObject.FindGameObjectWithTag("Monster");
        playerRotation = player.transform.rotation;
        monsterRotation = monster.transform.rotation;
        print("at start win value is " + win);
        win.text = "player won: " + winInt + "/" + totalRounds + " rounds";
        lose.text = "player lose: " + loseInt + "/" + totalRounds + " rounds";
        roundNo.text = "Round number " + roundInt + "/" + totalRounds;
        //print(win.text);
    }
	
	// Update is called once per frame
	void Update () {

        win.text = "player won: " + winInt + "/" + totalRounds + " rounds";
        lose.text = "player lose: " + loseInt + "/" + totalRounds + " rounds";
        roundNo.text = "Round number " + roundInt + "/" + totalRounds;

        if (Input.GetKeyDown("q")) {
            fin.enabled = true;
        }

        if (roundInt >= totalRounds) {
            fin.enabled = true;
            if (winInt > loseInt)
            {
                fin.text = "player wins";
            } else
                fin.text = "player loses";
            Time.timeScale = 0;
        }

    }
}
