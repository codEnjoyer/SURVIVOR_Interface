using System;
using Assets.Scripts.Model.Items.Cloth;
using Assets.Scripts.Model.ServiceClasses;

namespace Assets.Scripts.Model.Player.BodyPart
{
    public abstract class BodyPart
    {
        protected readonly int MaxHp;
        public float Hp { get; protected set; }

        private float size;
        public float Size
        {
            get => size;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException();
                size = value;
            }
        }

        protected readonly int MaxClothesAmount;//Максимальное количество одежды, которое можно надеть на часть тела
        public readonly Cloth[] Clothes;//Вся одежда, надетая на часть тела
        public event Action OnZeroHp;

        protected BodyPart()
        {
            Clothes = new Cloth[MaxClothesAmount];
        }


        public void TakeDamage(Shoot shot)
        {
            //TODO Умная формула Вани
            var blockedDamage = Clothes.Sum(cloth => cloth.CalculateBlockedDamage(shot));
            throw new NotImplementedException();
            if (Hp <= 0)
                OnZeroHp?.Invoke();
        }
    }
}