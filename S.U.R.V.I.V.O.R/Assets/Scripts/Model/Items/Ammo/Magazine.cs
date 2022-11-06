using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magazine: InventoryItem
{
    [SerializeField] private AmmoContainerData data; 
    
    private Stack<Caliber> ammo;
    public Caliber DeLoad() => ammo.Pop();
    public void Load(Caliber caliber)
    {
        if (!caliber.Equals(data.CaliberType))
            throw new NotImplementedException();
        if (CurrentNumberAmmo < data.Capacity)
            ammo.Push(data.CaliberType);
    }
    
    public int CurrentNumberAmmo => ammo.Count;
    public bool IsEmpty => ammo.Count == 0;

    private void Awake()
    {
        ammo = new Stack<Caliber>(data.Capacity);
    }
}
