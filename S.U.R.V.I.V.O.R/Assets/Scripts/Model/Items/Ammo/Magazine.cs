using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BaseItem))]
public class Magazine: MonoBehaviour
{
    [SerializeField] private MagazineData data;

    private Stack<SingleAmmo> ammoStack;
    public SingleAmmo DeLoad() => ammoStack.Pop();
    public void Load(SingleAmmo ammo)
    {
        //TODO сделать класс магазина
    }
    
    public int CurrentNumberAmmo => ammoStack.Count;
    public bool IsEmpty => ammoStack.Count == 0;

    public MagazineData Data => data;

    private void Awake()
    {
        //TODO class magazine
    }
}
