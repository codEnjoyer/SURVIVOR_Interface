using System.Collections.Generic;

public class Health
{
    private Body body;
    private float radiation;
    private List<HealthProperty> healthProperties = new();
    public float Radiation
    {
        get => radiation;
        set
        {
            if (value >= 0)
                radiation = value;
        }
    }

    public Health(Body body)
    {
        this.body = body;
    }

    public void AddProperty(HealthProperty healthProperty)
    {
        healthProperties.Add(healthProperty); //TODO добавлять только если такого свойства нет
    }

    public void DeleteProperty(HealthProperty healthProperty)
    {
        healthProperties.Remove(healthProperty);
    }

    public void OnTurnEnd()
    {
        
    }
}