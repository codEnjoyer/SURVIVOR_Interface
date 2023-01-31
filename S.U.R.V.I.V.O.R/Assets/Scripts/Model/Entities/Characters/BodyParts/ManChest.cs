using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    [DataContract(Namespace = "Model.Entities.Characters.BodyParts")]
    public sealed class ManChest : BodyPathWearableClothes
    {
        public Clothes Underwear => clothesDict[ClothType.Underwear];
        public Clothes Jacket => clothesDict[ClothType.Jacket];
        public Clothes Backpack => clothesDict[ClothType.Backpack];
        public Clothes Vest => clothesDict[ClothType.Vest];

        public ManChest()
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Backpack,null},
                {ClothType.Jacket,null},
                {ClothType.Vest,null},
                {ClothType.Underwear,null}
            };
            possibleClothTypes = clothesDict.Keys.ToList();
        }
    }
}