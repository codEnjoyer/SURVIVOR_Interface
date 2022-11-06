using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AmmoStorage: Item
{
    [SerializeField] private AmmoStorageData ammoStorageData;
    
    protected Stack<Bullet> ammo;

    public int Capacity => ammoStorageData.Capacity;
    public Bullet CaliberType => ammoStorageData.CaliberType; 
    public UnityEvent onWrongCaliber = new();
    public UnityEvent onOverflowing = new();
    public UnityEvent onLoad = new();
    public UnityEvent onDeLoad = new();

    public virtual void Load(Bullet bullet)
    {
        if (!bullet.Equals(CaliberType))
            onWrongCaliber?.Invoke();
        else if (CurrentNumberAmmo >= Capacity)
            onOverflowing?.Invoke();
        else
        {
            ammo.Push(CaliberType);
            onLoad?.Invoke();
        }
    }

    public virtual Bullet DeLoad()
    {
        onDeLoad?.Invoke();
        return ammo.Pop();
    }

    public int CurrentNumberAmmo => ammo.Count;
    public bool isEmpty => ammo.Count == 0;

    private void Awake()
    {
        ammo = new Stack<Bullet>(Capacity);
    }
}