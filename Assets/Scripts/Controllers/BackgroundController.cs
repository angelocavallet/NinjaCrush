using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalMaxOffset;
    [SerializeField] private float verticalSpeed; 
    [SerializeField] private float verticalMaxOffset; 
    private Material material;
    private Vector2 textureOffset;

    void Awake() {
        material = GetComponent<MeshRenderer>().materials[0];
        textureOffset = material.mainTextureOffset;
    }

    void Update()
    {
        if (Mathf.Abs(textureOffset.x) > horizontalMaxOffset) horizontalSpeed *= -1;
        if (Mathf.Abs(textureOffset.y) > verticalMaxOffset) verticalSpeed *= -1;

        textureOffset.y += verticalSpeed * Time.deltaTime;
        textureOffset.x += horizontalSpeed * Time.deltaTime;
        material.mainTextureOffset = textureOffset;
    }
}
