using UnityEngine;

// Патрон не наследуется от класса предмета, патроны будут хранится в коробках, которые будут отображаться в инвентаре
[CreateAssetMenu(fileName = "New Caliber", menuName = "Bullet Data", order = 53)]
public class Bullet: ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float extraFireDistance;
    [SerializeField] private float armorPenetrating;
    [SerializeField] private float boneBreakingChance;
    [SerializeField] private float bleedingChance;
    [SerializeField] private float recoil;
    [SerializeField] private float extraAccuracy;
    [SerializeField] private float noise;
    
    public float Damage => damage;
    public float ExtraFireDistance => extraFireDistance;
    public float ArmorPenetrating => armorPenetrating;
    public float BoneBreakingChance => boneBreakingChance;
    public float BleedingChance => bleedingChance;
    public float Recoil => recoil;
    public float ExtraAccuracy => extraAccuracy;
    public float Noise => noise;
}
