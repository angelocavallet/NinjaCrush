using TMPro;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] private StatsInfoScriptableObject statsInfoData;

    private ProgressBar healthBar;
    private float displayedHealth;

    public float maxHealth
    {
        set {
            displayedHealth = value;
            healthBar.maxValue = value;
        }
    }

    public void Awake()
    {
        healthBar = GetComponentInChildren<ProgressBar>();
    }

    public void UpdateHealth(float health)
    {
        float healthOffset = health - displayedHealth;

        if (healthOffset < 0) ShowDamage(healthOffset.ToString());

        displayedHealth = health > 0 ? health : 0;

        healthBar.UpdateCurrentValue(displayedHealth);
    }

    protected void ShowDamage(string damageText)
    {
        Show(statsInfoData.DamageStatsInfoTextPrefab, damageText);
    }

    protected void Show(GameObject statsInfoTextprefab, string displayedText)
    {
        GameObject damageGameObject = Instantiate(statsInfoTextprefab);
        damageGameObject.transform.position = transform.position;
        damageGameObject.GetComponent<TextMeshPro>().text = displayedText;
        damageGameObject.SetActive(true);
    }
}
