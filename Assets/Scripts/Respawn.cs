using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 RespawnPosition;
    
    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (collision.gameObject.name == "Player")
        {
            otherGameObject.transform.position = RespawnPosition;
        }
    }
}