using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSystem : BaseSystem
{

    static EnemyMoveSystem Instance;
    public static EnemyMoveSystem GetInstance()
    {
        if (Instance == null) Instance = new EnemyMoveSystem();
        return Instance;
    }


    public override void Update()
    {
        List<EnemyMovementComponent> movelist = EnemyMovementComponent.GetMovementComponents();
        for (int i = 0; i < movelist.Count; i++)
        {
            //random chase target for now
            if (movelist[i].target==null)
            {
                foreach (var item in GlobalData.playerList)
                {
                    if (Random.value<0.5)
                    {
                        movelist[i].target = item.Value;
                        movelist[i].State = MovementState.Move;
                        break;
                    }
                }
            }

            switch (movelist[i].State)
            {
                case MovementState.Move:
                    Rigidbody rigi= movelist[i].GetComponent<Rigidbody>();
                    Vector3 forwar = (movelist[i].target.transform.position - movelist[i].transform.position).normalized;
                    rigi.velocity = forwar*5;
                    break;
                default:
                    break;
            }
        }
    
    }
    
}
