using Assets.Scripts.Model.Items;

namespace Assets.Scripts.Model
{
    public abstract class Location
    {
        public Item GetLoot()
        {
            return default;
        }
    }
}