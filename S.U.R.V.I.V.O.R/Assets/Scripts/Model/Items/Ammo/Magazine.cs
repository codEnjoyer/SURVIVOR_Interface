using System;
using System.Collections.Generic;
using Model.Items;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Reloadable))]
public class Magazine: MonoBehaviour
{
    [SerializeField] private MagazineData data;

    private Stack<SingleAmmo> ammoStack;
    public SingleAmmo GetAmmo() => ammoStack.Pop();
    public void Load(AmmoBox box)
    {
        var amount = box.CurrentNumberAmmo;
        if (box.Data.Caliber != Data.Caliber) return;
        for (int i = 0; i < amount; i++)
        {
            ammoStack.Push(box.TakeBullet());
            if (CurrentNumberAmmo == Data.MaxAmmoAmount)
                return;
        }
    }
    
    public int CurrentNumberAmmo => ammoStack.Count;
    public bool IsEmpty => ammoStack.Count == 0;

    public MagazineData Data => data;

    private void Awake()
    {
        ammoStack = new Stack<SingleAmmo>();
    }
}
