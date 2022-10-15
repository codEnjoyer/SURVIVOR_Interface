using global::System;

namespace Assets.Scripts.Model{
    public class Character : Entity
    {
        public readonly Skills Skills;

        public readonly ManBody Body;
        public int Mobility => throw new NotImplementedException(); //Скорость передвижения на глобальной карте

        public Character()
        {
            Body = new ManBody();
        }
    }
}