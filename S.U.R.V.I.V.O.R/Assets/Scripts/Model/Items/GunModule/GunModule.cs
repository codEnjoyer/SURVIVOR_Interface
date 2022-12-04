using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class GunModule : MonoBehaviour
{
    [SerializeField] private GunModuleData data;
    public GunModuleData Data => data;

    public void SetGunModuleData(GunModuleData newGunModuleData) => data = newGunModuleData;
}