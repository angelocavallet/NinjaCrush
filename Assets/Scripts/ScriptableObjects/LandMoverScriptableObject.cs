using UnityEngine;

[CreateAssetMenu(fileName = "new LandMoverData", menuName = "ScriptableObjects/Movers/LandMoverData")]
public class LandMoverScriptableObject : ScriptableObject
{
    [Header("Physics Settings")]
    public float maxHealth;
    public float speed;
    public float maxSpeed;
    public float jumpForce;
    public float recoverColdown;
    public bool hasDoubleJump;
    public bool hasWallJump;

    [Header("Collision Tags")]
    public string TAG_GROUND = "Ground";

    [Header("Animation Labels")]
    public string ANIM_MOVING = "Moving";
    public string ANIM_JUMPING = "Jumping";
    public string ANIM_FALLING = "Falling";
    public string ANIM_GROUNDED = "Grounded";
    public string ANIM_HURT = "Hurt";
}
