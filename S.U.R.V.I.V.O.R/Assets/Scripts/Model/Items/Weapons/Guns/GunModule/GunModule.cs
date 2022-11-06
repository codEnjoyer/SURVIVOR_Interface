using UnityEngine;

public class GunModule: Item
{
    [SerializeField] private GunModuleData gunModuleData;

    public GunModuleData GunModuleData => gunModuleData;
}