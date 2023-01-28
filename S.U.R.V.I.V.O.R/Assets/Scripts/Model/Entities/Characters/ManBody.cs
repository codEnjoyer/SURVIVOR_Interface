using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Entities.Characters.BodyParts;
using Model.GameEntity;
using Model.GameEntity.Health;
using Object = UnityEngine.Object;

namespace Model.Entities.Characters
{
    [DataContract]
    public class ManBody : Body, IWearClothes
    {
        [DataMember] private int energy;
        [DataMember] private int hunger;
        [DataMember] private int water;

        [DataMember] private int maxEnergy = 10;
        [DataMember] private int maxHunger = 10;
        [DataMember] private int maxWater = 10;
        
        [DataMember] public ManHead Head { get; private set; }
        [DataMember] public ManChest Chest { get; private set; }
        [DataMember] public ManStomach Stomach { get; private set; }
        [DataMember] public ManArm LeftArm { get; private set; }
        [DataMember] public ManArm RightArm { get; private set; }
        [DataMember] public ManLeg LeftLeg { get; private set; }
        [DataMember] public ManLeg RightLeg { get; private set; }

        private readonly IWearClothes[] wearClothesBodyParts;

        public ManBody()
        {
            Head = new ManHead(this);
            Chest = new ManChest(this);
            Stomach = new ManStomach(this);
            LeftArm = new ManArm(this);
            RightArm = new ManArm(this);
            LeftLeg = new ManLeg(this);
            RightLeg = new ManLeg(this);

            bodyParts.AddRange(new List<BodyPart> {Head, Chest, Stomach, LeftArm, RightArm, LeftLeg, RightLeg});
            Energy = maxEnergy;
            Hunger = maxHunger;
            Water = maxWater;
            MaxCriticalLoses = bodyParts.Count;

            wearClothesBodyParts = bodyParts.OfType<IWearClothes>().ToArray();
        }

        public event Action<Health> PlayerTired;
        public event Action<Health> PlayerHungry;
        public event Action<Health> PlayerThirsty;

        public event Action<int> EnergyChange;
        public event Action<int> HungerChange;
        public event Action<int> WaterChange;
        public event Action<ClothType> WearChanged;
        
        public int Energy
        {
            get => energy;
            set
            {
                if (value <= 0)
                {
                    energy = 0;
                    PlayerTired?.Invoke(Health);
                }
                else if (value > maxEnergy)
                    energy = maxEnergy;
                else
                    energy = value;

                EnergyChange?.Invoke(energy);
            }
        }

        public int Hunger
        {
            get => hunger;
            set
            {
                if (value <= 0)
                {
                    hunger = 0;
                    PlayerHungry?.Invoke(Health);
                }
                else if (value > maxHunger)
                    hunger = maxHunger;
                else
                    hunger = value;

                HungerChange?.Invoke(hunger);
            }
        }

        public int Water
        {
            get => water;
            set
            {
                if (value <= 0)
                {
                    water = 0;
                    PlayerThirsty?.Invoke(Health);
                }
                else if (value > maxWater)
                    water = maxWater;
                else
                    water = value;

                WaterChange?.Invoke(water);
            }
        }

        public int MaxEnergy
        {
            get => maxEnergy;
            set => maxEnergy = Math.Min(1, value);
        }

        public int MaxHunger
        {
            get => maxHunger;
            set => maxHunger = Math.Min(1, value);
        }

        public int MaxWater
        {
            get => maxWater;
            set => maxWater = Math.Min(1, value);
        }


        public Clothes GetClothByType(ClothType type)
        {
            switch (type)
            {
                case ClothType.Backpack:
                    return Chest.Backpack;
                case ClothType.Boots:
                    return RightLeg.Boots;
                case ClothType.Pants:
                    return RightLeg.Pants;
                case ClothType.Hat:
                    return Head.Hat;
                case ClothType.Jacket:
                    return Chest.Jacket;
                case ClothType.Underwear:
                    return Chest.Underwear;
                case ClothType.Vest:
                    return Chest.Vest;
            }

            return default;
        }

        public bool PlaceItemToInventory(BaseItem itemToPlace)
        {
            var clothes = GetClothes();
            foreach (var cloth in clothes)
            {
                if (cloth.Inventory.InsertItem(itemToPlace))
                    return true;
            }

            if (LocationInventory.Instance.LocationInventoryGrid.InsertItem(itemToPlace))
                return true;
            Object.Destroy(itemToPlace);
            return false;
        }

        public bool Wear(Clothes clothesToWear)
        {
            var isSuccess = false;
            foreach (var wearClothesBodyPart in wearClothesBodyParts)
            {
                if (wearClothesBodyPart.Wear(clothesToWear))
                    isSuccess = true;
            }

            if (isSuccess) WearChanged?.Invoke(clothesToWear.Data.ClothType);
            return isSuccess;
        }


        public Clothes UnWear(ClothType clothType)
        {
            Clothes clothes = null;
            foreach (var bodyPart in wearClothesBodyParts)
            {
                var x = bodyPart.UnWear(clothType);
                if (x is not null)
                    clothes = x;
            }

            if (clothes is not null)
            {
                WearChanged?.Invoke(clothType);
            }

            return clothes;
        }

        public IEnumerable<Clothes> GetClothes()
        {
            var clothes = new List<Clothes>();
            foreach (var bodyPart in wearClothesBodyParts)
            {
                clothes.AddRange(bodyPart.GetClothes());
            }

            return clothes.Distinct().Where(x => x is not null);
        }
    }
}