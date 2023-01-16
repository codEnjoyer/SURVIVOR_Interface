using System;
using System.Collections.Generic;
using System.Linq;
using Model.GameEntity;
using Model.GameEntity.Skills;
using UnityEngine;

public class Character : Entity
{
    public readonly ManBody body = new ();
    [SerializeField] private Sprite sprite; 
    [SerializeField] private string firstName;
    [SerializeField] private string surname;

    public Sprite Sprite => sprite;
    public string FirstName => firstName;
    public string Surname => surname;

    
    private Gun primaryGun;

    public Gun PrimaryGun
    {
        get => primaryGun;
        set
        {
            primaryGun = value;
            OnGunsChanged?.Invoke(GunType.PrimaryGun);
        }
    }
    
    private Gun secondaryGun;
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
    
    public MeleeWeapon MeleeWeapon { get; set; }
    public readonly Skills skills;

    public Character()
    {
        skills = new Skills(this);
    }

    public BaseItem Loot(LocationData infoAboutLocation)
    {
        //TODO Добавить опыт к навыку лутания в зависмости от редкости найденной вещи
        return infoAboutLocation.GetLoot();
    }
    
    public event Action<GunType> OnGunsChanged; 

    public override Body Body => body;
    public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте
    
    public override void Attack(IEnumerable<BodyPart> targets, float distance)
    {
        var damage = new DamageInfo(40f);
        targets.First().TakeDamage(damage);
    }
}