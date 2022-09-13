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
        EnemyStateMachine.SuperAttackCooldown -= deltaTime;

        if (EnemyStateMachine.SuperAttackCooldown <= 0f)
        {
            stateMachine.SwitchState(new EnemySuperAttackState(stateMachine));
            return;
        }

        if (GetNormalizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

        FaceToPlayer();
    }

    public override void Exit() { }

   
}
