using UnityEngine;

public class Location: MonoBehaviour
{
    [SerializeField] private LocationData data;

    public LocationData Data => data;

    public void Awake()
    {
        if (data == null)
        {
            Debug.Log("У ноды нет локации!");
        }
    }

    public BaseItem GetLoot() => data.GetLoot();
}