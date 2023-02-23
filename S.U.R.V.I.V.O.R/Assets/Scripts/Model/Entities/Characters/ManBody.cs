using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Entities.Characters.BodyParts;
using Model.GameEntity;
using Model.GameEntity.EntityHealth;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Model.Entities.Characters
{
    public sealed class ManBody : Body, IWearClothes
    {
        private int energy;
        private int hunger;
        private int water;

        [SerializeField] [Min(0)] private int maxEnergy = 10;
        [SerializeField] [Min(0)] private int maxHunger = 10;
        [SerializeField] [Min(0)] private int maxWater = 10;

        [field: SerializeField] public ManHead Head { get; private set; }
        [field: SerializeField] public ManChest Chest { get; private set; }
        [field: SerializeField] public ManStomach Stomach { get; private set; }
        [field: SerializeField] public ManArm LeftArm { get; private set; }
        [field: SerializeField] public ManArm RightArm { get; private set; }
        [field: SerializeField] public ManLeg LeftLeg { get; private set; }
        [field: SerializeField] public ManLeg RightLeg { get; private set; }

        private IWearClothes[] wearClothesBodyParts;

        protected override void Awake()
        {
            base.Awake();
            // Проверка
            if (
                Head != (ManHead) BodyParts[0] ||
                Chest != (ManChest) BodyParts[1] ||
                Stomach != (ManStomach) BodyParts[2] ||
                LeftArm != (ManArm) BodyParts[3] ||
                RightArm != (ManArm) BodyParts[4] ||
                LeftLeg != (ManLeg) BodyParts[5] ||
                RightLeg != (ManLeg) BodyParts[6]
            )
                throw new Exception("Несостыковка!");
            //

            Energy = MaxEnergy;
            Hunger = MaxHunger;
            Water = MaxWater;
            wearClothesBodyParts = BodyParts.OfType<IWearClothes>().ToArray();
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
            set => maxEnergy = Math.Max(1, value);
        }

        public int MaxHunger
        {
            get => maxHunger;
            set => maxHunger = Math.Max(1, value);
        }

        public int MaxWater
        {
            get => maxWater;
            set => maxWater = Math.Max(1, value);
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
            Destroy(itemToPlace);
            return false;
        }

        public bool Wear(Clothes clothesToWear)
        {
            if (clothesToWear == null)
                return false;
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


        public override BodyData CreateData()
        {
            var baseSave = base.CreateData(); 
            return new ManBodyData()
            {
                healthProperties = baseSave.healthProperties,
                bodyPartSaves = baseSave.bodyPartSaves,
                energy = Energy,
                hunger = hunger,
                water = water,
                
                maxEnergy = maxEnergy,
                maxHunger = maxHunger,
                maxWater = maxWater,
            };
        }


        public override void Restore(BodyData data)
        {
            base.Restore(data);
            if (data is ManBodyData manBodySave)
            {
                MaxEnergy = manBodySave.maxEnergy;
                MaxHunger = manBodySave.maxHunger;
                MaxWater = manBodySave.maxWater;

                Energy = manBodySave.energy;
                Hunger = manBodySave.hunger;
                Water = manBodySave.water;
            }
        }
    }

    [DataContract]
    public class ManBodyData: BodyData
    {
        [DataMember] public int energy;
        [DataMember] public int hunger;
        [DataMember] public int water;

        [DataMember] public int maxEnergy;
        [DataMember] public int maxHunger;
        [DataMember] public int maxWater;
    }
}