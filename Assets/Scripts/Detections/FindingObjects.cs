using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingObjects : MonoBehaviour
{
    public float rayDistance = 4f;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        // player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //Direção do raio
        Ray landingRay = new Ray(transform.position, transform.forward);
        // Ray landingRay = new Ray(transform.position, player.transform.position);
        Ray leftRay = new Ray(transform.position, transform.up);
        Ray rightRay = new Ray(transform.position, transform.right);
        Ray forwardRay = new Ray(transform.position, transform.forward);
        //   ray = this.transform.position, player.transform.position;

        Physics.Raycast(leftRay, rayDistance);
        Physics.Raycast(rightRay, rayDistance);
        Physics.Raycast(forwardRay,rayDistance);
        
        // Ray landingRay = new Ray(transform.position, objetos[0].transform.position);

        // Debug.DrawRay(transform.position, transform.forward * rayDistance);
        // Debug.DrawRay(transform.position, player.transform.position * rayDistance);
        Debug.DrawRay(landingRay.origin, landingRay.direction * rayDistance);
        Debug.DrawRay(leftRay.origin, leftRay.direction * rayDistance, Color.green);
        Debug.DrawRay(rightRay.origin, rightRay.direction * rayDistance, Color.red);
        Debug.DrawRay(rightRay.origin, rightRay.direction * rayDistance, Color.blue);

        // Debug.DrawRay(transform.position, transform.forward * rayDistance);

        if (Physics.Raycast(landingRay, out hit, rayDistance))
            if (hit.collider.tag == "ObjectRaycast")
                Tocou();

    }

    private void Tocou()
    {
        Debug.Log("tocou");
    }
}
