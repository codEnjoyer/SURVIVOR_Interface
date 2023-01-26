using System.Collections.Generic;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManLeg : BodyPathWearableClothes
    {
        public Clothes Boots => clothesDict[ClothType.Boots];

        public Clothes Pants => clothesDict[ClothType.Pants];

        public ManLeg(Body body) : base(body)
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Pants,null},
                {ClothType.Boots,null}
            };
        }
    }
}