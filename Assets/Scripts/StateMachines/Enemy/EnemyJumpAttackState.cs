using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyJumpAttackState : EnemyBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float TransitionDuration = 0.1f;

    public EnemyJumpAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        FaceToPlayer();

        stateMachine.AttackPoint.SetAttack(stateMachine.JumpAttackDamage, stateMachine.JumpAttackRange, true);
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        MoveForward(deltaTime);
        stateMachine.JumpAreaDisplay.SetActive(true);

        if (IsPlayingAnimation(stateMachine.Animator)) { return; }
        stateMachine.JumpAreaDisplay.SetActive(false);

        stateMachine.SwitchState(new EnemyJumpImpactState(stateMachine));
    }

    public override void Exit() { }
}
