using System.Collections.Generic;

namespace Model.GameEntity
{
    public abstract class BodyPathWearableClothes : BodyPart, IWearClothes
    {
        protected BodyPathWearableClothes(Body body, int maxHp = 100, int size = 100) : base(body, maxHp, size)
        {
        }

        protected Dictionary<ClothType, Clothes> clothesDict;
        
        public void WearOrUnWear(Clothes clothesToWear, bool shouldUnWear, out bool isSuccessful)
        {
            if (clothesToWear == null)
            {
                isSuccessful = false;
                return;
            }
        
            if (clothesDict.ContainsKey(clothesToWear.Data.ClothType))
            {
                var valueToWear = shouldUnWear ? null : clothesToWear;
                clothesDict[clothesToWear.Data.ClothType] = valueToWear;
            }
        
            isSuccessful = true;
        }
        public IEnumerable<Clothes> GetClothes() => clothesDict.Values;
    }
}