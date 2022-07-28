using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    bool isRolling;
    Animator animator;
    Vector3 moveInput;
    Rigidbody rb;

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

        StartCoroutine(HandleRoll());
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        if (isRolling) return;
        bool isMoving = rb.velocity.magnitude > 0;

        animator.SetBool("isMoving", isMoving);

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Move X", inputX);
        animator.SetFloat("Move Y", inputZ);

        moveInput.Set(inputX, rb.velocity.y, inputZ);
        moveInput.Normalize();

        rb.velocity = moveInput * speed;
    }

    IEnumerator HandleRoll()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("sono qui "+ isRolling );

            isRolling = true;
            rb.velocity = moveInput * speed;

            animator.SetTrigger("Roll");

            yield return new WaitForSeconds(1f);
            isRolling = false;
        }
        yield return new WaitForEndOfFrame();

    }
}
