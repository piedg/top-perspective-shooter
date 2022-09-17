using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
   
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement * stateMachine.MovementSpeed) * deltaTime);
    }

    protected void MoveForward(float deltaTime)
    {
        stateMachine.Controller.Move(stateMachine.transform.forward * stateMachine.JumpForce * deltaTime);
    }

    protected void FaceToPlayer()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
    protected void RotateToPlayer(float deltaTime)
    {
        if (stateMachine.Player == null) { return; }

        var targetRotation = Quaternion.LookRotation(stateMachine.Player.transform.position - stateMachine.transform.position);

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.RotationSpeed * deltaTime);
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.Player.GetComponent<Health>().IsDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }

    protected bool HasJumpAttack()
    {
        return stateMachine.CooldownManager.CooldownTimeRemaining(stateMachine.JumpAttackCooldown.ToString()) <= 0;
    }

    protected bool HasMissileAttack()
    {
        return stateMachine.CooldownManager.CooldownTimeRemaining(stateMachine.MissileAttackCooldown.ToString()) <= 0;
    }

}
