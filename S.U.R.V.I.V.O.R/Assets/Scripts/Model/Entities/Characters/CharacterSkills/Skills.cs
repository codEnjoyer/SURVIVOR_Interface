using System.Runtime.Serialization;
using Model.SaveSystem;
using UnityEngine;

namespace Model.Entities.Characters.CharacterSkills
{
    public class Skills : ISaved<SkillsSave>
    {
        public Strength Strength { get; private set; }

        public Skills(Character character)
        {
            Strength = new Strength(character.Body);
        }
        

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