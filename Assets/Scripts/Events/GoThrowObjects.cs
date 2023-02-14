using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThrowObjects : MonoBehaviour
{
    private GameObject parede;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "IsSmall" && name == "Dog")
        {
            parede = hit.gameObject;
            parede.GetComponent<BoxCollider>().isTrigger = true;

        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IsSmall" && name == "Girl")
        {
            parede = other.gameObject;
            parede.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
