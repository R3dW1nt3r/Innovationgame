using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    Rigidbody rigidbody;
    Camera viewCamera;

    public GameObject rayStart,rayEnd;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;

        //Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit);
	}
	
	// Update is called once per frame
	void Update () {
        //Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 50f);
        if (Physics.Raycast(rayStart.transform.position, -(rayStart.transform.position - rayEnd.transform.position).normalized, out hit, 50f)) {
            if (hit.transform.tag == "Player")
                print("hitty");
        }
            
    }
}
