using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : BaseSystem
{
    public float TimeCounter=5;

    static EnemySpawnSystem Instance;
    public static EnemySpawnSystem GetInstance()
    {
        if (Instance == null) Instance = new EnemySpawnSystem();
        return Instance;
    }


    public override void Update()
    {
        if (!GlobalData.isJoinRoom)
        {
            return;
        }
        TimeCounter += Time.deltaTime;

        //Generate Enemy per 4 sec
        if (TimeCounter > 4)
        {
            TimeCounter = 0;
            List<EnemyStateComponent> EnemyList = EnemyStateComponent.GetMovementComponents();
            //Generate Enemy
            if (EnemyList.Count < 5)
            {
                SummonEnemy();
            }
        }
    
    }


    void SummonEnemy()
    {
        //if (!PhotonNetwork.connectionState)
        //    return;
        PhotonNetwork.Instantiate("Prefabs/Enemy", new Vector3(Random.Range(-40.0f,40.0f), 0f, Random.Range(-40.0f, 40.0f)), Quaternion.identity, 0);
    }
    
}
