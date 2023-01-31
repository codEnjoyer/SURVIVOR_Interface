using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    [DataContract(Namespace = "Model.Entities.Characters.BodyParts")]
    public sealed class ManStomach : BodyPathWearableClothes
    {
        public ManStomach()
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Vest, null},
                {ClothType.Underwear, null},
                {ClothType.Jacket, null}
            };
            possibleClothTypes = clothesDict.Keys.ToList();
        }
    }
}