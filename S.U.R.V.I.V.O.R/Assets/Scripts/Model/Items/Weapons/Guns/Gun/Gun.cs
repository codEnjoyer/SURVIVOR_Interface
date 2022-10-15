namespace Assets.Scripts.Model
{
    public abstract class Gun<TCaliber> : Weapon where TCaliber : Caliber
    {
        protected Magazine<TCaliber> CurrentMagazine;
        protected Caliber Chamber;
        /* 
         * После патронника в реализации абстракных классов будут идти слоты для GunModule.
         * У каждого оружия будет свой допустимый набор возможных GunModules
         */
        protected int FireRate;
        protected float Accuracy;
        protected float ExtraDamage;
        protected float FireDistance;
        protected float Ergonomics;//Чем выше, тем больше негативное влияние на Mobility класса персонажа

        public Shoot GiveShoot()
        {
            //TODO реализовать метод который возвращает служебный класс выстрела
            return default;
        }
    }
}
