using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static Canvas canvas;

    public void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
}