using UnityEngine;

[CreateAssetMenu(fileName = "New GunData", menuName = "Data/Gun Data", order = 50)]
public class GunData : ScriptableObject
{
    [SerializeField] private int fireRate;
    [SerializeField] private float accuracy;
    [SerializeField] private float extraDamage;
    [SerializeField] private float fireDistance;
    [SerializeField] private float ergonomics; //Чем выше, тем больше негативное влияние на Mobility класса персонажа
    [SerializeField] private SingleAmmo ammoType;
    [SerializeField] private Caliber caliber;
    [SerializeField] private SpecialGunCellType gunCellType;
    
    public int FireRate => fireRate;
    public float Accuracy => accuracy;
    public float ExtraDamage => extraDamage;
    public float FireDistance => fireDistance;
    public float Ergonomics => ergonomics; 
    public SingleAmmo AmmoType => ammoType;
    public Caliber Caliber => caliber;
    public SpecialGunCellType GunCellType => gunCellType;
}