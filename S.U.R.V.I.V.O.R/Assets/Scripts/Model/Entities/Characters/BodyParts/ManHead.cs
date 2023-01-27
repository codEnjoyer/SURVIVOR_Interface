using System.Collections.Generic;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManHead : BodyPathWearableClothes
    {
        public Clothes Hat => clothesDict[ClothType.Hat];

        public ManHead(Body body) : base(body)
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Hat,null}
            };
        }
    }
}