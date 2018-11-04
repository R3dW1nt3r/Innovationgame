using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoRepresentation : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.gameObject.transform.position, Vector3.one);
    }
}
