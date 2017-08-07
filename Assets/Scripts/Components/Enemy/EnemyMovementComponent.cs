using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class EnemyMovementComponent : MonoBehaviour
{
    //Static stuff
    public GameObject target;
    static List<EnemyMovementComponent> movementComponents;
    static EnemyMovementComponent myMovement;
    
    public static List<EnemyMovementComponent> GetMovementComponents()
    {
        if (movementComponents == null) movementComponents = new List<EnemyMovementComponent>();
        return movementComponents;
    }

    public static EnemyMovementComponent GetLocalMovementComponent()
    {
        return myMovement;
    }

    //State
    public MovementState State;

    //Logic Variables
    [HideInInspector]
    public PhotonView pv;

    //Must have: add self to static, and remove on disable

    public void Awake()
    {
        RegisterToList();
 
        pv = GetComponent<PhotonView>();

    }
	
    public void RegisterToList()
    {
        GetMovementComponents().Add(this);
    }

    private void OnDisable()
    {
        GetMovementComponents().Remove(this);
    }

}
