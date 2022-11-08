using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BaseItem))]
public class Magazine: MonoBehaviour
{
    [SerializeField] private AmmoContainerData data;

    private Stack<SingleAmmo> ammoStack;
    public SingleAmmo DeLoad() => ammoStack.Pop();
    public void Load(SingleAmmo ammo)
    {
        if (!ammo.Equals(data.AmmoType))
            throw new NotImplementedException();
        if (CurrentNumberAmmo < data.Capacity)
            this.ammoStack.Push(data.AmmoType);
    }
    
    public int CurrentNumberAmmo => ammoStack.Count;
    public bool IsEmpty => ammoStack.Count == 0;

    private void Awake()
    {
        ammoStack = new Stack<SingleAmmo>(data.Capacity);
    }
}
