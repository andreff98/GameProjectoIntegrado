using System;
using UnityEngine;

[System.Serializable]
public class Player //  : MonoBehaviour
{
    [NonSerialized] public CharacterController CharControl;

    [Serializable]
    public class Dog : Player
    {
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float RotationSpeed = 720f;
        [NonSerialized] public Vector3 moveDirection;
        [NonSerialized] public float targetAngle;
        [NonSerialized] public float magnitude;
        [NonSerialized] public float ySpeed;
        [NonSerialized] public Animator animation;

        public void GetMoveInput()
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
    }

    [Serializable]
    public class PickObject : Player
    {

    }
}
