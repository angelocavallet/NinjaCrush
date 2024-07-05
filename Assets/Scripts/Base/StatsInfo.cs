using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(TextMeshPro))]
public class StatsInfo : MonoBehaviour
{
    [SerializeField] private StatsInfoScriptableObject statsInfoData;
    public Canvas canvas { private get; set; }

    public ProgressBar healthBar { private get; set; }

    public float displayedHealth { private get; set; }

    private GameObject statsTextPrefab;
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

        secondsToFade = statsInfoData.secondsToFade;
        fadeVelocity = statsInfoData.fadeVelocity;
        verticalVelocity = statsInfoData.verticalVelocity;
        statsTextPrefab = statsInfoData.statsTextPrefab;

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

        rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, rectTransform.localPosition.y + (Time.deltaTime * verticalVelocity));
    }

    public void UpdateHealth(float offsetHealth)
    {
        displayedHealth += displayedHealth > 0 ? offsetHealth : 0;
        healthBar.UpdateCurrentValue(displayedHealth);
    }

    protected void Show(string displayedText)
    {
        GameObject damageGameObject = Instantiate(statsTextPrefab, canvas.transform);
        damageGameObject.GetComponent<TextMeshPro>().text = displayedText;
        damageGameObject.SetActive(true);
    }
}
