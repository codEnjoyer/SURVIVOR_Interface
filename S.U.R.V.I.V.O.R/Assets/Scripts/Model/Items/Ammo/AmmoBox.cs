using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Caliber", menuName = "Data/Bullet Data", order = 50)]
public class AmmoBox : InventoryItem
{
    private AmmoContainerData data;
    private Stack<Caliber> ammo;
    public Caliber TakeBullet() => ammo.Pop();
    public int CurrentNumberAmmo => ammo.Count;
    public bool IsEmpty => ammo.Count == 0;

    private void Awake()
    {
        ammo = new Stack<Caliber>(data.Capacity);
        for (var i = 0; i < data.Capacity; i++)
            ammo.Push(data.CaliberType);
    }
}