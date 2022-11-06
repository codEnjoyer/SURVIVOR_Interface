using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magazine: Item 
{
    private Stack<Bullet> ammo;
    [SerializeField] private Bullet bullet;
    [SerializeField] private int capacity;
    public Bullet DeLoad() => ammo.Pop();
    public void Load()
    {
        if (CurrentNumberAmmo < capacity)
            ammo.Push(ScriptableObject.CreateInstance<Bullet>());
    }
    
    public int CurrentNumberAmmo => ammo.Count;
    public bool isEmpty => ammo.Count == 0;

    private void Awake()
    {
        ammo = new Stack<Bullet>(capacity);
    }
}
