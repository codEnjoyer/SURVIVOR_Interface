using System.Collections.Generic;

public interface IHealth
{
    public IAlive Target { get; }
    public ICollection<IHealthProperty> HealthProperties { get; }
    
    public void AddProperty(IHealthProperty property)
    {
        if (HealthProperties.Contains(property))
            return;
        HealthProperties.Add(property);
        property.InitialAction(this);
    }

    public void DeleteProperty(IHealthProperty property)
    {
        HealthProperties.Remove(property);
        property.FinalAction(this);
    }

    public void OnTurnEnd()
    {
        foreach (var healthProperty in HealthProperties)
            healthProperty.OnTurnEnd(this);
    }
}