using System.Collections.Generic;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManStomach : BodyPathWearableClothes
    {
        public ManStomach(Body body) : base(body)
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Vest,null},
                {ClothType.Underwear,null},
                {ClothType.Jacket,null}
            };
        }
    }
}