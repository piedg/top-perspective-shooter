using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookForwardHash = Animator.StringToHash("Forward");
    private readonly int FreeLookRightHash = Animator.StringToHash("Right");

    private const float AnimatorDumpTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    // TEMP
    Vector3 direction;

    float forwardAmount;
    float turnAmount;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(FreeLookForwardHash, forwardAmount);
        stateMachine.Animator.SetFloat(FreeLookRightHash, turnAmount);

        direction = (stateMachine.InputManager.MovementValue.y * Vector3.forward) + (stateMachine.InputManager.MovementValue.x * Vector3.right).normalized;

        Move(direction * stateMachine.DefaultMovementSpeed, deltaTime);

        ConvertDirection(direction);

        FaceToMouse();
    }

    public override void Exit() { }

    void ConvertDirection(Vector3 direction)
    {
        if (direction.magnitude > 1)
        { 
            direction.Normalize();
        }

        Vector3 convertedDirection = direction;

        Vector3 localMove = stateMachine.transform.InverseTransformDirection(convertedDirection);

        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    /*private void FaceToMouse()
    {
       
    } */
}
