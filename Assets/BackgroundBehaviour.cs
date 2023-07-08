using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    [SerializeField] private float hspeed = 0.0001f; 
    [SerializeField] private float vspeed = 0.0001f; 
    private PlayerMovement playerMovement;
    private Material material;

    void Awake() {
        playerMovement = FindFirstObjectByType(typeof(PlayerMovement)) as PlayerMovement;
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = playerMovement.GetVelocity();
        velocity.x *= hspeed;
        velocity.y *= vspeed;
        material.mainTextureOffset += velocity;

    }
}
