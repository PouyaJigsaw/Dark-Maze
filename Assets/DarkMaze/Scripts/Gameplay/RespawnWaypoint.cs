using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnWaypoint : MonoBehaviour
{

    [SerializeField]
    Vector3 debugDrawSize = new Vector3(0.2f, 0.2f, 0.2f);



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, debugDrawSize);
    }
}
