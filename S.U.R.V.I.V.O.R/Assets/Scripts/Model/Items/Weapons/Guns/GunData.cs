using UnityEngine;

[CreateAssetMenu(fileName = "New 1", menuName = "Data/1", order = 50)]
public class GunData: ScriptableObject
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

}