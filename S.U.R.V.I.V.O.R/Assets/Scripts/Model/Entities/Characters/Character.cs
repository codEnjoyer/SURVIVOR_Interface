using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.GameEntity.EntityHealth;
using Model.Items;
using Model.SaveSystem;
using UnityEngine;
using BodyPart = Model.GameEntity.BodyPart;

namespace Model.Entities.Characters
{
    [RequireComponent(typeof(Saved))]
    public class Character : Entity, ISaved<CharacterSave>
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string firstName;
        [SerializeField] private string surname;

        private Gun primaryGun;
        private Gun secondaryGun;
        public MeleeWeapon MeleeWeapon { get; set; }

        public readonly Skills skills;
        public ManBody ManBody => (ManBody) Body;

        public event Action<GunType> OnGunsChanged;


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
            ManBody.Energy += food.Data.DeltaEnergy;
            ManBody.Water += food.Data.DeltaWater;
            ManBody.Hunger += food.Data.DeltaHunger;
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

        public CharacterSave CreateSave()
        {
            ManBody.Health.AddProperty(new Poisoning());
            return new CharacterSave()
            {
                resourcesPath = GetComponent<Saved>().ResourcesPath,
                skills = skills.CreateSave(),
                hat = ManBody.Head.Hat?.CreateSave(),
                underwear = ManBody.Chest.Underwear?.CreateSave(),
                jacket = ManBody.Chest.Jacket?.CreateSave(),
                backpack = ManBody.Chest.Backpack?.CreateSave(),
                vest = ManBody.Chest.Vest?.CreateSave(),
                boots = ManBody.LeftLeg.Boots?.CreateSave(),
                pants = ManBody.LeftLeg.Pants?.CreateSave()
            };
        }

        public void Restore(CharacterSave save)
        {
            Wear(save.hat);
            Wear(save.underwear);
            Wear(save.jacket);
            Wear(save.backpack);
            Wear(save.vest);
            Wear(save.boots);
            Wear(save.pants);

            skills.Restore(save.skills);

            void Wear(ClothesSave clothesSave)
            {
                if (clothesSave == null)
                    return;
                var clothesPref = Resources.Load<Clothes>(clothesSave.itemSave.resourcesPath);
                var clothes = Instantiate(clothesPref);
                clothes.Restore(clothesSave);
                ManBody.Wear(clothes);
            }
        }
    }

    [DataContract(Namespace = "Model.Entities.Characters")]
    public class CharacterSave
    {
        [DataMember] public string resourcesPath;
        [DataMember] public SkillsSave skills;

        [DataMember] public ClothesSave hat;
        [DataMember] public ClothesSave underwear;
        [DataMember] public ClothesSave jacket;
        [DataMember] public ClothesSave backpack;
        [DataMember] public ClothesSave vest;
        [DataMember] public ClothesSave boots;
        [DataMember] public ClothesSave pants;
    }
}