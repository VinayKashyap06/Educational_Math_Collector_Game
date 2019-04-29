using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour {

    float speed;
    Rigidbody rb;
    float moveVertical, moveHorizontal;
    float speedH = 2.0f;
        
    private float pitch = 0.0f;

    public Animator playerAnimator;
	
	void Start ()
    {
        speed = 5f;
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        moveHorizontal=Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
       
        pitch += speedH * Input.GetAxis("Mouse X");
        
        speed += 0.01f;
        speed = Mathf.Clamp(speed, 5f, 30f);

        transform.eulerAngles = new Vector3(0,pitch,0);
        ChangeAnimations();
      

        transform.Translate(moveHorizontal,0, moveVertical);
      

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetBool("isJumping",true);
            rb.AddForce(new Vector3(0,50,0));
        }
        else
        {
            playerAnimator.SetBool("isJumping", false);
        }

	}

    private void ChangeAnimations()
    {
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            playerAnimator.SetBool("hasStopped", true);
            playerAnimator.SetBool("isGoingRight", false);
            playerAnimator.SetBool("isGoingLeft", false);
            playerAnimator.SetBool("isWalkingBack", false);
            speed = 5f;
            playerAnimator.SetFloat("TurnDirection", 0);
            playerAnimator.SetBool("isRunning", false);
            playerAnimator.SetBool("isWalking", false);
           
        }
         if (moveHorizontal != 0)
        {
            playerAnimator.SetBool("hasStopped", false);
            playerAnimator.SetBool("isWalkingBack", false);
            playerAnimator.SetFloat("TurnDirection", moveHorizontal);
            playerAnimator.SetBool("isRunning", false);
            speed = 5f;
            if (moveHorizontal>0)
           {
                playerAnimator.SetBool("isGoingRight", true);
                playerAnimator.SetBool("isGoingLeft", false);
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isRunning", false);
            }
            if(moveHorizontal<0)
            {
                playerAnimator.SetBool("isGoingLeft", true);
                playerAnimator.SetBool("isGoingRight", false);
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isRunning", false);
            }
     
          
        }
         if (moveVertical != 0)
        {
            playerAnimator.SetFloat("TurnDirection", 0);
            playerAnimator.SetBool("hasStopped", false);
            playerAnimator.SetBool("isGoingRight", false);
            playerAnimator.SetBool("isGoingLeft", false);
            playerAnimator.SetBool("isRunning", false);

            if(moveVertical>0)
            {
                playerAnimator.SetBool("isWalkingBack", false);
                
                playerAnimator.SetBool("isWalking", true);
            }
            if(moveVertical<0)
            {
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isWalkingBack", true);

            }

        }

        if (speed > 5.8f && moveVertical>0)
        {
            playerAnimator.SetBool("hasStopped", false);
            playerAnimator.SetBool("isRunning", true);
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isWalkingBack", false);
        }

        
    }
}
