using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunData", menuName = "Data/Gun Data", order = 50)]
public sealed class GunData : ScriptableObject
{
    [SerializeField] private float fireRate;
    [SerializeField] private float accuracy;
    [SerializeField] private float extraDamage;
    [SerializeField] private float fireDistance;
    [SerializeField] private float ergonomics; //Чем выше, тем больше негативное влияние на Mobility класса персонажа
    [SerializeField] private Caliber caliber;
    [SerializeField] private GunType gunType;
    [SerializeField] private List<GunModuleType> availableGunModules;

    public GunData(float fireRate, float accuracy, float extraDamage, float fireDistance,
        float ergonomics, Caliber caliber, GunType gunType,
        IEnumerable<GunModuleType> availableGunModules)
    {
        this.fireRate = fireRate;
        this.accuracy = accuracy;
        this.extraDamage = extraDamage;
        this.fireDistance = fireDistance;
        this.ergonomics = ergonomics;
        this.caliber = caliber;
        this.gunType = gunType;
        this.availableGunModules = availableGunModules.ToList();
    }

    public float FireRate => fireRate;
    public float Accuracy => accuracy;
    public float ExtraDamage => extraDamage;
    public float FireDistance => fireDistance;
    public float Ergonomics => ergonomics;
    public Caliber Caliber => caliber;
    public GunType GunType => gunType;
    public IReadOnlyCollection<GunModuleType> AvailableGunModules => availableGunModules;
}