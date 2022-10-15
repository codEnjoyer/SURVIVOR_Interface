
using Assets.Scripts.Model.Player.Body;

namespace Assets.Scripts.Model.Player.Entity.Realization
{
    public class Character : Entity
    {
        public readonly ManBody Body;
        public readonly Skills skills;
        public int Mobility;//Скорость передвижения на глобальной карте
    }
}