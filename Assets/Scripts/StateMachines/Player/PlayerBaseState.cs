using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        // Handle player no movement input
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 direction, float deltaTime)
    {
        // Handle player CharacterController Move method
        stateMachine.Controller.Move(direction * deltaTime);
    }
}
