namespace Model.Entities.Characters.CharacterSkills
{
    public interface IDeveloping
    {
        public int CurrentLevel { get; }
        public void Development();
    }
}