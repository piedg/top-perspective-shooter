using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public float rollSpeed;
    bool isRolling;

    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    public GameObject CharacterModel;

    Animator animator;
    CharacterController controller;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
      
        move = vertical * Vector3.forward + horizontal * Vector3.right;

        if(Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            StartCoroutine(Roll());
        }


        Move(move);
        FollowMousePosition();
    }

    void Move(Vector3 move)
    {
        if(move.magnitude > 1)
        {
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
        
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);

        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Forward", forwardAmount);
        animator.SetFloat("Turn", turnAmount);
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;

        if(isRolling)
        {
            if(Mathf.Approximately(move.x, 0) && Mathf.Approximately(move.z, 0))
            {
                controller.Move(velocity * speed * Time.deltaTime);
            }
            else
            {
                controller.Move(move * rollSpeed * Time.deltaTime);
            }
        }
        else
        {
            controller.Move(velocity * speed * Time.deltaTime);
        }

        Debug.Log("MOVE " + move + " VELOCITY" + velocity);
    }

    IEnumerator Roll()
    {
        isRolling = true;
        animator.SetBool("isRolling", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isRolling", false);
        isRolling = false;
    }

    void FollowMousePosition()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }
}
