using System.Collections.Generic;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class AmmoBox : MonoBehaviour
{
    [SerializeField] private AmmoContainerData data;
    
    private Stack<SingleAmmo> ammoStack;
    public SingleAmmo TakeBullet() => ammoStack.Pop();
    public int CurrentNumberAmmo => ammoStack.Count;
    public bool IsEmpty => ammoStack.Count == 0;

    public AmmoContainerData Data => data;

    public Stack<SingleAmmo> AmmoStack => ammoStack;

    private void Awake()
    {
        ammoStack = new Stack<SingleAmmo>(data.Capacity);
        for (var i = 0; i < data.Capacity; i++)
            ammoStack.Push(data.AmmoType);
    }
}