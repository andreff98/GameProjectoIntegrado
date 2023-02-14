using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    bool canpickup, canpickupKey; //a bool to see if you can or cant pick up the item
    public GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    public GameObject key; // the gameobject onwhich you collided with

    bool hasItem, hasItemKey; // a bool to see if you have an item in your hand
    // Start is called before the first frame update
    public Animator animator;
    public Animator animatorGate;

    public bool canDestroy, canOpen;
    public GameObject destroyedPlanks;
    public GameObject planks, planks1;
    int clickCount = 0;
    public GameObject trigger1, trigger2, trigger3;
    public GameObject aviso, aviso1;



    void Start()
    {
        canpickup = false;    //setting both to false
        canpickupKey = false;
        hasItemKey = false;
        hasItem = false;
        canDestroy= false;
        canOpen= false;
        animator = GetComponent<Animator>();

        aviso.SetActive(false);
        aviso1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (canpickup == true) // if you enter thecollider of the objecct
        {



            if (Input.GetKeyDown(KeyCode.E))  // can be e or any key
            {

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_crouch") || animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_Run") ||
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_JumpRun"))
                {
                    canpickup = false;
                    
                }
                else
                {
                    canpickup = true;
                }



                animator.Play("Picking Up");

                ObjectIwantToPickUp.transform.rotation = myHands.transform.rotation;
                ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                ObjectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
                ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
                hasItem = true;


                

                GameObject.FindGameObjectWithTag("camChange1").SetActive(false);
                GameObject.FindGameObjectWithTag("camChange3").SetActive(false);
                GameObject.FindGameObjectWithTag("camChange4").SetActive(false);
                trigger1.SetActive(true); trigger2.SetActive(true); trigger3.SetActive(true);

                if (hasItemKey)
                {
                    key.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

                    key.transform.parent = null;
                }
            }

        }

        if (canpickupKey == true) // if you enter thecollider of the objecct
        {



            if (Input.GetKeyDown(KeyCode.E))  // can be e or any key
            {

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_crouch") || animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_Run") ||
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || animator.GetCurrentAnimatorStateInfo(0).IsName("Girl_JumpRun"))
                {
                    canpickupKey = false;

                }
                else
                {
                    canpickupKey = true;
                }



                animator.Play("Picking Up");

             


                key.transform.rotation = myHands.transform.rotation;
                key.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                key.transform.position = myHands.transform.position; // sets the position of the object to your hand position
                key.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
                hasItemKey = true;

                

         


            }

        }

        if(hasItem == true && canpickupKey== true)
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

            ObjectIwantToPickUp.transform.parent = null;
        }

        if(hasItemKey == true && canpickup == true)
        {
            key.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

            key.transform.parent = null;
        }

        if (hasItem== true && canDestroy == true)
        {
            BreakObjects();
            BreakObjects2();



        }

        if (hasItemKey == true && canOpen == true)
        {
            OpenGate();
        }

        if(ObjectIwantToPickUp.transform.position == myHands.transform.position)
        {
           aviso.SetActive(false);

        }


        if (Input.GetKeyDown(KeyCode.Q) && hasItem == true) // if you have an item and get the key to remove the object, again can be any key
        {
            ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again

            ObjectIwantToPickUp.transform.parent = null; // make the object no be a child of the hands

          aviso.SetActive(false);

        }

   

    }

    public void OpenGate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animatorGate.SetBool("Open", true);
            
          

        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            key.SetActive(false);
        }
    }


   public void BreakObjects()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetBool("isBreaking", true);
            clickCount++;

            if (clickCount >= 5)
            {

                Instantiate(destroyedPlanks, transform.position, transform.rotation);
                Destroy(planks);
                Debug.Log("A PARTIR");
                clickCount= 0;
            }

            

           
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            animator.SetBool("isBreaking", false);

        }
    }

    public void BreakObjects2()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetBool("isBreaking", true);
            clickCount++;

            if (clickCount >= 5)
            {

                Instantiate(destroyedPlanks, transform.position, transform.rotation);
                Destroy(planks1);
                Debug.Log("A PARTIR");
                clickCount = 0;
            }




        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            animator.SetBool("isBreaking", false);

        }
    }


    private void OnControllerColliderHit(ControllerColliderHit martelo) // to see when the player enters the collider
    {
        if (martelo.gameObject.tag == "object") //on the object you want to pick up set the tag to be anything, in this case "object"
        {

            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = martelo.gameObject; //set the gameobject you collided with to one you can reference

            Debug.Log("toca");


            aviso.SetActive(true);

            aviso.transform.position = ObjectIwantToPickUp.transform.position + new Vector3(0, 3.5f, 0);


        }
        else
        {
            

            canpickup = false;
            aviso.SetActive(false);

        }

        if(martelo.gameObject.tag == "planks") {
            
            canDestroy= true;
            Debug.Log("Pode destruir");

        }

        if(martelo.gameObject.tag == "Key")
        {
            canpickupKey = true;  //set the pick up bool to true
            key = martelo.gameObject;

            aviso1.SetActive(true);
            Debug.Log("toca");
            aviso1.transform.position = key.transform.position + new Vector3(0, 3.5f, 0);
        }

        if(martelo.gameObject.tag == "gate")
        {
            canOpen = true;
            Debug.Log("pode abrir");


        }



    }

}
