using UnityEngine;
using Model.GameEntity;

public class FightCharacter : MonoBehaviour
{
    public Entity Entity { get; private set; }
    public CharacterType Type { get; private set; }

    public void ApplyProperties(Entity target, CharacterType type, bool alive = true)
    {
        Debug.Log("Apply");
        target.Body.Died += OnDied;
        Entity = target;
        Type = type;
        Alive = alive;
        Debug.Log(Entity.Body);
    }

    public float Initiative => Entity.Initiative;
    public int Energy => Entity.SpeedInFightScene;
    public bool Alive = true;
    public GameObject TargetToHit;
    public readonly float radius;
    
    // public void MakeShoot(GameObject targetObj, string targetName)
    // {
    //     transform.LookAt(targetObj.transform);
    //     //RangeWeapon.Shoot(gameObject, targetObj, "Head");
    // }
    //
    // public void MakeHit()
    // {
    //     transform.LookAt(TargetToHit.transform);
    //     //MeleeWeapon.Hit(TargetToHit);
    // }

    public void Attack()
    {
        transform.LookAt(TargetToHit.transform.position);
        var bodyParts = TargetToHit.GetComponent<FightCharacter>().Entity.Body.BodyParts;
        Entity.Attack(bodyParts, Vector3.Distance(gameObject.transform.position,
                                                             TargetToHit.transform.position));
    }

    public void OnDisable()
    {
        Debug.Log("Disable");
        Entity.Body.Died -= OnDied;
    }

    private void OnDied()
    {
        Debug.Log("I'm Died");
        FightSceneController.Instance.DeleteDeathCharacterFromQueue(this);
        Destroy(gameObject);
    }
}
