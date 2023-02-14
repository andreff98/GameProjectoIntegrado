using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DogPlayerMovement : MonoBehaviour
{
    [SerializeField] private Player.Dog player;

    private void Start()
    {
        player.CharControl = GetComponent<CharacterController>();
        player.animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        DogMoviment();
    }

    private void GetMoveInput()
    {
        player.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        player.magnitude = Mathf.Clamp01(player.moveDirection.magnitude) * player.moveSpeed;
        player.ySpeed += Physics.gravity.y * Time.deltaTime;
    }

    private void Move()
    {
        Vector3 velocity = player.moveDirection * player.magnitude;
        velocity.y = player.ySpeed;
        player.CharControl.Move(velocity * Time.deltaTime);
        player.animation.SetBool("isMoving", true);
    }

    /// <summary>
    /// Método para mover Cao
    /// </summary>
    private void DogMoviment()
    {
        if (player.moveDirection != Vector3.zero)
        {
            // player.animation.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(player.moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, player.RotationSpeed * Time.deltaTime);
            Move();
        }
        else if(!player.CharControl.isGrounded)
        {
            Move();
        }
        else
        {
            // Move();
            //Animação parado
            player.animation.SetBool("isMoving", false);
        }
    }
}
