using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.Items;
using Model.SaveSystem;
using UnityEngine;

[assembly: ContractNamespace("", ClrNamespace = "Contoso.CRM")]
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
        private Skills skills;
        public MeleeWeapon MeleeWeapon { get; set; }
        public event Action<GunType> OnGunsChanged;
        protected override void Awake()
        {
            base.Awake();
            skills = new Skills(this);
        }

        public ManBody ManBody => (ManBody) Body;
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
            return new CharacterSave()
            {
                resourcesPath = GetComponent<Saved>().ResourcesPath,
                manBody = (ManBodySave) ManBody.CreateSave(),
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
            ManBody.Restore(save.manBody);
            skills.Restore(save.skills);
            
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
                ManBody.Wear(clothes);
            }
        }
    }

    [DataContract]
    public class CharacterSave
    {
        [DataMember] public string resourcesPath;
        [DataMember] public ManBodySave manBody;
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