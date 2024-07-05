using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    private InputAction moveInput;
    private InputAction jumpInput;
    private InputAction aimInput;
    private InputAction throwInput;

    //@todo create InputManager
    public static PlayerInput instance
    {
        get => _instance;
    }

    public PlayerInput(PlayerInputScriptableObject playerInputData)
    {
        moveInput = playerInputData.moveInput;
        jumpInput = playerInputData.jumpInput;
        aimInput = playerInputData.aimInput;
        throwInput = playerInputData.throwInput;
        _instance = this;
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