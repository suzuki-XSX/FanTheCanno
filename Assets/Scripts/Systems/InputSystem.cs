using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : BaseSystem
{
    static InputSystem Instance;

    public static InputSystem GetInstance()
    {
        if (Instance == null) Instance = new InputSystem();
        return Instance;
    }

    InputSingleton input;

    public override void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (input == null)
        {
            input = InputSingleton.GetLocalInputComponent();
        }
        if (input != null)
        {
            Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            inputAxis = Vector3.ClampMagnitude(inputAxis, 1.0f);
            input.InputAxis = inputAxis;
            input.Jump = Input.GetKeyDown(KeyCode.Space);
            input.MousePos = Input.mousePosition;

            input.Skills.Clear();
            GetSkillInput(SkillEnum.Tower, KeyCode.F);
        }
    }

    void GetSkillInput(SkillEnum skillEnum, KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {
            input.Skills.Add(skillEnum);
        }
    }
}
