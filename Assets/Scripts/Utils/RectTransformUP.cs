using UnityEngine;

public class RectTransformUP : MonoBehaviour
{
    public float yPerFrame = 0.5f;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPostition = rectTransform.localPosition;
        newPostition.y += yPerFrame;

        rectTransform.localPosition = newPostition;
    }
}
