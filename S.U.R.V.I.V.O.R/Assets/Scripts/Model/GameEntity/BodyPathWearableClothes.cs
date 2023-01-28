using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model.GameEntity
{
    [DataContract]
    public abstract class BodyPathWearableClothes : BodyPart, IWearClothes
    {
        protected BodyPathWearableClothes(Body body, int maxHp = 100, int size = 100) 
            : base(body, maxHp, size)
        {
        }

        [IgnoreDataMember] protected Dictionary<ClothType, Clothes> clothesDict;

        public bool Wear(Clothes clothesToWear)
        {
            if (clothesToWear == null || !clothesDict.ContainsKey(clothesToWear.Data.ClothType) || clothesDict[clothesToWear.Data.ClothType] != null) 
                return false;
            
            clothesDict[clothesToWear.Data.ClothType] = clothesToWear;
            return true;
        }

        public Clothes UnWear(ClothType clothType)
        {
            try
            {
                var removedClothes = clothesDict[clothType];
                clothesDict[clothType] = null;
                return removedClothes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Clothes> GetClothes() => clothesDict.Values;
    }
}