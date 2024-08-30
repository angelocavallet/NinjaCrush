using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(TextMeshPro))]
public class StatsInfoText : MonoBehaviour
{
    [SerializeField] private StatsInfoTextScriptableObject statsInfoTextData;

    private RectTransform rectTransform;
    private TextMeshPro textMeshPro;
    private float secondsToFade;
    private float fadeVelocity;
    private float verticalVelocity;
    private float existTime;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshPro>();

        secondsToFade = statsInfoTextData.secondsToFade;
        fadeVelocity = statsInfoTextData.fadeVelocity;
        verticalVelocity = statsInfoTextData.verticalVelocity;

        existTime = Time.time;
    }

    void Update()
    {
        if (existTime + secondsToFade < Time.time)
        {
            float newAlpha = textMeshPro.color.a - Time.deltaTime * fadeVelocity;
            if (newAlpha > 0)
            {
                //@todo: move to Shader
                textMeshPro.color = new Color32(
                    (byte)(textMeshPro.color.r * 255),
                    (byte)(textMeshPro.color.g * 255),
                    (byte)(textMeshPro.color.b * 255),
                    (byte)(newAlpha * 255)
                );
            }
            else
            {
                Destroy(gameObject);
            }
        }

        transform.position = new Vector2(transform.position.x, transform.position.y + (Time.deltaTime * verticalVelocity));
    }
}
