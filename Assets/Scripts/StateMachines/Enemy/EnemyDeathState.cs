using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    private readonly int DieHash = Animator.StringToHash("Die");
    private const float CrossFadeduration = 0.1f;
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DieHash, CrossFadeduration);
    }

    public override void Tick(float deltaTime) { }

    public override void Exit() { }
   
}
