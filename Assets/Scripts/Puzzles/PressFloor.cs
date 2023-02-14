using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressFloor : MonoBehaviour
{
    public float speedMovement = 0.5f;

    [SerializeField]
    private GameObject MovingPlataform;
    [SerializeField]
    private GameObject StartPostion;
    [SerializeField]
    private GameObject FinalPostion;

    private bool IsPressedPlane;
    private bool MovingForward = true;

    // Start is called before the first frame update
    void Start()
    {
        // MovingPlataform.AddComponent<Rigidbody>();
        // MovingPlataform.GetComponent<Rigidbody>().useGravity = false;
        // MovingPlataform.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (!IsPressedPlane)
            return;

        if (MovingForward)
            MovePlataformForward();
        else
            MovePlataformBackwards();
    }

    void MovePlataformForward()
    {
        Vector3 movement = Vector3.forward;
        MovingPlataform.transform.position = Vector3.Lerp(MovingPlataform.transform.position, FinalPostion.transform.position, speedMovement * Time.deltaTime);

        // Validação Paragem
        if (Vector3.Distance(MovingPlataform.transform.position, FinalPostion.transform.position) < 0.1f)
            MovingForward = false;
    }

    void MovePlataformBackwards()
    {
        MovingPlataform.transform.position = Vector3.Lerp(MovingPlataform.transform.position, StartPostion.transform.position, speedMovement * Time.deltaTime);

        // Validação Paragem
        if (Vector3.Distance(MovingPlataform.transform.position, StartPostion.transform.position) < 0.1f)
            MovingForward = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerDog"))
        {
            MovingPlataform.GetComponent<AudioSource>().Play();
            IsPressedPlane = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerDog"))
        {
            MovingPlataform.GetComponent<AudioSource>().Stop();
            IsPressedPlane = false;
        }
    }
}
