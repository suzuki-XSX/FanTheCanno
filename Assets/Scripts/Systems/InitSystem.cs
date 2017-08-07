using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSystem : MonoBehaviour {

    GameObject singletonContainer;

	void Awake () {
        InputSystem.GetInstance();
        PlayerMovementSystem.GetInstance();
        SkillSystem.GetInstance();
        EnemySpawnSystem.GetInstance();
        EnemyMoveSystem.GetInstance();

        singletonContainer = transform.GetChild(0).gameObject;
        singletonContainer.AddComponent<InputSingleton>();
	}
	
	void Update () {
        InputSystem.GetInstance().Update();
        PlayerMovementSystem.GetInstance().Update();
        SkillSystem.GetInstance().Update();
        EnemySpawnSystem.GetInstance().Update();
        EnemyMoveSystem.GetInstance().Update();
    }
}
