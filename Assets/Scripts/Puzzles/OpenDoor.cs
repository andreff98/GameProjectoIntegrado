using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    public GameObject cellDoor;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Submit"))
            {
                cellDoor.GetComponent<Animator>().enabled = true;
                cellDoor.GetComponent<AudioSource>().Play();
                GameObject.Find("KeyUI").GetComponent<RawImage>().enabled = false;
            }
        }
    }
}
