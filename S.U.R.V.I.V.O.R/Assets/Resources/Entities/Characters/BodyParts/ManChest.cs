using System;
using System.Collections.Generic;
using UnityEngine.Events;

public sealed class ManChest : BodyPart
{
    private Clothes underwear;

    public Clothes Underwear
    {
        get => underwear;
        set
        {
            underwear = value;
            OnClothesChanged.Invoke();
        }
    }

    private Clothes jacket;
    public Clothes Jacket 
    {
        get => jacket;
        set
        {
            jacket = value;
            OnClothesChanged.Invoke();
        }
    }
    public Clothes Backpack { get; set; }
    public Clothes Vest { get; set; }

    public ManChest(Body body) : base(body) { }

    public override int MaxHp { get; }
    public override float Hp { get; protected set; }
    
    public override float Size { get; }
    public override IEnumerable<Clothes> Clothes { get; }
    public override event Action OnClothesChanged;
}