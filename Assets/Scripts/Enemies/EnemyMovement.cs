using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float speedMovement = 2.5f;
    public float distance = 10f;

    [Serializable]
    public class Direction
    {
        public bool right = false;
        public bool left = false;
        public bool forward = true;
        public bool backwards = false;
    }
    public Direction direction;
    private Rigidbody rigidbodyrb;

    private Vector3 posicaoFinal;
    private Vector3 posicaoInicial;
    private bool IsForward = true;


    private void Awake()
    {
        rigidbodyrb = gameObject.AddComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // rigidbodyrb = GetComponent<Rigidbody>();
        rigidbodyrb.isKinematic = true;
        rigidbodyrb.useGravity = false;
        posicaoInicial = transform.localPosition;
        posicaoFinal = (direction.backwards) ? new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - distance) 
            : (direction.right) ? new Vector3(transform.localPosition.x + distance, transform.localPosition.y, transform.localPosition.z)
            : (direction.left) ? new Vector3(transform.localPosition.x - distance, transform.localPosition.y, transform.localPosition.z)
            : new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + distance);        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsForward)
            MoveForward();
        else if (!IsForward)
            MoveBackwords();
    }

    private void MoveForward()
    {
        // Vector3 movement = Vector3.forward * Time.deltaTime;
        Vector3 movement = (direction.backwards) ? Vector3.back : (direction.right) ? Vector3.right : (direction.left) ? Vector3.left : Vector3.forward;
        rigidbodyrb.position += movement * speedMovement * Time.deltaTime;

        // Validação Paragem
        if (Vector3.Distance(transform.localPosition, posicaoFinal) < 0.2f)
            IsForward = false;
    }

    private void MoveBackwords()
    {
        Vector3 movement = (direction.backwards) ? Vector3.forward : (direction.right) ? Vector3.left : (direction.left) ? Vector3.right : Vector3.back;
        // Vector3 movement = Vector3.back * Time.deltaTime;
        rigidbodyrb.position += movement * speedMovement * Time.deltaTime;

        // Validação Paragem
        if (Vector3.Distance(transform.localPosition, posicaoInicial) < 0.2f)
            IsForward = true;
    }
}
 