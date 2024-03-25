using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed = 5.0f;
    //public float maxSpeed;

    private bool isSliding = false;

    public float slideDuration = 1.5f;


    private int desiredLane = 1;   //0:left 1:middle 2:right
    public float laneDistance = 4; //distance between two lanes 
    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        animator.SetBool("isGameStarted", true);
        //Increase Speed
        /*if(forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }*/
        direction.z = forwardSpeed;

        animator.SetBool("isGrounded", controller.isGrounded);
        if(controller.isGrounded) //checks if player is on the ground before jumping
        {
            if(SwipeManager.swipeUp)
            {
                Jump();
            }
            if(SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
            if(SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
                direction.y = -10;
            }
        }

        //Inputs of lane we should be
        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if(SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        //Calculate future positions
        Vector3 targetPosition  = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        
        if(transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                controller.Move(moveDir);
            }
            else
            {
                controller.Move(diff);
            }
        }    
        controller.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {
        StopCoroutine(Slide());
        animator.SetBool("isSliding", false);
        controller.center = Vector3.zero;
        controller.height = 2;
        isSliding = false;
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("ObstacleHit");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.25f/ Time.timeScale);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds((slideDuration - 0.25f)/Time.timeScale);

        animator.SetBool("isSliding", false);

        controller.center = Vector3.zero;
        controller.height = 2;

        isSliding = false;
    }

    public void SetSpeed(float modifier)
    {
        forwardSpeed = 5.0f + modifier;
    }
}
