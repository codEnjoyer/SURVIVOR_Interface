using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.SaveSystem;
using Unity.VisualScripting;
using UnityEngine;

[assembly: ContractNamespace("", ClrNamespace = "Contoso.CRM")]
namespace Model.Entities.Characters
{
    [RequireComponent(typeof(Saved))]
    public class Character : Entity, ISaved<CharacterData>
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string firstName;
        [SerializeField] private string surname;

        private Gun primaryGun;
        private Gun secondaryGun;
        private Skills skills;
        public MeleeWeapon MeleeWeapon { get; set; }

        private IWeapon chosedWeapon;
        public event Action<GunType> OnGunsChanged;
        protected override void Awake()
        {
            base.Awake();
            skills = new Skills(this);
            var weapon  =  Instantiate(Resources.Load<GameObject>("Items/Guns/Ak-74/Ak-74"), transform);
            weapon.transform.localPosition = Vector3.zero;
            chosedWeapon = weapon.GetComponent<IWeapon>();
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
            Destroy(food.gameObject);
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

        public override void Attack(Vector3 targetPoint)
        {
            chosedWeapon.Attack(targetPoint,skills);
            
        }

        public CharacterData CreateData()
        {
            return new CharacterData()
            {
                resourcesPath = GetComponent<Saved>().ResourcesPath,
                manBody = (ManBodyData) ManBody.CreateData(),
                skills = skills.CreateData(),
                hat = ManBody.Head.Hat?.CreateData(),
                underwear = ManBody.Chest.Underwear?.CreateData(),
                jacket = ManBody.Chest.Jacket?.CreateData(),
                backpack = ManBody.Chest.Backpack?.CreateData(),
                vest = ManBody.Chest.Vest?.CreateData(),
                boots = ManBody.LeftLeg.Boots?.CreateData(),
                pants = ManBody.LeftLeg.Pants?.CreateData(),
                localPosition = transform.localPosition
            };
        }

        public void Restore(CharacterData data)
        {
            ManBody.Restore(data.manBody);
            skills.Restore(data.skills);
            
            Wear(data.hat);
            Wear(data.underwear);
            Wear(data.jacket);
            Wear(data.backpack);
            Wear(data.vest);
            Wear(data.boots);
            Wear(data.pants);

            transform.localPosition = data.localPosition;

            void Wear(ClothesData clothesSave)
            {
                if (clothesSave == null)
                    return;
                var clothesPref = Resources.Load<Clothes>(clothesSave.itemData.resourcesPath);
                var clothes = Instantiate(clothesPref);
                clothes.Restore(clothesSave);
                ManBody.Wear(clothes);
            }
        }
    }

    [DataContract]
    public class CharacterData
    {
        [DataMember] public string resourcesPath;
        [DataMember] public ManBodyData manBody;
        [DataMember] public SkillsData skills;

        [DataMember] public ClothesData hat;
        [DataMember] public ClothesData underwear;
        [DataMember] public ClothesData jacket;
        [DataMember] public ClothesData backpack;
        [DataMember] public ClothesData vest;
        [DataMember] public ClothesData boots;
        [DataMember] public ClothesData pants;

        [DataMember] public Vector3 localPosition;


        public Character Prefab => Resources.Load<Character>(resourcesPath);
    }
}