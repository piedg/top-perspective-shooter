using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private const float TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.AttackPoint.SetAttack(stateMachine.AttackDamage);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (IsPlayingAnimation(stateMachine.Animator)) { return; }

        if (HasJumpAttack())
        {
            stateMachine.SwitchState(new EnemyJumpAttackState(stateMachine, deltaTime));
            return;
        }

        stateMachine.SwitchState(new EnemyChasingState(stateMachine));

        FaceToPlayer(deltaTime);
    }

    public override void Exit() { }

   
}
