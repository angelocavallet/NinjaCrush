using Unity.VisualScripting;
using UnityEngine;

public class BackgroundCamera : MonoBehaviour
{
    [SerializeField] private float parallaxRelativeToMoveSpeed;
    private Vector3 lastPositionParent;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock block;
    private float speed = 0f;

    private const string MOVE_DIRECTION = "_MoveDirection";
    private const string OFFSET = "_Offset";

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();
        lastPositionParent = transform.parent.position;
    }

    void Update()
    {
        //float cameraVelocity = Vector3.Distance(lastPositionParent, transform.parent.position) / Time.deltaTime;
        Vector3 cameraDistance = (lastPositionParent - transform.parent.position);

        //cameraDistance *= Time.deltaTime;
        if (cameraDistance != Vector3.zero)
        {
            spriteRenderer.GetPropertyBlock(block);
            //@todo: update with offset
            //block.SetVector(MOVE_DIRECTION, cameraDistance);
            spriteRenderer.SetPropertyBlock(block);
            lastPositionParent = transform.parent.position;
        }
    }
}