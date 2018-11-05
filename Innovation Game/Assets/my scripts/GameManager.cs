using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Transform playerStart, monsterStart;
    public Text win, lose, roundNo, fin;
    public int winInt, loseInt, roundInt, totalRounds;

    public GameObject player, monster;

    public Quaternion playerRotation, monsterRotation;

    // Use this for initialization
    public void Start()
    {
        winInt = 0;
        loseInt = 0;
        roundInt = 1;
        totalRounds = 5;

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
        lose.text = "seeker won: " + loseInt + "/" + totalRounds + " rounds";
        roundNo.text = "Round number " + roundInt + "/" + totalRounds;
        //print(win.text);
    }

    // Update is called once per frame
    void Update()
    {

        win.text = "player won: " + winInt + "/" + totalRounds + " rounds";
        lose.text = "seeker won " + loseInt + "/" + totalRounds + " rounds";
        roundNo.text = "Round number " + roundInt + "/" + totalRounds;

        //if the player presses e close the instructions text + image
        if (Input.GetKeyDown("e"))
        {
            GameObject.Find("Instructions").GetComponent<Image>().enabled = false;
            GameObject.Find("Instructions Text").GetComponent<Text>().enabled = false;
        }

        //if the round number is greater than or equal to 4 and if the player has won more rounds than the seeker the player wins else the seeker wins
        if ((winInt >= 3) && (winInt > loseInt))
        {
            fin.enabled = true;
            fin.text = "player wins";
            Time.timeScale = 0;
        } else if ((loseInt >= 3) && (loseInt > winInt)) {
            fin.enabled = true;
            fin.text = "seeker wins";
            Time.timeScale = 0;
        }
        
    }
}