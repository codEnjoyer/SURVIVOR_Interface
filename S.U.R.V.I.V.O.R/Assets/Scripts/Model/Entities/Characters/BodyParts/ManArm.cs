using System.Collections.Generic;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManArm : BodyPathWearableClothes
    {
        public Clothes Underwear => clothesDict[ClothType.Underwear];
        public Clothes Jacket => clothesDict[ClothType.Jacket];

        public ManArm(Body body) : base(body)
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Jacket,null},
                {ClothType.Underwear,null}
            };
        }
    }
}