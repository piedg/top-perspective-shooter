using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyJumpAttackState : EnemyBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private const float TransitionDuration = 0.1f;
    private float attackRadius;

    public EnemyJumpAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        attackRadius = stateMachine.AttackPoint.GetComponent<SphereCollider>().radius;
        FaceToPlayer();

        stateMachine.AttackPoint.SetAttack(stateMachine.JumpAttackDamage, stateMachine.JumpAttackRange, true);
        RectTransform rt = stateMachine.JumpAreaDisplay.GetComponentInChildren<Image>().rectTransform;
        rt.sizeDelta = new Vector2(stateMachine.JumpAttackRange * stateMachine.JumpAttackRange, stateMachine.JumpAttackRange * stateMachine.JumpAttackRange);

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
