using System.Runtime.Serialization;
using Model.SaveSystem;
using UnityEngine;

namespace Model.Entities.Characters.CharacterSkills
{
    public class Skills : MonoBehaviour, ISaved<SkillsSave>
    {
        [field: SerializeField] public Strength Strength { get; private set; }

        public SkillsSave CreateSave()
        {
            return new SkillsSave()
            {
                strength = Strength.CreateSave()
            };
        }

        public void Restore(SkillsSave save)
        {
            Strength.Restore(save.strength);
        }
    }


    [DataContract]
    public class SkillsSave
    {
        [DataMember] public SkillSave strength;
    }
}