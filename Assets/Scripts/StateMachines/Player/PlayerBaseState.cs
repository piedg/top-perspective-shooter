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

    protected void FaceToMouse()
    {
        // Handle player rotation to mouse position

        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(stateMachine.InputManager.MouseValue);

        if (Physics.Raycast(_ray, out _hit))
        {
            stateMachine.transform.LookAt(new Vector3(_hit.point.x, stateMachine.transform.position.y, _hit.point.z));
            /* if (!isRolling)
             {
                 CharacterModel.transform.LookAt(Vector3.Lerp(CharacterModel.transform.position, new Vector3(_hit.point.x, transform.position.y, _hit.point.z), Time.deltaTime));
             }*/
        }
    }
}
