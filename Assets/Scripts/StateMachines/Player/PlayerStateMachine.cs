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
    [field: SerializeField]
    public float DefaultRotationSpeed { get; private set; }

    [field: SerializeField, Header("Dodge Settings")] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeForce { get; private set; }

    [field: SerializeField, Header("Shooting Settings")]
    public Transform FirePoint { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public ObjectPool ProjectilePool { get; private set; }

    [field: SerializeField, Header("Others")] public GameObject CharacterModel { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
}
