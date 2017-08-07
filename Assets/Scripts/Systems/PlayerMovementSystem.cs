using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSystem : BaseSystem
{
    static PlayerMovementSystem Instance;
    public static PlayerMovementSystem GetInstance()
    {
        if (Instance == null) Instance = new PlayerMovementSystem();
        return Instance;
    }
	
	public override void Update () {
        HandleLocalMovement();
	}

    void HandleLocalMovement()
    {
        PlayerMovementComponent move = PlayerMovementComponent.GetLocalMovementComponent();
        if (move && move.isMine)
        {
            HandleMovementInput(move);
            MoveObjectByVelocity(move);
        }
    }

    void HandleMovementInput(PlayerMovementComponent move)
    {
        InputSingleton input = InputSingleton.GetLocalInputComponent();
        if(input != null)
        {
            switch (move.State)
            {
                case MovementState.Idle:
                    if (input.IsInputActivated())
                    {
                        if (input.Jump)
                        {
                            ChangeXZVelocityByInput(move, input.InputAxis);
                            move.EnterState(MovementState.Jump);
                        }
                        else if (input.InputAxis != Vector3.zero)
                        {
                            ChangeXZVelocityByInput(move, input.InputAxis);
                            move.EnterState(MovementState.Move);
                        }
                    }
                    break;
                case MovementState.Move:
                    if (input.IsInputActivated())
                    {
                        if (input.Jump)
                        {
                            ChangeXZVelocityByInput(move, input.InputAxis);
                            move.EnterState(MovementState.Jump);
                        }
                        else
                        {
                            ChangeXZVelocityByInput(move, input.InputAxis);
                        }
                    }
                    else
                    {
                        move.EnterState(MovementState.Idle);
                    }
                    
                    break;
                case MovementState.Jump:
                    ChangeXZVelocityByInput(move, input.InputAxis);
                    if (move.cc.isGrounded && move.velocity.y < 0)
                    {
                        move.EnterState(MovementState.Idle);
                    }
                    break;
                default:
                    Debug.LogError("HandleMovementInput: Uncaught movement system state");
                    break;
            }
        }
    }

    void MoveObjectByVelocity(PlayerMovementComponent move)
    {
        Vector3 velocityClamp = move.velocity;
        velocityClamp.y = 0;
        if(velocityClamp.magnitude > move.speedOnGround)
        {
            velocityClamp = Vector3.ClampMagnitude(velocityClamp, move.speedOnGround);
        }

        move.velocity.x = velocityClamp.x;
        move.velocity.z = velocityClamp.z;

        move.velocity.y -= move.gravity * Time.deltaTime;
        move.cc.Move(move.velocity * Time.deltaTime);
        move.velocity = move.cc.velocity;
    }

    void ChangeXZVelocityByInput(PlayerMovementComponent move, Vector3 input)
    {
        switch (move.State)
        {
            case MovementState.Jump:
                AddVectorXZ(ref move.velocity, input * move.accelerationInAir * Time.deltaTime);
                break;
            case MovementState.Move:
                ChangeVectorXZ(ref move.velocity, input * move.speedOnGround);
                break;
            default:
                ChangeVectorXZ(ref move.velocity, Vector3.zero);
                break; ;
        }
    }

    private void AddVectorXZ(ref Vector3 to, Vector3 from)
    {
        to.x += from.x;
        to.z += from.z;
    }

    private void ChangeVectorXZ(ref Vector3 to, Vector3 from)
    {
        to.x = from.x;
        to.z = from.z;
    }

}


