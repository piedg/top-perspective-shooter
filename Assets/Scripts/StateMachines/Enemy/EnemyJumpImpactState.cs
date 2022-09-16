using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpImpactState : EnemyBaseState
{
    private readonly int JumpImpactHash = Animator.StringToHash("JumpImpact");
    private const float TransitionDuration = 0.1f;

    public EnemyJumpImpactState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.AttackPoint.gameObject.SetActive(true);
        stateMachine.Animator.CrossFadeInFixedTime(JumpImpactHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.AttackPoint.gameObject.SetActive(false);

        Move(deltaTime);

        if (IsPlayingAnimation(stateMachine.Animator)) { return; }


        stateMachine.SwitchState(new EnemyChasingState(stateMachine));
    }

    public override void Exit() 
    {
        stateMachine.CooldownManager.BeginCooldown(stateMachine.JumpAttackCooldown.ToString(), stateMachine.JumpAttackCooldown);
    }
}
