using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GunInterfaceSet : MonoBehaviour
{
    public Character Player { get; set; }
    public Gun CurrentInterfaceSetGun { get; set; }
    [SerializeField]
    public SpecialGunCell gunSlot;
    [SerializeField]
    private GunMagazineSpecialCell magazineSlot;
    [SerializeField]
    private SpecialGunModuleCell springSlot;
    [SerializeField]
    private SpecialGunModuleCell shutterSlot;
    [SerializeField]
    private SpecialGunModuleCell scopeSlot;
    [SerializeField]
    private SpecialGunModuleCell gripSlot;
    [SerializeField]
    private SpecialGunModuleCell tacticalSlot;
    [SerializeField]
    private SpecialGunModuleCell supressorSlot;

    private List<SpecialGunModuleCell> allSlots;

    public UnityEvent GunPlaced = new ();
    public UnityEvent GunTaken = new ();
    
    public void Awake()
    {
        allSlots = new List<SpecialGunModuleCell>
        {
            shutterSlot,
            springSlot,
            scopeSlot,
            supressorSlot,
            tacticalSlot,
            gripSlot,
        };
        gunSlot.OnItemPlaced.AddListener(OnGunPlaced);
        gunSlot.OnItemTaked.AddListener(OnGunTaken);
    }

    private void OnGunPlaced()
    {
        CurrentInterfaceSetGun = gunSlot.PlacedItem.GetComponent<Gun>();
        CheckGunAvailableModules();
        UpdateAllSlots();
        GunPlaced.Invoke();
    }

    private void OnGunTaken()
    {
        CurrentInterfaceSetGun = null;
        ClearAllSlots();
        GunTaken.Invoke();
    }

    private void CheckGunAvailableModules()
    {
        var gun = gunSlot.PlacedItem.GetComponent<Gun>();
        if (springSlot != null)
        {
            springSlot.gameObject.SetActive(gun.CheckGunModule(GunModuleType.Spring));
            shutterSlot.gameObject.SetActive(gun.CheckGunModule(GunModuleType.Shutter));
            scopeSlot.gameObject.SetActive(gun.CheckGunModule(GunModuleType.Scope));
            gripSlot.gameObject.SetActive(gun.CheckGunModule(GunModuleType.Grip));
            supressorSlot.gameObject.SetActive(gun.CheckGunModule(GunModuleType.Suppressor));
            tacticalSlot.gameObject.SetActive(gun.CheckGunModule(GunModuleType.Tactical));
        }
    }

    private void ClearAllSlots()
    {
        foreach (var slot in allSlots)
        {
            if (slot != null)
            {
                slot.PlaceItem(null);
                slot.gameObject.SetActive(false);
            }
        }
    }
    
    private void UpdateAllSlots()
    {
        if (gunSlot.PlacedItem == null) return;
        foreach (var slot in allSlots)
        {
            if(slot != null)
                slot.gameObject.SetActive(true);
        }
        var gun = gunSlot.PlacedItem.GetComponent<Gun>();

        if (springSlot != null)
        {
            springSlot.PlaceItem(gun.GunModules.First(x => x.Data.ModuleType == GunModuleType.Spring).GetComponent<BaseItem>());
            shutterSlot.PlaceItem(gun.GunModules.First(x => x.Data.ModuleType == GunModuleType.Shutter).GetComponent<BaseItem>());
            supressorSlot.PlaceItem(gun.GunModules.First(x => x.Data.ModuleType == GunModuleType.Suppressor).GetComponent<BaseItem>());
            scopeSlot.PlaceItem(gun.GunModules.First(x => x.Data.ModuleType == GunModuleType.Scope).GetComponent<BaseItem>());
            gripSlot.PlaceItem(gun.GunModules.First(x => x.Data.ModuleType == GunModuleType.Grip).GetComponent<BaseItem>());
            tacticalSlot.PlaceItem(gun.GunModules.First(x => x.Data.ModuleType == GunModuleType.Tactical).GetComponent<BaseItem>());
            magazineSlot.PlaceItem(gun.CurrentMagazine.GetComponent<BaseItem>()); 
        }
    }
}
