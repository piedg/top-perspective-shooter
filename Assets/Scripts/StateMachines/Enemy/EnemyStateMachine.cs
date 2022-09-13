using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField, Header("Main Components")]
    public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float SuperAttackTimer { get; private set; }
    [field: SerializeField] public Damage AttackPoint { get; private set; }
    [field: SerializeField] public int SuperAttackDamage { get; private set; }
    [field: SerializeField] public int SuperAttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }

    public GameObject Player { get; private set; }

    public static float SuperAttackCooldown;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SuperAttackCooldown = SuperAttackTimer;

        SwitchState(new EnemyIdleState(this));
    }

    public void ResetSuperAttackTimer()
    {
        SuperAttackCooldown = SuperAttackTimer;
    }

    public void Attack()
    {
        AttackPoint.gameObject.SetActive(true);
    }

    public void FinishAttack()
    {
        AttackPoint.gameObject.SetActive(false);
    }
}
