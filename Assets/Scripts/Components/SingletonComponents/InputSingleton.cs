using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSingleton : MonoBehaviour
{
    static InputSingleton LocalInput;

    public static InputSingleton GetLocalInputComponent()
    {
        return LocalInput;
    }

    public Vector3 InputAxis;
    public bool Jump;
    public Vector3 MousePos;
    public HashSet<SkillEnum> Skills;

    void Awake()
    {
        LocalInput = this;
        InitData();
    }

    void InitData()
    {
        InputAxis = Vector3.zero;
        Jump = false;
        MousePos = Vector3.zero;
        Skills = new HashSet<SkillEnum>();
    }

    public bool IsInputActivated()
    {
        return (InputAxis != Vector3.zero || Jump);
    }

    public bool IsInputAxisActivated()
    {
        return InputAxis != Vector3.zero;
    }
}