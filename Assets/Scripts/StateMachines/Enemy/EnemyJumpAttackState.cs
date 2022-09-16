using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpAttackState : EnemyBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float TransitionDuration = 0.1f;
    float deltaTimeBeforeJump;

    public EnemyJumpAttackState(EnemyStateMachine stateMachine, float deltaTime) : base(stateMachine) {
        deltaTimeBeforeJump = deltaTime;
    }

    public override void Enter()
    {
        FaceToPlayer(deltaTimeBeforeJump);
        stateMachine.AttackPoint.SetAttack(stateMachine.SuperAttackDamage, stateMachine.SuperAttackRange, true);
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
         MoveForward(deltaTime);

        if (IsPlayingAnimation(stateMachine.Animator)) { return; }
        
        stateMachine.SwitchState(new EnemyJumpImpactState(stateMachine));
    }

    public override void Exit() { }
}
