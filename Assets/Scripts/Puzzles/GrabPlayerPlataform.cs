using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrabPlayerPlataform : MonoBehaviour
{
    private Transform PreviousParent;

    private void Start()
    {
        PreviousParent = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerDog"))
            other.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerDog"))
            other.gameObject.transform.parent = PreviousParent;
    }
}
