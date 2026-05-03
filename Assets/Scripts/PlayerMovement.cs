using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    public CharacterController controller;
    public Animator anim;

    public float speed = 12f;
    public float rotationSpeed = 120f;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;
    public float runSpeed = 7;

    Vector3 velocity;

    private float x, y;
    //bool isGrounded;

    //public Transform groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask groundMask;

    public gameManager gm;

    //bool capCorEmpuje = true;

    // Update is called once per frame
    void Update()
    {
        //movementController(gm.canMove);

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed,0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
    }

    public void movementController(bool canMove)
    {
        if (canMove)
        {/*
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }*/

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //Vector3 move = transform.right * x + transform.forward * z;
            Vector3 move = transform.forward * z;
            Vector3 rotate = Vector3.up * x;

            controller.Move(move * speed * Time.deltaTime);
            transform.Rotate(rotate * rotationSpeed *Time.deltaTime);

            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", z);

            bool isWalking = (x != 0 || z != 0);
            //anim.SetBool("walk", isWalking);
            /*
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }*/

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}

