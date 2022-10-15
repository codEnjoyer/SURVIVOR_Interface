namespace Assets.Scripts.Model{
    public abstract class MedRemedy : Item
    {
        // можно создать интерфейс IMedRemedy в котором будет метод Use(BodyPart target)
        // пример реализации:
        // interface IMedRemedy
        // {
        //     public void Use(BodyPart target);
        // }
        
        // class Bandage: Item, IMedRemedy
        // {
        //     public void Use(BodyPart target)
        //     {
        //         // TODO убрать кровотечение
        //         throw new NotImplementedException();
        //     }
        // }
        
        private bool IsHealingBleeding;
        private bool IsHealingBroke;
        private float ExtraHp;

        public void Use(BodyPart target)
        {
            //TODO у target добавить хп и убрать свойсвтва кровотечения и перелома в зависимости от бул свойств
        }
    }
}