using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerDog"))
        {
            GameObject.Find("Dog").GetComponent<Animator>().Play("howl");
            GameObject.Find("Dog").GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerDog"))
        {
            GameObject.Find("Dog").GetComponent<Animator>().Play("howl");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerDog"))
        {
            GameObject.Find("Dog").GetComponent<AudioSource>().Stop();
        }
    }
}
