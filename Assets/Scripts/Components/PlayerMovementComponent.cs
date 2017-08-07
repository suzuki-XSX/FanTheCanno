using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PhotonView), typeof(CharacterController))]
public class PlayerMovementComponent : MonoBehaviour {
    //Static stuff
    static List<PlayerMovementComponent> movementComponents;
    static PlayerMovementComponent myMovement;
    
    public static List<PlayerMovementComponent> GetMovementComponents()
    {
        if (movementComponents == null) movementComponents = new List<PlayerMovementComponent>();
        return movementComponents;
    }

    public static PlayerMovementComponent GetLocalMovementComponent()
    {
        return myMovement;
    }

    //State
    public MovementState State;

    //Editor Variables
    public float speedOnGround;
    public float speedInAir;
    public float accelerationInAir;
    public float jumpSpeed;
    public float gravity;

    //Logic Variables
    [HideInInspector]
    public PhotonView pv;
    [HideInInspector]
    public CharacterController cc;
    [HideInInspector]
    public bool isMine = false;
    [HideInInspector]
    public Vector3 velocity;

    //Must have: add self to static, and remove on disable
    void Awake () {
        RegisterToList();
 
        pv = GetComponent<PhotonView>();
        cc = GetComponent<CharacterController>();
        if (pv.isMine)
        {
            isMine = true;
            myMovement = this;
        }
        velocity = Vector3.zero;
        State = MovementState.Idle;
    }
	
    void RegisterToList()
    {
        GetMovementComponents().Add(this);
    }

    private void OnDisable()
    {
        GetMovementComponents().Remove(this);
    }

    //Real functions starts here
    public void EnterState(MovementState newState)
    {
        State = newState;
        switch(newState)
        {
            case MovementState.Jump:
                velocity.y = jumpSpeed;
                break;
            default:
                break;
        }
    }
}
