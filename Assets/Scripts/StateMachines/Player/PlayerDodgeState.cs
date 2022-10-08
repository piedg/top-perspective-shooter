using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DodgeHash = Animator.StringToHash("Dodge");
    private const float CrossFadeDuration = 0.1f;

    private float remainingDodgeTime;
    private Vector3 dodgeDirection;

    public PlayerDodgeState(PlayerStateMachine stateMachine, Vector3 dodgeDirection) : base(stateMachine)
    {
        this.dodgeDirection = dodgeDirection;
    }
    public override void Enter()
    {
        stateMachine.Health.SetInvulnerable(true);
        remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.Animator.CrossFadeInFixedTime(DodgeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            stateMachine.CooldownManager.BeginCooldown(stateMachine.DodgeCooldown.ToString(), stateMachine.DodgeCooldown);
            stateMachine.Health.SetInvulnerable(false);
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        if (dodgeDirection == Vector3.zero)
        {
            Move(stateMachine.transform.forward * stateMachine.DodgeForce, deltaTime);
            return;
        }
   
        Move(dodgeDirection.normalized * stateMachine.DodgeForce, deltaTime);

        FaceMovementDirection(dodgeDirection, deltaTime);
    }


    public override void Exit() { }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.DefaultRotationSpeed);
    }
}
