using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : BaseSystem
{
    static SkillSystem Instance;
    public static SkillSystem GetInstance()
    {
        if (Instance == null) Instance = new SkillSystem();
        return Instance;
    }

    InputSingleton input;
    SkillComponent localSkillComponent;

    public override void Update()
    {
        if (input == null)
        {
            input = InputSingleton.GetLocalInputComponent();
        }
        if (input != null)
        {
            HandleLocalSkills();
        }
    }

    void HandleLocalSkills()
    {
        if(localSkillComponent == null)
        {
            localSkillComponent = SkillComponent.GetLocalMovementComponent();
        }
        if(localSkillComponent != null)
        {
            foreach (SkillEnum skillEnum in input.Skills)
            {
                if (!localSkillComponent.HasSkill(skillEnum)) continue;
                switch (skillEnum)
                {
                    case SkillEnum.Tower:
                        Debug.Log("Summon turret!");
                        SummonTurret();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void SummonTurret()
    {
        PhotonNetwork.Instantiate("Prefabs/Turret", localSkillComponent.gameObject.transform.position + new Vector3(3.0f, 0f, 0f), Quaternion.identity, 0);
    }
    
}
