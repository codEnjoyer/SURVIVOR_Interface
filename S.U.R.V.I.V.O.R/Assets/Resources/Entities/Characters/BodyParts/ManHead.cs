using System;
using System.Collections.Generic;
using Model.GameEntity;


public sealed class ManHead : BodyPart, IWearClothes
{
    public Clothes Hat { get; set; }
    public ManHead(Body body) : base(body) {}
    public void WearOrUnWear(Clothes clothToWear, bool shouldUnWear, out bool isSuccessful)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Clothes> GetClothes()
    {
        throw new NotImplementedException();
    }
}