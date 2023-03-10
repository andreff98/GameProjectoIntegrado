using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class EnemyFieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Animator animator;
    public Light luz;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        luz = GetComponent<Light>();
    }

    

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);


        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;


            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) { 
                    canSeePlayer = true;
                    animator.Play("Falling Back Death");
                    luz.color = Color.red;



                } else {
                    canSeePlayer = false; 
                }
            }
            else
                canSeePlayer = false; 
        }
        else if (canSeePlayer)
            canSeePlayer = false; 

    }
}