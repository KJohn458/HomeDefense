using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float rotationRate = 150;

    public float moveSpeed = .25f;

    Animator Character;

    private void Start()
    {
        Character = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);

        ApplyInput(moveAxis, turnAxis);

        if (Input.GetKey(KeyCode.W))
        {
            Character.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Character.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Character.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Character.SetBool("isWalking", true);
        }
        else
        {
            Character.SetBool("isWalking", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Character.Play("Attack");
            Character.SetBool("attackParticles", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Character.SetBool("attackParticles", false);
        }
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
