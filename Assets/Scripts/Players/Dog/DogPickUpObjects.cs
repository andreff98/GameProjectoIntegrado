using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPickUpObjects : MonoBehaviour
{
    [SerializeField] public GameObject bodyPart;
    [SerializeField] private GameObject grabRock;
    private MoveableObject grabObject;
    private LastMoveableObject lastGrabObject;
    private GameObject lastPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        grabObject = new MoveableObject();
        lastGrabObject = new LastMoveableObject();
        grabRock = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
        Dropdown();
    }
    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "object") //Verificar se o objeto tem a tag object para destinguir items que podem ser ou n�o pegados
        {
            Debug.Log("Tocou");
            canPickUP = true;
            objectToPickUp = hit.gameObject.GetComponent<GameObject>(); // Recolhe os dados do objeto que vai ser pegado
        }
    }
    */

    /// <summary>
    /// Quando entre da area do collider do objecto
    /// </summary>
    /// <param name="other">Objeto em quest�o</param>
    private void OnTriggerEnter(Collider other)
    {
        //Verificar se o objeto tem a tag object para destinguir items que podem ser ou n�o pegados
        if (other.gameObject.tag == grabObject.label)
        {
            grabObject.canPickUP = true;
            // grabObject.objectToPickUp = lastPosition = other.gameObject;


            grabObject.objectToPickUp = other.gameObject.GetComponentInChildren<GameObject>();
        }
    }

    /// <summary>
    /// Quando sai da area do collider do objecto deixa de conseguir pegar no objecto
    /// </summary>
    /// <param name="other">Objeto em quest�o</param>
    private void OnTriggerExit(Collider other)
    {
        //Verificar se o objeto tem a tag object para destinguir items que podem ser ou n�o pegados
        if (other.gameObject.tag == grabObject.label)
        {
            grabObject.canPickUP = false;
            // grabObject.objectToPickUp = default;
        }
    }

    /// <summary>
    /// Metod que pega no objeto e coloca na bodypart
    /// </summary>
    private void PickUp()
    {
        if (!grabObject.canPickUP) return;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            // Meter o objeto sem controlo da fisica
            grabObject.objectToPickUp.GetComponent<Rigidbody>().isKinematic = true;
            // lastPosition =  grabObject.objectToPickUp.GetComponentInParent<GameObject>();

            //Guardar posições anteriores
            // grabObject.LastParent = grabObject.objectToPickUp.transform.parent;
            // grabObject.LastPosition = grabObject.objectToPickUp.transform.position;
            // lastGrabObject.LastPosition = grabObject.objectToPickUp.transform.position;
            // lastGrabObject.LastParent = grabObject.objectToPickUp.GetComponentInParent<GameObject>();

            // O objeto a ser pegado vai para a posi��o da bodyport do player
            grabObject.objectToPickUp.transform.position = bodyPart.transform.position;

            // o objeto pegado passa a ser filho do player
            grabObject.objectToPickUp.transform.parent = bodyPart.transform;

            //Item is grabbed
            grabObject.hasItem = true;

           
        }
    }

    private void Dropdown()
    {
        if (!grabObject.hasItem) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
            grabObject.objectToPickUp.GetComponent<Rigidbody>().isKinematic = false;
            grabObject.objectToPickUp.transform.parent = null; // grabRock.transform;

            grabObject.objectToPickUp.transform.position = position;

            // grabObject.objectToPickUp.transform.position.y = Vector3.zero.y;
            grabObject.hasItem = false;

            //grabObject.objectToPickUp.transform.parent = lastPosition.transform;
            //grabObject.objectToPickUp.transform.position = lastPosition.transform.position;
            //Debug.Log(lastPosition.transform.position);
            //Debug.Log(lastPosition.transform.parent);

            // Debug.Log(grabObject.LastParent);
            //Meter no sitio de origem e no parent
            // grabObject.objectToPickUp.transform.parent = grabObject.LastParent;
            // grabObject.objectToPickUp.transform.position = grabObject.LastPosition;
        }
    }
}
