using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int TurnLocomotionHash = Animator.StringToHash("TurnLocomotion");
    private readonly int TurnHash = Animator.StringToHash("Turn");
    private readonly int FreeLookForwardHash = Animator.StringToHash("Forward");
    private readonly int FreeLookRightHash = Animator.StringToHash("Right");

    private const float CrossFadeDuration = 0.1f;
    private float nextFire;

    Vector3 direction;

    float forwardAmount;
    float turnAmount;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputManager.DodgeEvent += OnDodge;

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        direction = (stateMachine.InputManager.MovementValue.y * Vector3.forward) + (stateMachine.InputManager.MovementValue.x * Vector3.right);
        direction.Normalize();

        Move(direction * stateMachine.DefaultMovementSpeed, deltaTime);

        ConvertDirection(direction);

        FaceToMouse();

        OnShoot();

        stateMachine.Animator.SetFloat(FreeLookForwardHash, forwardAmount);
        stateMachine.Animator.SetFloat(FreeLookRightHash, turnAmount);

        Debug.Log("Forward Amount" + forwardAmount);
    }

    public override void Exit() {
        stateMachine.InputManager.DodgeEvent -= OnDodge;
    }

    void ConvertDirection(Vector3 direction)
    {
        if (direction.magnitude > 1)
        { 
            direction.Normalize();
        }

        Vector3 convertedDirection = direction;

        Vector3 localMove = stateMachine.transform.InverseTransformDirection(convertedDirection);

        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    private void FaceToMouse()
    {
        // Handle player rotation to mouse position
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(stateMachine.InputManager.MouseValue);

        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        {
            Vector3 point = new Vector3(_hit.point.x, stateMachine.transform.position.y, _hit.point.z);
            stateMachine.transform.LookAt(point);
        }
    }

    private void OnDodge()
    {
        stateMachine.SwitchState(new PlayerDodgeState(stateMachine, direction));
    }

    private void OnShoot()
    {
        // stateMachine.SwitchState(new PlayerShootingState(stateMachine));

        if(stateMachine.InputManager.IsShooting && Time.fixedTime > nextFire)
        {
            nextFire = Time.fixedTime + stateMachine.FireRate;

            GameObject bullet = stateMachine.ProjectilePool.GetObjectFromPool();

            //Set missile 
            bullet.transform.SetPositionAndRotation(stateMachine.FirePoint.transform.position, stateMachine.FirePoint.transform.rotation);

            //Active from Pool
            bullet.SetActive(true);
        }
    }
}
