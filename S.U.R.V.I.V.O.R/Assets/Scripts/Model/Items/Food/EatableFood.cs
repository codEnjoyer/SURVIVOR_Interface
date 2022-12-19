using UnityEngine;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Eatable))]
public class EatableFood: MonoBehaviour
{
    [SerializeField] private EatableFoodData data;
    
    public EatableFoodData Data => data;
}