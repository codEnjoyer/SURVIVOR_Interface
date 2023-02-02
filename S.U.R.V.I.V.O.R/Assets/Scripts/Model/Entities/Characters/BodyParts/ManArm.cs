using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManArm : BodyPathWearableClothes
    {
        public Clothes Underwear => clothesDict?[ClothType.Underwear];
        public Clothes Jacket => clothesDict?[ClothType.Jacket];
    }
}