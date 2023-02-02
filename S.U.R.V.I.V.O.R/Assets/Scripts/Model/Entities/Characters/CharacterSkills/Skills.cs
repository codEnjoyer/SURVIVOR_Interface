using System.Runtime.Serialization;
using Model.SaveSystem;

namespace Model.Entities.Characters.CharacterSkills
{
    public class Skills: ISaved<SkillsSave>
    {
        public readonly Strength strength;

        public Skills(Character character)
        {
            strength = new Strength(character.body);
        }

        public SkillsSave CreateSave()
        {
            return new SkillsSave()
            {
                strength = strength.CreateSave()
            };
        }

        public void Restore(SkillsSave save)
        {
            strength.Restore(save.strength);
        }
    }


    [DataContract]
    public class SkillsSave
    {
        [DataMember] public SkillSave strength;
    }
}