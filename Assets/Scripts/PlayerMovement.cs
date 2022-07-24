using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Animator animator;
    Vector3 moveInput;
    Rigidbody rb;
    [SerializeField] Transform playerSpine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0)
        {
            // Player is moving
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);

        }
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Move X", inputX);
        animator.SetFloat("Move Y", inputZ);

        moveInput.Set(inputX, rb.velocity.y, inputZ);
        moveInput.Normalize();
    

        rb.velocity = moveInput * speed;

    }
}
