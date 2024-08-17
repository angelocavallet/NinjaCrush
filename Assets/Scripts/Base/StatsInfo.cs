using TMPro;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] private StatsInfoScriptableObject statsInfoData;
    public Canvas canvas { private get; set; }

    public ProgressBar healthBar { private get; set; }

    public float displayedHealth { private get; set; }

    public void UpdateHealth(float offsetHealth)
    {
        displayedHealth += displayedHealth > 0 ? offsetHealth : 0;
        healthBar.UpdateCurrentValue(displayedHealth);
    }

    protected void Show(string displayedText)
    {
        GameObject damageGameObject = Instantiate(statsInfoData.DamageStatsInfoTextPrefab, canvas.transform);
        damageGameObject.GetComponent<TextMeshPro>().text = displayedText;
        damageGameObject.SetActive(true);
    }
}
