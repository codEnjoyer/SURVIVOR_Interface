using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.GameEntity;
using Model.GameEntity.Skills;
using Model.Items;
using Model.SaveSystem;
using UnityEngine;
using BodyPart = Model.GameEntity.BodyPart;

namespace Model.Entities.Characters
{
    [RequireComponent(typeof(Saved))]
    public class Character : Entity, ISaved<CharacterSave>
    {
        public ManBody body = new();
        [SerializeField] private Sprite sprite;
        [SerializeField] private string firstName;
        [SerializeField] private string surname;

        private Gun primaryGun;
        private Gun secondaryGun;
        public MeleeWeapon MeleeWeapon { get; set; }

        public readonly Skills skills;
        public event Action<GunType> OnGunsChanged;

        public Character()
        {
            skills = new Skills(this);
        }


        public override Body Body => body;
        public Sprite Sprite => sprite;
        public string FirstName => firstName;
        public string Surname => surname;

        public Gun PrimaryGun
        {
            get => primaryGun;
            set
            {
                primaryGun = value;
                OnGunsChanged?.Invoke(GunType.PrimaryGun);
            }
        }

        public Gun SecondaryGun
        {
            get => secondaryGun;
            set
            {
                secondaryGun = value;
                OnGunsChanged?.Invoke(GunType.SecondaryGun);
            }
        }


        public void Eat(EatableFood food)
        {
            body.Energy += food.Data.DeltaEnergy;
            body.Water += food.Data.DeltaWater;
            body.Hunger += food.Data.DeltaHunger;
            food.GetComponent<BaseItem>().Destroy();
        }

        public IEnumerable<BaseItem> Cook(CookableFood food)
        {
            //TODO Добавить опыт к навыку готовки
            return food.Cook();
        }

        public BaseItem Loot(LocationData infoAboutLocation)
        {
            //TODO Добавить опыт к навыку лутания в зависмости от редкости найденной вещи
            return infoAboutLocation.GetLoot();
        }

        public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте

        public override void Attack(IEnumerable<BodyPart> targets, float distance)
        {
            var damage = new DamageInfo(40f);
            targets.First().TakeDamage(damage);
        }

        public CharacterSave CreateSave()
        {
            return new CharacterSave()
            {
                resourcesPath = GetComponent<Saved>().ResourcesPath,
                manBody = body,
                hat = body.Head.Hat?.CreateSave(),
                underwear = body.Chest.Underwear?.CreateSave(),
                jacket = body.Chest.Jacket?.CreateSave(),
                backpack = body.Chest.Backpack?.CreateSave(),
                vest = body.Chest.Vest?.CreateSave(),
                boots = body.LeftLeg.Boots?.CreateSave(),
                pants = body.LeftLeg.Pants?.CreateSave()
            };
        }

        public void Restore(CharacterSave save)
        {
            body = save.manBody;
            body.RestoreBodyParts();

            foreach (var bodyPart in body.BodyParts)
                ((BodyPathWearableClothes) bodyPart).RestoreClothesDict();
            
            Wear(save.hat);
            Wear(save.underwear);
            Wear(save.jacket);
            Wear(save.backpack);
            Wear(save.vest);
            Wear(save.boots);
            Wear(save.pants);

            void Wear(ClothesSave clothesSave)
            {
                if (clothesSave == null)
                    return;
                var clothesPref = Resources.Load<Clothes>(clothesSave.itemSave.resourcesPath);
                var clothes = Instantiate(clothesPref);
                clothes.Restore(clothesSave);
                body.Wear(clothes);
            }
        }
    }

    [DataContract(Namespace = "Model.Entities.Characters")]
    public class CharacterSave
    {
        [DataMember] public string resourcesPath;
        [DataMember] public ManBody manBody;

        [DataMember] public ClothesSave hat;
        [DataMember] public ClothesSave underwear;
        [DataMember] public ClothesSave jacket;
        [DataMember] public ClothesSave backpack;
        [DataMember] public ClothesSave vest;
        [DataMember] public ClothesSave boots;
        [DataMember] public ClothesSave pants;
    }
}