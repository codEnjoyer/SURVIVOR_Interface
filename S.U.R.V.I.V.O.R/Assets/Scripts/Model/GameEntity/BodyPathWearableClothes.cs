using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Model.GameEntity
{
    [DataContract(Namespace = "Model.GameEntity")]
    public abstract class BodyPathWearableClothes : BodyPart, IWearClothes
    {
        [DataMember] protected List<ClothType> possibleClothTypes;
        [IgnoreDataMember] protected Dictionary<ClothType, Clothes> clothesDict;

        protected BodyPathWearableClothes(int maxHp = 100, int size = 100) : base(maxHp, size)
        {
        }

        public bool Wear(Clothes clothesToWear)
        {
            if (clothesToWear == null || !clothesDict.ContainsKey(clothesToWear.Data.ClothType) ||
                clothesDict[clothesToWear.Data.ClothType] != null)
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

        public void RestoreClothesDict()
        {
            clothesDict = new Dictionary<ClothType, Clothes>();
            foreach (var possibleClothType in possibleClothTypes)
                clothesDict.Add(possibleClothType, null);
        }
    }
}