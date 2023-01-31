﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    [DataContract(Namespace = "Model.Entities.Characters.BodyParts")]
    public sealed class ManHead : BodyPathWearableClothes
    {
        public Clothes Hat => clothesDict[ClothType.Hat];

        public ManHead()
        {
            clothesDict = new Dictionary<ClothType, Clothes>
            {
                {ClothType.Hat,null}
            };
            
            possibleClothTypes = clothesDict.Keys.ToList();
        }
    }
}