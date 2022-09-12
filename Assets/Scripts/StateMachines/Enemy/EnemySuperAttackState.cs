using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperAttackState : EnemyBaseState
{
    private readonly int SuperAttackHash = Animator.StringToHash("SuperAttack");
    private const float TransitionDuration = 0.1f;

    Vector3 playerLastPosition;

    public EnemySuperAttackState(EnemyStateMachine stateMachine) : base(stateMachine) {
    }

    public override void Enter()
    {
        stateMachine.ResetSuperAttackTimer();
        playerLastPosition = stateMachine.Player.transform.position;
        stateMachine.Animator.CrossFadeInFixedTime(SuperAttackHash, TransitionDuration);

    }

    public override void Tick(float deltaTime)
    {
        MoveTo(playerLastPosition);

        if (GetNormalizedTime(stateMachine.Animator, "SuperAttack") >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit() { }
   
}
