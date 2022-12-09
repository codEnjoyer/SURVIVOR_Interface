using System;
using System.Collections.Generic;
using Model.GameEntity;
using UnityEngine.Events;


public sealed class ManLeg : BodyPart, IWearClothes
{
    public Clothes Boots { get; set; }

    public Clothes Pants { get; set; }
    
    public ManLeg(Body body) : base(body)
    {
    }

    public void WearOrUnWear(Clothes clothToWear, bool shouldUnWear, out bool isSuccessful)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Clothes> GetClothes()
    {
        throw new NotImplementedException();
    }
}