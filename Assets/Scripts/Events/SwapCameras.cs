using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCameras : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera fromCamera;
    public Cinemachine.CinemachineVirtualCamera toCamera;
    public GameObject grupoPlayers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SwapCamerasEvent();
    }

    private void SwapCamerasEvent()
    {
        fromCamera.enabled = false;
        toCamera.enabled = true;
        toCamera.Follow = grupoPlayers.transform;
        toCamera.LookAt = grupoPlayers.transform;
    }

    private void Awake()
    {
        if (grupoPlayers == null)
            grupoPlayers = GameObject.FindGameObjectWithTag("GroupCamera");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
