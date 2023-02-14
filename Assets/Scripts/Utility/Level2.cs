using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cameraRoom1;
    public Cinemachine.CinemachineVirtualCamera cameraRoom2;
    public GameObject grupoPlayers;

    public void StartLevel2()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 newPosition = this.transform.position; // new Vector3(teleportTo.transform.position.x, 0, 0);

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = newPosition;
        }

        cameraRoom1.enabled = false;
        cameraRoom2.enabled = true;
        cameraRoom2.Follow = grupoPlayers.transform;
        cameraRoom2.LookAt = grupoPlayers.transform;
        // cameraRoom2.m_Lens.FieldOfView = 70;
        // cameraRoom2.transform.rotation.x = 30;
    }
}
