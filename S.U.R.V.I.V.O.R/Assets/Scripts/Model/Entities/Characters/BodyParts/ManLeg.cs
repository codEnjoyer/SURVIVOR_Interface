using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManLeg : BodyPathWearableClothes
    {
        public Clothes Boots => clothesDict?[ClothType.Boots];

        public Clothes Pants => clothesDict?[ClothType.Pants];
    }
}