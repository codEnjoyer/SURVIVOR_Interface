namespace Model.GameEntity.Skills
{
    public class Skills
    {
        private readonly Strength strength;

        public Skills(Character character)
        {
            strength = new Strength(character.body);
        }
    }
}