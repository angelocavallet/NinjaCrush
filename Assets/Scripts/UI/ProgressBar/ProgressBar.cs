using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float maxValue;
    [SerializeField] private RectTransform sliderSlotRectTransform;
    [SerializeField] private RectTransform sliderRectTransform;

    private float maxWidthUI = 0f;

    public void UpdateCurrentValue(float currentValue)
    {
        float percentVal = (maxWidthUI * (currentValue / maxValue)) - maxWidthUI;
        sliderRectTransform.offsetMax = new Vector2(percentVal, sliderRectTransform.offsetMax.y);
    }

    private void Start()
    {
        maxWidthUI = sliderSlotRectTransform.rect.width;
    }
}
