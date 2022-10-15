using global::System;

namespace Assets.Scripts.Model
{
    public abstract class Entity
    {
        public Body Body { get; protected set; }
        protected Weapon PrimaryGun;
        protected Weapon SecondaryGun;
        protected Weapon MeleeWeapon;
        protected Weapon ChosenWeapon;
        public void Attack(Weapon weapon, Body target)
        {
            //TODO Реализовать метод стрельбы из weapon по target вызывает у gun метод стрельбы, который формирует служебный класс Shoot
            throw new NotImplementedException();
        }

    }
}