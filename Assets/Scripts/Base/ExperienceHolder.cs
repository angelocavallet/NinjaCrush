
using System.Numerics;

public class ExperienceHolder
{
    public long experience
    {
        get => _experience;
    }

    private long _experience;

    public ExperienceHolder(long experience)
    {
        this._experience = experience;
    }

    public void gainExperience(long experience)
    {
        this._experience += experience;
    }

    public void loseExperience(long experience)
    {
        this._experience -= experience;
    }
}
