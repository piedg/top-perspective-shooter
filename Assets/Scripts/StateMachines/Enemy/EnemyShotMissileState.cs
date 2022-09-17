using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotMissileState : EnemyBaseState
{
    private readonly int FlexingHash = Animator.StringToHash("Flexing");

    private const float CrossFadeDuration = 0.1f;

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector = new Vector3(0.25f, 0f ,0f);
    [SerializeField][Range(0, 1)] float movementFactor = 1f;
    [SerializeField] float period = 0.1f;
    bool isParticleSpawned = false;
    bool isMissilesSpawned = false;

    public EnemyShotMissileState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Sono dentro Shot Missile");
        startingPosition = stateMachine.transform.position;
        stateMachine.Animator.Play(FlexingHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f && stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.7f) { 

            Flicking();
            if (!isParticleSpawned) SpawnMissileFX() ;

            return; 
        }

        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < .7f) { return; }

        if (!isMissilesSpawned) SpawnDamageArea();

        if(stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) { return; }

        Debug.Log(stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        stateMachine.SwitchState(new EnemyChasingState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.CooldownManager.BeginCooldown(stateMachine.MissileAttackCooldown.ToString(), stateMachine.MissileAttackCooldown);
    }

    void SpawnMissileFX()
    {
        GameObject.Instantiate(stateMachine.MissileFX, stateMachine.MissileSpawnPoint.position, Quaternion.identity);
        isParticleSpawned = true;
    }

    void SpawnDamageArea()
    {
        for (int i = 0; i < Random.Range(3f, 9f); i++)
        {
            float offsetX = Random.Range(-5f - i, 5f + i);
            float offsetZ = Random.Range(-5f - i, 5f + i);

           GameObject area = GameObject.Instantiate(stateMachine.MissileArea, new Vector3(stateMachine.Player.transform.position.x + offsetX, 1f, stateMachine.Player.transform.position.z + offsetZ), Quaternion.identity);
           GameObject.Destroy(area, 5f);
        }
        isMissilesSpawned = true;
    }

    void Flicking()
    {
        float cycles = Time.time / period;  // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;   // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        stateMachine.transform.position = startingPosition + offset;
    }

   
}
