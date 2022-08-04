using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField, Header("Main Components")]
    public CharacterController Controller { get; private set; }
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField, Header("Movement Settings")]
    public float DefaultMovementSpeed { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
}
