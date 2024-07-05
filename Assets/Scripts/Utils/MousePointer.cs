using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private float speed = 5f;
    private float deadSpace = 0.5f;

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(mouseWorldPos, transform.position) > deadSpace)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, mouseWorldPos, step);
        }
    }
}
