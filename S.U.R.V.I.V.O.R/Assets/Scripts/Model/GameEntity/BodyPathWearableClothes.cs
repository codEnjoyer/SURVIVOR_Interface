using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Items;
using UnityEngine;

namespace Model.GameEntity
{
    public abstract class BodyPathWearableClothes : BodyPart, IWearClothes
    {
        [SerializeField] private List<ClothType> possibleClothTypes;
        protected Dictionary<ClothType, Clothes> clothesDict;
        public float CurrentArmor => clothesDict.Values.Sum(x => x.CurrentArmor);
        public bool IsArmorDestroyed => CurrentArmor == 0f;
        
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

        public IEnumerable<BaseItem> GetItemsFromInventory()
        {
            if (clothesDict == null || clothesDict.Values == null || clothesDict.Values.Count == 0) return null;
            return clothesDict.Values
                .Where(x => x != null && x.Inventory != null)
                .SelectMany(x => x.Inventory.GetItems());
        } 

        public void DamageArmor(float damage)
        {
            if (damage < 0) throw new ArgumentException("Урон по броне меньше нуля");
            var cloth = clothesDict.Values.OrderBy(x => x.CurrentArmor).First();

            cloth.CurrentArmor = damage <= cloth.CurrentArmor ? cloth.CurrentArmor - damage : 0;
        }
        
        protected override void Awake()
        {
            base.Awake();
            clothesDict = new Dictionary<ClothType, Clothes>();
            foreach (var possibleClothType in possibleClothTypes)
                clothesDict.Add(possibleClothType, null);
        }
    }
}