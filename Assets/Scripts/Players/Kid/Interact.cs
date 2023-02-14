using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Interact : MonoBehaviour
{
    // Não estou a conseguir fazer com raycast
    private bool IsNear = false;
    private GameObject terminalPanel;

    private void Awake()
    {
        terminalPanel = GameObject.Find("TerminalUI");
        if(terminalPanel != null )
            terminalPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3") && IsNear)
            ShowTerminalCode();
        else if (Input.GetButtonDown("Cancel") && IsNear)
            CloseTerminalCode();

        // RaycastHit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Girl_TPose")
        {
            // interaction.SetActive(true);
            IsNear = true;
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Girl_TPose")
        {
            terminalPanel.SetActive(false);
            IsNear = false;
        }
    }
    */


    void ShowTerminalCode()
    {
        terminalPanel.SetActive(true);
    }

    void CloseTerminalCode()
    {
        terminalPanel.SetActive(true);
    }
}
