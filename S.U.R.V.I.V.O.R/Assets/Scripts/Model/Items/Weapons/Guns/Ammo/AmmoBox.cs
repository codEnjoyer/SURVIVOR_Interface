using System.Collections.Generic;
using UnityEngine;


public class AmmoBox : Item
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int capacity;
    private Stack<Bullet> ammo;
    public Bullet TakeBullet() => ammo.Pop();

    public void LoadBullet()
    {
        if (CurrentNumberPatron < capacity)
            ammo.Push(ScriptableObject.CreateInstance<Bullet>());
    }

    public int CurrentNumberPatron => ammo.Count;
    public bool isEmpty => ammo.Count == 0;

    private void Awake()
    {
        ammo = new Stack<Bullet>(capacity);
        for (var i = 0; i < capacity; i++)
            ammo.Push(ScriptableObject.CreateInstance<Bullet>());
    }
}