using Model.GameEntity;

namespace Model.Entities.Rats.SimpleRat
{
    public class RatBody : Body
    {
        public RatChest Chest { get; private set; }

        public RatBody()
        {
            Chest = new RatChest();
            AddBodyPart(Chest, 1);
            MaxCriticalLoses = BodyParts.Count;
        }
    }
}