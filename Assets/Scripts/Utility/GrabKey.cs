using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GrabKey : MonoBehaviour
{
    public GameObject key;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Submit"))
            {
                key.GetComponent<AudioSource>().Play();
                key.gameObject.SetActive(false);
                GameObject.Find("KeyUI").GetComponent<RawImage>().enabled = true;
            }
        }
    }
}
