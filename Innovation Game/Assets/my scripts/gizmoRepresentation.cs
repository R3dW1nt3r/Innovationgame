using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoRepresentation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGizmoDraw()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.gameObject, Vector3.one);
    }
}
