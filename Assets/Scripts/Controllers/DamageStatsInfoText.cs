using UnityEngine;

public class DamageStatsInfoText : StatsInfo
{
    public override void UpdateHealth(float offsetHealth)
    {
        base.UpdateHealth(offsetHealth);

        if (offsetHealth < 0) base.ShowDamage(offsetHealth.ToString());
    }
}
