namespace Assets.Scripts.Model
{
    public class Ak47: Gun<Caliber_762>
    {
        private Magazine<Caliber_762> magazine1 = new(30);
        public Ak47()
        {
            FireRate = 1;
            CurrentMagazine = magazine1;
            //TODO реализовать характеристики 
        }
    }
}