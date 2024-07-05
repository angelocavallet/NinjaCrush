using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new PlayerInputData", menuName = "ScriptableObjects/GameManager/PlayerInputData")]
public class PlayerInputScriptableObject: ScriptableObject
{
    [Header("Inputs")]
    public InputAction moveInput;
    public InputAction jumpInput;
    public InputAction aimInput;
    public InputAction throwInput;
}