using TMPro;
using UnityEngine;

public class StatsTextController : MonoBehaviour
{
    public float velocityUp;

    public float fadeVelocity;

    public float secondsToFade;

    private RectTransform _rectTransform;

    private TextMeshPro _textMeshPro;

    private float existTime;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshPro>();
        existTime = Time.time;
    }

    void Update()
    {
        if (existTime + secondsToFade < Time.time)
        {
            float newAlpha = _textMeshPro.color.a - Time.deltaTime * fadeVelocity;
            if (newAlpha > 0)
            {
                _textMeshPro.color = new Color32(
                    (byte) (_textMeshPro.color.r * 255), 
                    (byte) (_textMeshPro.color.g * 255), 
                    (byte) (_textMeshPro.color.b * 255), 
                    (byte) (newAlpha * 255)
                );
            }
            else
            {
                Destroy(gameObject);
            }
        }

        _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x, _rectTransform.localPosition.y + (Time.deltaTime * velocityUp));
    }
}
