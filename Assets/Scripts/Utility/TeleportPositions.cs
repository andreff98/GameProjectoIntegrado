using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPositions : MonoBehaviour
{
    public GameObject teleportTo;
    private GameObject previousTeleport;

    void OnTriggerEnter(Collider other)
    {
        var players =  GameObject.FindGameObjectsWithTag("Player");
        Vector3 newPosition = teleportTo.transform.position;// new Vector3(teleportTo.transform.position.x, 0, 0);

        for(int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = newPosition;
        }

        previousTeleport.transform.position = this.transform.position;
        // transform.position = teleportTo.transform.position;

        // teleportTo.transform.position = other.transform.position;
    }
}
