using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.00001f; 
    [SerializeField] private float verticalSpeed = 0.00001f; 
    [SerializeField] private float windSpeed = 0.00001f;
    private Material material;

    void Awake() {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    void Update()
    {
        material.mainTextureOffset += Vector2.left * windSpeed * Time.deltaTime;
    }
}
