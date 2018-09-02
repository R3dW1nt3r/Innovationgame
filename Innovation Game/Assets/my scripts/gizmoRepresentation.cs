using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoRepresentation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //OnGizmoDraw();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.gameObject.transform.position, Vector3.one);
    }
}
