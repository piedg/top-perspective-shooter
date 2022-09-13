using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookForwardHash = Animator.StringToHash("Forward");
    private readonly int FreeLookRightHash = Animator.StringToHash("Right");

    private const float CrossFadeDuration = 0.1f;
    private float nextFire;

    Vector3 direction;

    float forwardAmount;
    float rightAmount;
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

        OnShoot();

        Move(direction * stateMachine.DefaultMovementSpeed, deltaTime);

        ConvertDirection(direction);

        FaceToMouse();


        stateMachine.Animator.SetFloat(FreeLookForwardHash, forwardAmount);
        stateMachine.Animator.SetFloat(FreeLookRightHash, rightAmount);

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

        Vector3 localMove = stateMachine.transform.InverseTransformDirection(direction);

        rightAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    private void FaceToMouse()
    {

        // Handle player rotation to mouse position
        Ray ray = Camera.main.ScreenPointToRay(stateMachine.InputManager.MouseValue);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
          Plane virtualPlane = new Plane(Vector3.up, stateMachine.transform.position);
          if (virtualPlane.Raycast(ray, out float hitDist))
          {
              Vector3 hitPoint = ray.GetPoint(hitDist);
              stateMachine.transform.LookAt(hitPoint);
          }

        //For Gamepad
        /* Vector3 direction = new Vector3(stateMachine.InputManager.MouseValue.x, 0, stateMachine.InputManager.MouseValue.y); //if you're 2d side scroller, you need to swap 2nd and 3rd value.
         stateMachine.transform.rotation = Quaternion.LookRotation(direction); */

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
