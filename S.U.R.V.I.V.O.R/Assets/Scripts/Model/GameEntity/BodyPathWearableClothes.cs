using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

namespace Model.GameEntity
{
    public abstract class BodyPathWearableClothes : BodyPart, IWearClothes
    {
        [SerializeField] private List<ClothType> possibleClothTypes;
        protected Dictionary<ClothType, Clothes> clothesDict;

        public float currentArmor => clothesDict.Values.Sum(x => x.CurrentArmor);
        
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

        public void GetDamageToArmor(float damage){}//TODO нанести урон броне
        
        protected override void Awake()
        {
            base.Awake();
            clothesDict = new Dictionary<ClothType, Clothes>();
            foreach (var possibleClothType in possibleClothTypes)
                clothesDict.Add(possibleClothType, null);
        }
    }
}