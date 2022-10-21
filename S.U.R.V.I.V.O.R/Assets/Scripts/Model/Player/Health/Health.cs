using System.Collections.Generic;

public class Health
{
    private Body body;
    private float radiation;
    private readonly List<IHealthProperty> healthProperties = new();
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

    public void AddProperty(IHealthProperty property)
    {
        healthProperties.Add(property); 
        //TODO добавлять только если такого свойства нет
        property.InitialAction(this);
    }

    public void DeleteProperty(IHealthProperty property)
    {
        healthProperties.Remove(property);
        property.FinalAction(this);
    }

    public void OnTurnEnd()
    {
        foreach (var healthProperty in healthProperties)
            healthProperty.OnTurnEnd(this);
    }
}