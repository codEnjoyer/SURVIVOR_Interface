using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class EatableFood: MonoBehaviour
{
    [SerializeField] private EatableFoodData data;
    
    public EatableFoodData Data => data;
}