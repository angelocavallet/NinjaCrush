using UnityEngine;

public class DamageStatsInfoText : StatsInfo
{
    public void UpdateHealth(float offsetHealth)
    {
        base.UpdateHealth(offsetHealth);

        if (offsetHealth < 0) base.Show(offsetHealth.ToString());
    }
}
