using TMPro;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] private StatsInfoScriptableObject statsInfoData;
    public Canvas canvas { private get; set; }

    public ProgressBar healthBar { private get; set; }

    public float displayedHealth { private get; set; }

    public virtual void UpdateHealth(float offsetHealth)
    {
        displayedHealth += displayedHealth > 0 ? offsetHealth : 0;
        healthBar.UpdateCurrentValue(displayedHealth);
    }

    protected void ShowDamage(string damageText)
    {
        Show(statsInfoData.DamageStatsInfoTextPrefab, damageText);
    }
    

    protected void Show(GameObject statsInfoTextprefab, string displayedText)
    {
        GameObject damageGameObject = Instantiate(statsInfoTextprefab, canvas.transform);
        damageGameObject.GetComponent<TextMeshPro>().text = displayedText;
        damageGameObject.SetActive(true);
    }
}
