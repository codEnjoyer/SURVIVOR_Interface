using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GunInterfaceSet : MonoBehaviour
{
    public Character Player { get; set; }
    public IGun CurrentInterfaceSetGun { get; set; }
    [SerializeField]
    public SpecialCell gunSlot;
    [SerializeField]
    private SpecialCell magazineSlot;
    [SerializeField]
    private SpecialCell springSlot;
    [SerializeField]
    private SpecialCell shutterSlot;
    [SerializeField]
    private SpecialCell scopeSlot;
    [SerializeField]
    private SpecialCell gripSlot;
    [SerializeField]
    private SpecialCell tacticalSlot;
    [SerializeField]
    private SpecialCell supressorSlot;

    public UnityEvent GunPlaced = new ();
    public UnityEvent GunTaken = new ();
    
    public void Awake()
    {
        gunSlot.OnItemPlaced.AddListener(OnGunPlaced);
        gunSlot.OnItemTaked.AddListener(OnGunTaken);
        
        magazineSlot.OnItemPlaced.AddListener(OnMagazinePlaced);
        magazineSlot.OnItemTaked.AddListener(OnMagazineTaken);
    }

    private void OnGunPlaced()
    {
        CurrentInterfaceSetGun = gunSlot.PlacedItem.GetComponent<IGun>();
        CheckGunAvailableModules();
        //TODO Когда оружие кладут в слот расставить по местам модулей все его модули.
        GunPlaced.Invoke();
    }

    private void OnGunTaken()
    {
        CurrentInterfaceSetGun = null;
        //TODO Когда оружие берут, убрать из слотов все его модули
        GunTaken.Invoke();
    }

    private void OnMagazinePlaced()
    {
        CurrentInterfaceSetGun.Reload(magazineSlot.PlacedItem.GetComponent<Magazine>());
    }

    private void OnMagazineTaken()
    {
        CurrentInterfaceSetGun.Reload(null);
    }

    private void CheckGunAvailableModules()
    {
        springSlot.gameObject.SetActive(CurrentInterfaceSetGun.AvailableGunModules.Contains(SpecialCellType.ModuleSpring));
        shutterSlot.gameObject.SetActive(CurrentInterfaceSetGun.AvailableGunModules.Contains(SpecialCellType.ModuleShutter));
        scopeSlot.gameObject.SetActive(CurrentInterfaceSetGun.AvailableGunModules.Contains(SpecialCellType.ModuleScope));
        gripSlot.gameObject.SetActive(CurrentInterfaceSetGun.AvailableGunModules.Contains(SpecialCellType.ModuleGrip));
        supressorSlot.gameObject.SetActive(CurrentInterfaceSetGun.AvailableGunModules.Contains(SpecialCellType.ModuleSuppressor));
        tacticalSlot.gameObject.SetActive(CurrentInterfaceSetGun.AvailableGunModules.Contains(SpecialCellType.ModuleTactical));
    }
}
