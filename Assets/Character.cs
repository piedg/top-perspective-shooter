using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float originalStepOffset;
    public float speed;
    
    Animator animator;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

     

        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Roll());
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;

        controller.Move(velocity * speed * Time.deltaTime);
    }

    IEnumerator Roll()
    {
        animator.SetBool("isRolling", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isRolling", false);

    }
}
