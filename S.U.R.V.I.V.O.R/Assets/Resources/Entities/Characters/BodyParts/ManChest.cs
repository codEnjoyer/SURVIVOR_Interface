using System;
using System.Collections.Generic;
using Model.GameEntity;
using UnityEngine.Events;


public sealed class ManChest : BodyPart, IWearClothes
{
    public Clothes Underwear { get; set; }

    public Clothes Jacket { get; set; }

    public Clothes Backpack { get; set; }
    public Clothes Vest { get; set; }

    public ManChest(Body body) : base(body) { }
    public void WearOrUnWear(Clothes clothToWear, bool shouldUnWear, out bool isSuccessful)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Clothes> GetClothes()
    {
        throw new NotImplementedException();
    }
}