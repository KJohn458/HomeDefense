using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float rotationRate = 150;

    public float moveSpeed = .6f;

    private bool dontspendmywood;

    Animator Character;

    private Health healthScript;
    private GameObject HouseGameObj;

    public Canvas repairCanvas;

    private void Start()
    {
        Character = GetComponent<Animator>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        healthScript = HouseGameObj.GetComponent<Health>();
        repairCanvas.gameObject.SetActive(false);
        dontspendmywood = false;
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

        if(Input.GetKeyUp(KeyCode.F))
        {
            dontspendmywood = false;
        }
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed * Time.deltaTime);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "House")
        {
            repairCanvas.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && !dontspendmywood)
            {
                dontspendmywood = true;
                if (healthScript.wood >= 2)
                {
                    healthScript.spendWood(2);
                    healthScript.Heal(1);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        repairCanvas.gameObject.SetActive(false);
    }
}
