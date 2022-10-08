using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotMissileState : EnemyBaseState
{
    private readonly int FlexingHash = Animator.StringToHash("Flexing");

    Vector3 startingPosition;
    Vector3 movementVector = new Vector3(0.25f, 0f ,0f);
    float movementFactor = 1f;
    float period = 0.1f;

    bool isParticleSpawned = false;
    bool areMissilesSpawned = false;


    public EnemyShotMissileState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
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

        if (!areMissilesSpawned) SpawnMissileArea();

        if(IsPlayingAnimation(stateMachine.Animator)) { return; }

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

    void SpawnMissileArea()
    {
        for (int i = 0; i < stateMachine.MaxMissileToSpawn; i++)
        {
            GameObject missileArea = GameObject.Instantiate(stateMachine.MissileArea);

            missileArea.transform.SetPositionAndRotation(RandomSpawnPos(stateMachine.Player.transform.position, stateMachine.MissileSpawnArea), Quaternion.identity);
        }

        areMissilesSpawned = true;
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

    Vector3 RandomSpawnPos(Vector3 center, float range)
    {
        List<Vector3> positions = new List<Vector3>();

        float randomX;
        float y = center.y + 0.15f;
        float randomZ;

        do
        {
            randomX = Random.Range(center.x - range, center.x + range);
            randomZ = Random.Range(center.z - range, center.z + range);
            positions.Add(new Vector3(randomX, y, randomZ));
        }
        while (!positions.Contains(new Vector3(randomX, y, randomZ)));
        
        Vector3 randPos = new Vector3((int)randomX, y, (int)randomZ);
        return randPos;
    }
}
