using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField, Header("Main Components")]
    public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public CooldownManager CooldownManager { get; private set; }
    [field: SerializeField, Header("Movement Settings")] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField, Header("Attack Settings")] public Damage AttackPoint { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField, Header("Jump Attack Settings")] public float JumpAttackRange { get; private set; }
    [field: SerializeField] public int JumpAttackDamage { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float JumpAttackCooldown { get; private set; }
    [field: SerializeField] public GameObject JumpAreaDisplay { get; private set; }

    [field: SerializeField, Header("Missile Attack Settings")] public float MissileAttackCooldown { get; private set; }
    [field: SerializeField] public Transform MissileSpawnPoint { get; private set; }
    [field: SerializeField] public GameObject MissileFX { get; private set; }
    [field: SerializeField] public GameObject MissileArea { get; private set; }
    [field: SerializeField] public float MissileSpawnArea { get; private set; }
    [field: SerializeField] public int MaxMissileToSpawn { get; private set; }




    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SwitchState(new EnemyIdleState(this));
    }

    public void Attack()
    {
        AttackPoint.gameObject.SetActive(true);
    }

    public void FinishAttack()
    {
        AttackPoint.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        //Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleDie()
    {
        // TODO: switch to Die State
        SwitchState(new EnemyDeathState(this));
    }
}
