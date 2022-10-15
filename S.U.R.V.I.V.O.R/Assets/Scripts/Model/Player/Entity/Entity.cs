using Assets.Scripts.Model.Items.Weapons;

namespace Assets.Scripts.Model.Player.Entity
{
    public abstract class Entity
    {
        public readonly Body.Body Body;
        private Weapon primaryGun;
        private Weapon secondaryGun;
        private Weapon meleeWeapon;
        private Weapon chosenWeapon;
        public void Attack(Weapon weapon, Body.Body target)
        {
            //Стрельба из gun по target вызывает у gun метод стрельбы, который формирует служебный класс Shoot
        }

    }
}