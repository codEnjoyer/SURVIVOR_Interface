using UnityEngine;

public class Gun: Weapon
{
    [SerializeField] private int fireRate;
    [SerializeField] private float accuracy;
    [SerializeField] private float extraDamage;
    [SerializeField] private float fireDistance;
    [SerializeField] private float ergonomics; //Чем выше, тем больше негативное влияние на Mobility класса персонажа
    [SerializeField] private Magazine currentMagazine;
    [SerializeField] private Bullet chamber;
    [SerializeField] private GunModule gunModule;
    
    public int FireRate => fireRate;
    public float Accuracy => accuracy;
    public float ExtraDamage => extraDamage;
    public float FireDistance => fireDistance;
    public float Ergonomics => ergonomics; 
    
    /* 
     * После патронника в реализации абстракных классов будут идти слоты для GunModule.
     * У каждого оружия будет свой допустимый набор возможных GunModules
     */

    public Shoot GiveShoot()
    {
        //TODO реализовать метод который возвращает служебный класс выстрела
        return default;
    }
}

