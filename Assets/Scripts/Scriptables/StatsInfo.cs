using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "new StatsInfo", menuName = "ScriptableObjects/StatsInfo")]
public class StatsInfo : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private GameObject statsInfoPrefab;

    [Header("StatsText Prefabs")]
    [SerializeField] private GameObject damageStatsText;

    public Canvas canvas { private get; set; }

    public ProgressBar healthBar { private get; set; }

    public float displayedHealth { private get; set; }

    public StatsInfo Clone(Transform transform, float maxHealth)
    {
        StatsInfo newStatsInfo = Instantiate(this);

        GameObject statsInfoGameObject = Instantiate(statsInfoPrefab, transform.position, transform.rotation, transform);
        newStatsInfo.canvas = statsInfoGameObject.GetComponent<Canvas>();
        newStatsInfo.healthBar = statsInfoGameObject.GetComponentInChildren<ProgressBar>();
        newStatsInfo.healthBar.maxValue = maxHealth;
        newStatsInfo.displayedHealth = maxHealth;

        return newStatsInfo;
    }

    public void UpdateHealth(float offsetHealth)
    {
        displayedHealth += displayedHealth > 0 ? offsetHealth : 0;
        healthBar.UpdateCurrentValue(displayedHealth);

        if (offsetHealth < 0) ShowDamage(offsetHealth.ToString());
    }

    private void ShowDamage(string displayedText)
    {
        GameObject damageGameObject = Instantiate(damageStatsText, canvas.transform);
        damageGameObject.GetComponent<TextMeshPro>().text = displayedText;
        damageGameObject.SetActive(true);
    }
}
