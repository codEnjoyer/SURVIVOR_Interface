using System.Runtime.Serialization;
using Model.SaveSystem;
using UnityEngine;

namespace Model.Entities.Characters.CharacterSkills
{
    public class Skills : ISaved<SkillsData>
    {
        public Strength Strength { get; private set; }

        public Skills(Character character)
        {
            Strength = new Strength(character.Body);
        }
        

        public SkillsData CreateData()
        {
            return new SkillsData()
            {
                strength = Strength.CreateData()
            };
        }

        public void Restore(SkillsData data)
        {
            Strength.Restore(data.strength);
        }
    }


    [DataContract]
    public class SkillsData
    {
        [DataMember] public SkillData strength;
    }
}