using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ClimbPlayer
{
    public bool canClimb { get; set; }
    public bool isClimbing { get; set; }
    public Vector3 climbingPosition { get; set; }
    public CharacterController controller { get; set; }
    public float climbSpeed { get; set; }



    public ClimbPlayer()
    {
        climbSpeed = 5f;
    }
}

public class KidClimbing : MonoBehaviour
{
    ClimbPlayer climbPlayer;
    public GameObject player;
    public Transform target;// Objeto para teletransportar no final (não necessário)
    private Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        climbPlayer = new ClimbPlayer();
        climbPlayer.controller = GetComponent<CharacterController>();
        //fim = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartClimbing();

        if (climbPlayer.isClimbing)
            Climb();
    }

    /// <summary>
    /// Subir a escada
    /// </summary>
    private void Climb()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Subir
            climbPlayer.controller.Move(Vector3.up * Time.deltaTime);



        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //Descer
            climbPlayer.controller.Move(Vector3.down * Time.deltaTime);
            animator.Play("ClimbDown");

        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // Voltar a carregar no I sai da escada e volta ao normal 
            VoltarNormal();
        }
        else
        {
            // Parar
            climbPlayer.controller.Move(Vector3.zero * Time.deltaTime);
            animator.Play("StopClimb");
        }
    }

    /// <summary>
    /// Premir tecla I para começar a subir
    /// </summary>
    private void StartClimbing()
    {
        if (!climbPlayer.canClimb) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            climbPlayer.isClimbing = true;
            GetComponent<KidPlayerMovement>().enabled = false;


            animator.SetBool("isClimbing", true);

            if(transform.rotation.y > 0 && transform.rotation.y < 180)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.left);

            } else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.right);

            }

        }
    }

    private void VoltarNormal()
    {
        // Estado fora da escada
        climbPlayer.isClimbing = false;
        climbPlayer.canClimb = false;

        //Ativar script de mover personagem
        GetComponent<KidPlayerMovement>().enabled = true;

        animator.SetBool("isClimbing", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isGrounded", true);


    }

    private void EndClimbing()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            climbPlayer.isClimbing = false;
            climbPlayer.climbingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Inicio")
        {
            climbPlayer.canClimb = true;



        }
        else if (other.gameObject.tag == "Fim")
        {
            player.transform.position = target.transform.position;
            VoltarNormal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Inicio" && GetComponent<KidPlayerMovement>().enabled) || other.gameObject.tag == "Fim")
        {
            VoltarNormal();
        }
    }
}

      