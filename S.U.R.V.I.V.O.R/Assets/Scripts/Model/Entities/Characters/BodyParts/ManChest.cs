﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;

namespace Model.Entities.Characters.BodyParts
{
    public sealed class ManChest : BodyPathWearableClothes
    {
        public Clothes Underwear => clothesDict?[ClothType.Underwear];
        public Clothes Jacket => clothesDict?[ClothType.Jacket];
        public Clothes Backpack => clothesDict?[ClothType.Backpack];
        public Clothes Vest => clothesDict?[ClothType.Vest];
    }
}