using System;
using System.Collections.Generic;

namespace Model.GameEntity.Skills
{
    public class Strength : Skill, IDrawableSkillLevel
    {
        private readonly Body body;

        class StrengthLevel : BaseLevel
        {
            public int additionalHp;
            public int additionalPortableWeight;
            public int additionalMeleeDamage;

            public StrengthLevel(int necessaryExperienceToLevelUp, int additionalHp, int additionalPortableWeight,
                int additionalMeleeDamage) : base(necessaryExperienceToLevelUp)
            {
                this.additionalHp = additionalHp;
                this.additionalPortableWeight = additionalPortableWeight;
                this.additionalMeleeDamage = additionalMeleeDamage;
            }
        }

        private readonly Dictionary<int, StrengthLevel> skillCharacteristic = new()
        {
            {1, new StrengthLevel(100, 10, 10, 10)}
            //TODO 
        };

        public Strength(Body body) : base(5)
        {
            this.body = body;
        }

        public override void AddExperience()
        {
            throw new NotImplementedException();
        }

        public string GetLevelInformation()
        {
            throw new NotImplementedException();
        }
    }
}