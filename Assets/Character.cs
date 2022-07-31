using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public float rollSpeed;
    public float rollRate = 0.8f;
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

        if (isRolling) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
      
        move = vertical * Vector3.forward + horizontal * Vector3.right;

        if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
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

        animator.SetBool("isRolling", isRolling);
    }

    private void OnAnimatorMove()
    {

        Vector3 velocity = animator.deltaPosition;

        Vector3 direction = move;

        if (isRolling)
        {
            controller.Move(direction.normalized * velocity.magnitude * rollSpeed * Time.deltaTime);
            CharacterModel.transform.rotation = Quaternion.Lerp(CharacterModel.transform.rotation, Quaternion.LookRotation(direction), 5f * Time.deltaTime);
        }
        else
        {
            controller.Move(velocity * speed * Time.deltaTime);
        }
    }

    IEnumerator Roll()
    {
        isRolling = true;
        yield return new WaitForSeconds(rollRate);
        isRolling = false;
    }

    void FollowMousePosition()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
            if(!isRolling)
            {
                CharacterModel.transform.LookAt(Vector3.Lerp(CharacterModel.transform.position, new Vector3(_hit.point.x, transform.position.y, _hit.point.z), 0.5f * Time.deltaTime));
            }
        }
    }
}
