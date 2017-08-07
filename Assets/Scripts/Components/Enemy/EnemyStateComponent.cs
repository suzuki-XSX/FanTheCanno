using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyStateComponent : MonoBehaviour {


    public int HP=5;
    public int attack;
    //Static stuff
    static List<EnemyStateComponent> StateComponents;
    static EnemyStateComponent myMovement;

    public static List<EnemyStateComponent> GetMovementComponents()
    {
        if (StateComponents == null) StateComponents = new List<EnemyStateComponent>();
        return StateComponents;
    }

    //Must have: add self to static, and remove on disable
    public  void Awake() {
        RegisterToList();
    }
	
    void RegisterToList()
    {
        GetMovementComponents().Add(this);
    }

    private void OnDisable()
    {
        GetMovementComponents().Remove(this);
    }

}
