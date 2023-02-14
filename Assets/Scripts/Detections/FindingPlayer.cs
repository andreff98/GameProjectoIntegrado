using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindingPlayer : MonoBehaviour
{
    private GameObject[] players;
    private Ray[] rayrays;
    private RaycastHit hit;

    public float rayDistance = 4f;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); //  GetComponent<GameObject[]>().ToArray();
        rayrays = new Ray[players.Length];
    }

    private void FixedUpdate()
    {
        if (players.Length == 0) return;

        CastRay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Não funciona como quero
    /// </summary>
    private void CastRay()
    {
        foreach(var player in players)
        {
            foreach(var ray in rayrays)
            {
                 Ray landingRey = new Ray(this.transform.position, transform.TransformDirection(player.gameObject.transform.position));
                // Ray landingRey = new Ray(player.gameObject.transform.position, new Vector3(-11.19f, 0.50f, -73.52f));
                // Ray landingRey = new Ray(this.transform.position, new Vector3(-11.19f, 0.50f, -73.52f));

                // landingRey.direction.magnitude = player.transform.position.magnitude;
                // landingRey = new Ray(player.transform.position,this.transform.position);

                DebugRay(landingRey);
                this.transform.rotation = player.gameObject.transform.rotation;
                // Debug.Log(landingRey.direction * rayDistance + " <-> " + player.transform.position);
                ColliderRay(landingRey);
            }
        }
    }

    //Ver se o ray tocou na personagem
    private void ColliderRay(Ray landingRey)
    {
        if (Physics.Raycast(landingRey, out hit, rayDistance))
            if (hit.collider.tag == "Player")
                CollisionDetected();
    }

    void CollisionDetected()
    {
        Debug.Log("tocou");
    }

    void DebugRay(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
