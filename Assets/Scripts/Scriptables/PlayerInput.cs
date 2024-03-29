using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new PlayerInput", menuName = "ScriptableObjects/PlayerInput")]
public class PlayerInput: ScriptableObject
{
    [Header("Inputs")]
    [SerializeField] private InputAction moveInput;
    [SerializeField] private InputAction jumpInput;
    [SerializeField] private InputAction aimInput;
    [SerializeField] private InputAction throwInput;

    //@todo create InputManager
    public static PlayerInput instance
    {
        get => _instance;
        //private 
            set => _instance = value;
    }

    private static PlayerInput _instance;

    public void EnableInputs()
    {
        moveInput.Enable();
        jumpInput.Enable();
        aimInput.Enable();
        throwInput.Enable();
    }

    public void DisableInputs()
    {
        moveInput.Disable();
        jumpInput.Disable();
        aimInput.Disable();
        throwInput.Disable();
    }

    public Boolean isJumpPressed()
    {
        return jumpInput.triggered;
    }

    public Boolean isThrowPressed()
    {
        return throwInput.triggered;
    }

    public float GetMoveXDir()
    {
        return moveInput.ReadValue<float>();
    }

    public Vector2 GetAimDir()
    {
        return aimInput.ReadValue<Vector2>();
    }
}