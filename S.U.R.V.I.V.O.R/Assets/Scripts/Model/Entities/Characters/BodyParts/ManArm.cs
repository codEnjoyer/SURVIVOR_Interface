using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    [DataContract(Namespace = "Model.Entities.Characters.BodyParts")]
    public sealed class ManArm : BodyPathWearableClothes
    {
        public Clothes Underwear => clothesDict[ClothType.Underwear];
        public Clothes Jacket => clothesDict[ClothType.Jacket];

        public ManArm()
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Jacket,null},
                {ClothType.Underwear,null}
            };

            possibleClothTypes = clothesDict.Keys.ToList();
        }
    }
}