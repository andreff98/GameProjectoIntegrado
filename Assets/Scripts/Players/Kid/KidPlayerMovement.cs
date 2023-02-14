using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class KidPlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;
    public float crouchButtonGracePeriod;
    private Animator animator;
    private CharacterController cc;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float? crouchButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;
    private bool isCrouching;
    private bool standUp;
    private bool isClimbing;
    private bool isMoving;
    public float climbSpeed = 5f;
    private float prevSpeed;
    private bool climbStop;
    private bool isStoped;
    float horizontalInput;
    float verticalInput;
    float magnitude;

    Vector3 lastClimbPos;
    Vector3 movementDirection;

    public bool isComando;


    public bool isDead;
    public GameObject deadMenuUI;
    public GameObject cam, martelo;



    public bool controlChange, controlChange1, controlChange2;
    public CinemachineVirtualCamera virtualCamera;

    public float fov;


    [SerializeField]
    private float forceMagnitude;

    #region Eventos do Unity
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        originalStepOffset = cc.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        CrouchPlayer();
        JumpPlayer();
        MovePlayer();


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_crouch") || animator.GetCurrentAnimatorStateInfo(0).IsName("CrouchedWalking") || animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death"))
        {
            cc.height = 2f;
            cc.center = Vector3.up;
        }
        else
        {
            cc.height = 3.6f;
            cc.center = new Vector3(0f, 1.88f, 0f);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Picking Up"))
        {
            speed = 0f;
        }
        else
        {
            speed = 5f;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Back Death"))
        {
            isDead = true;



        }

        if (isDead)
        {
            deadMenuUI.SetActive(true);
            speed = 0f;
            rotationSpeed = 0f;
            jumpSpeed = 0f;
        }

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "camChange")
        {
            controlChange2 = true;
            Debug.Log("TOCA");
            cam.transform.rotation = new Quaternion(0.148001477f, -0.492644459f, 0.0854486972f, 0.853285193f);
            
        }

        if (other.gameObject.tag == "camChange1")
        {
            controlChange1 = true;
            cam.transform.rotation = new Quaternion(0, 0.99619472f, -0.0871557817f, 0);
            virtualCamera.m_Lens.FieldOfView = fov;

        }

        if (other.gameObject.tag == "camChange2")
        {
            controlChange1 = true;
            cam.transform.rotation = new Quaternion(0.0210848972f, 0.966603458f, -0.0845668837f, 0.241001382f);
            virtualCamera.m_Lens.FieldOfView = 15;

        }

        if (other.gameObject.tag == "camChange3")
        {
            cam.transform.rotation = new Quaternion(0.079417944f, 0.834127545f, -0.1246612f, 0.531397879f);

        }

        if (other.gameObject.tag == "camChange4")
        {
            controlChange1 = true;
            cam.transform.rotation = new Quaternion(0.131275833f, 0.784035683f, -0.177409112f, 0.580155849f);
            virtualCamera.m_Lens.FieldOfView = 78;
            Debug.Log("TOCA NO 4");

        }

        if (other.gameObject.tag == "camChange5")
        {
            controlChange = true;
            virtualCamera.m_Lens.FieldOfView = 38;

            cam.transform.rotation = new Quaternion(0.0604246408f, -0.381864071f, 0.0250287037f, 0.921901464f);

        }

        if (other.gameObject.tag == "camChange6")
        {
            controlChange2 = true;
            controlChange = false;
            virtualCamera.m_Lens.FieldOfView = 38;
            cam.transform.rotation = new Quaternion(0.053575132f, -0.572348356f, 0.0375137068f, 0.81739825f);

        }

        if (other.gameObject.tag == "camChangeKey")
        {
            cam.transform.rotation = new Quaternion(0.113544732f, 0.824344218f, -0.178229541f, 0.5251652f);
            virtualCamera.m_Lens.FieldOfView = 17;

        }


        if (other.gameObject.tag == "ChangeScene")
        {
            SceneManager.LoadScene("StartMenu");

        }



        if (other.gameObject.tag == "DeadArea")
        {
              isDead = true;
          //  camAnim.Play("Busted");

            
        }

        if(other.gameObject.tag == "ab")
        {
            martelo.transform.position = new Vector3(459.700012f, 0.790000021f, 54.7099991f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "camChange")
        {
            controlChange2 = false;
            Debug.Log("SAI");
            cam.transform.rotation = new Quaternion(0.0468287654f, 0.840182126f, -0.0735064372f, 0.535255015f);

        }

        if (other.gameObject.tag == "camChange1")
        {
            controlChange1 = false;
            cam.transform.rotation = new Quaternion(0.0468287654f, 0.840182126f, -0.0735064372f, 0.535255015f);
            virtualCamera.m_Lens.FieldOfView = 38.86f;
            
        }

        if (other.gameObject.tag == "camChange2")
        {
            controlChange1 = false;
            cam.transform.rotation = new Quaternion(0.131275833f, 0.784035683f, -0.177409112f, 0.580155849f);
            virtualCamera.m_Lens.FieldOfView = 78;

        } 

        if (other.gameObject.tag == "camChange3" )
        {
            cam.transform.rotation = new Quaternion(0.0468287654f, 0.840182126f, -0.0735064372f, 0.535255015f);
            controlChange1 = false;


        }
        if(other.gameObject.tag == "camChangeKey")
        {
            cam.transform.rotation = new Quaternion(0.0468287654f, 0.840182126f, -0.0735064372f, 0.535255015f);

            virtualCamera.m_Lens.FieldOfView = 38.86f;

        }




    }



    #endregion

    void ClearAnimations()
    {
        isCrouching = isGrounded = isJumping = standUp = false;

        animator.SetBool("isCrouching", false);
        animator.SetBool("isGrounded", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("standUp", false);
    }


    void ClearAnimations(string activeAnimation)
    {
        isCrouching = isGrounded = isJumping = standUp = false;

        animator.SetBool("isCrouching", false);
        animator.SetBool("isGrounded", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("standUp", false);

        if (!string.IsNullOrEmpty(activeAnimation))
            animator.SetBool(activeAnimation, true);
    }

    void Move()
    {
        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        cc.Move(velocity * Time.deltaTime);
    }

    void MovePlayer()
    {

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Move();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void GetMoveInput()
    {

        horizontalInput = (isComando) ? Input.GetAxis("ManipuloDireitoHorizontal") : Input.GetAxis("Horizontal");
        verticalInput = (isComando) ? Input.GetAxis("ManipuloDireitoVertical") : Input.GetAxis("Vertical");
        movementDirection = new Vector3(verticalInput, 0, -horizontalInput);
        magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if(controlChange == true)
        {
            horizontalInput = (isComando) ? Input.GetAxis("ManipuloDireitoHorizontal") : Input.GetAxis("Horizontal");
            verticalInput = (isComando) ? Input.GetAxis("ManipuloDireitoVertical") : Input.GetAxis("Vertical");
            movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        } else if(controlChange1 == true)
        {
            horizontalInput = (isComando) ? Input.GetAxis("ManipuloDireitoHorizontal") : Input.GetAxis("Horizontal");
            verticalInput = (isComando) ? Input.GetAxis("ManipuloDireitoVertical") : Input.GetAxis("Vertical");
            movementDirection = new Vector3(-horizontalInput, 0, -verticalInput);

        } else if(controlChange2)
        {
            horizontalInput = (isComando) ? Input.GetAxis("ManipuloDireitoHorizontal") : Input.GetAxis("Horizontal");
            verticalInput = (isComando) ? Input.GetAxis("ManipuloDireitoVertical") : Input.GetAxis("Vertical");
            movementDirection = new Vector3(-verticalInput, 0, horizontalInput );
        }


    }
    void CrouchPlayer()
    {
        crouchButtonPressedTime = (Input.GetButtonDown("Crouch")) ? Time.time : 0f;
        lastGroundedTime = (cc.isGrounded) ? Time.time : 0f;


        if (Time.time - lastGroundedTime > crouchButtonGracePeriod) return;

        ClearAnimations();

        cc.stepOffset = originalStepOffset;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
            isCrouching = true;
            crouchButtonPressedTime = lastGroundedTime = 0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            cc.stepOffset = 0;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("standUp", true);

        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_crouch") || animator.GetCurrentAnimatorStateInfo(0).IsName("CrouchedWalking"))
        {
            cc.height = 2f;
            cc.center = Vector3.up;
        }
        else
        {
            cc.height = 3.6f;
            cc.center = new Vector3(0f, 1.88f, 0f);
        }
    }

    void PickObject()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Picking Up"))
        {
            speed = 0f;
        }
        else
        {
            speed = 5f;
        }
    }

    void JumpPlayer()
    {
        jumpButtonPressedTime = (Input.GetButtonDown("Jump")) ? Time.time : 0f;


        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {

            cc.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            ClearAnimations("isGrounded");
            isGrounded = true;
            isJumping = false;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJumping = true;
                jumpButtonPressedTime = lastGroundedTime = null;
            }
        }
        else
        {
            cc.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -3)
            {
                animator.SetBool("isFalling", true);
                Move();
            }
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigidBody = hit.collider.attachedRigidbody;



        if (rigidBody != null && hit.gameObject.tag == "PushableObject")
        {


            var forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();


            rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);


            animator.Play("Push");



        }
    }
}

            


      


