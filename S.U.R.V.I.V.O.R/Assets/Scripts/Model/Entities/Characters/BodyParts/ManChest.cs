using System.Collections.Generic;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManChest : BodyPathWearableClothes
    {
        public Clothes Underwear => clothesDict[ClothType.Underwear];
        public Clothes Jacket => clothesDict[ClothType.Jacket];
        public Clothes Backpack => clothesDict[ClothType.Backpack];
        public Clothes Vest => clothesDict[ClothType.Vest];

        public ManChest(Body body) : base(body)
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Backpack,null},
                {ClothType.Jacket,null},
                {ClothType.Vest,null},
                {ClothType.Underwear,null}
            };
        }
    }
}