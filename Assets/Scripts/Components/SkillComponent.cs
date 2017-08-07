using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillEnum
{
    Fireball,
    Tower
}

[RequireComponent(typeof(PhotonView))]
public class SkillComponent : MonoBehaviour {
    static SkillComponent mySkill;

    //Editor Variables
    //List is slow here, change to hashset later.
    public List<SkillEnum> skills;

    //Logic Variables
    [HideInInspector]
    public PhotonView pv;

    public static SkillComponent GetLocalMovementComponent()
    {
        return mySkill;
    }
    
    void Awake () {
        pv = GetComponent<PhotonView>();
        if (pv.isMine)
        {
            mySkill = this;
        }
    }
    
    public bool HasSkill(SkillEnum skillEnum)
    {
        return skills.Contains(skillEnum);
    }
    
    public void SummonTurretRPC()
    {
    }
}
