namespace Model.GameEntity.Skills
{
    public interface IDeveloping
    {
        public int CurrentLevel { get; }
        public void Development();
    }
}