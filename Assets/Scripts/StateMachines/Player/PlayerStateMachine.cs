using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField, Header("Main Components")]
    public CharacterController Controller { get; private set; }
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField, Header("Movement Settings")]
    public float DefaultMovementSpeed { get; private set; }
    [field: SerializeField]
    public float DefaultRotationSpeed { get; private set; }

    [field: SerializeField, Header("Dodge Settings")] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeForce { get; private set; }

    [field: SerializeField, Header("Shooting Settings")]
    public Transform FirePoint { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public int WeaponDamage { get; private set; }
    [field: SerializeField] public ObjectPool ProjectilePool { get; private set; }
    [field: SerializeField, Header("Physic")] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField, Header("Others")] public GameObject CharacterModel { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }
    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    void HandleTakeDamage()
    {
        // TODO: Create a TakeDamageState
        //StartCoroutine(TakeDamage());   
    }

    IEnumerator TakeDamage()
    {
        Health.SetInvulnerable(true);
        yield return new WaitForSeconds(5f);
        Health.SetInvulnerable(false);
    }

    void HandleDie()
    {

        // TODO: Create a DeadState
        // TEMP SOLUTION
        Animator.enabled = false;
        this.enabled = false;
    }
}
