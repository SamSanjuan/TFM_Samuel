using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    public CharacterController controller;
    public Animator anim;
    public AudioSource audioSource;

    [Header("Movimiento")]
    public float speed = 12f;
    public float rotationSpeed = 120f;
    public float gravity = -9.18f;

    [Header("Pasos")]
    public AudioClip[] footstepSounds;
    public float stepInterval = 0.5f;

    private float stepTimer;
    private int lastStepIndex = -1;

    Vector3 velocity;

    public gameManager gm;
    public EnemyIA eIA;

    void Update()
    {
        movementController(gm.canMove);
    }

    public void movementController(bool canMove)
    {
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (z < 0) z = 0;
            Vector3 move = transform.forward * z;
            Vector3 rotate = Vector3.up * x;

            controller.Move(move * speed * Time.deltaTime);
            transform.Rotate(rotate * rotationSpeed * Time.deltaTime);

            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", z);

            bool isWalking = (x != 0 || z != 0);

            if (isWalking)
            {
                stepTimer -= Time.deltaTime;

                if (stepTimer <= 0)
                {
                    PlayFootstep();
                    stepTimer = stepInterval;
                }
            }
            else
            {
                stepTimer = 0;
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length == 0) return;

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, footstepSounds.Length);
        }
        while (randomIndex == lastStepIndex);

        lastStepIndex = randomIndex;

        audioSource.PlayOneShot(footstepSounds[randomIndex]);
    }
}


