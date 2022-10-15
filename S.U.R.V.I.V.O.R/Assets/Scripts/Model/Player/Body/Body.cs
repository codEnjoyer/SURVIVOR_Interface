using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Model.ServiceClasses;

namespace Assets.Scripts.Model.Player.Body
{
    public abstract class Body
    {
        protected readonly List<BodyPart.BodyPart> bodyPaths;

        public float TotalHP => bodyPaths.Sum(path => path.Hp);
        public float TotalWeight => bodyPaths.Sum(path => path.)

        public readonly Health.Health health;

        public void GetDamage(Shoot shoot)//Этот метод будет переопределяться в классе наследнике. Он должен распределить урон от класса Shoot по частям тела
        {

        }
    }


    public class ManBody
    {
        public readonly ManHead Head = new();
        public readonly ManChest Chest = new();
        public readonly ManStomach Stomach = new();
        public readonly ManLeg LeftLeg = new();
        public readonly ManLeg RightLeg = new();
        public readonly ManArm LeftArm = new();
        public readonly ManArm RightArm = new();

        public int MaxEnergy => 10;
        public int MaxHunger => 10;
        public int MaxWater => 10;

        private int energy;
        public int Energy
        {
            get => energy;
            set
            {
                if (value <= 0)
                {
                    energy = 0;
                    PlayerTired?.Invoke();
                }
                else if (value > MaxEnergy)
                    energy = MaxEnergy;
                else
                    energy = value;
            }
        }
        private int food;
        public int Food
        {
            get => food;
            set
            {
                if (value <= 0)
                {
                    food = 0;
                    PlayerHungry?.Invoke();
                }
                else if (value > MaxHunger)
                    food = MaxHunger;
                else
                    food = value;
            }
        }
        private int water;
        public int Water
        {
            get => water;
            set
            {
                if (value <= 0)
                {
                    water = 0;
                    PlayerThirsty?.Invoke();
                }
                else if (value > MaxWater)
                    water = MaxWater;
                else
                    water = value;
            }
        }
        public event Action? PlayerDied;
        public event Action? PlayerTired;
        public event Action? PlayerHungry;
        public event Action? PlayerThirsty;


        public float endurance;//Длина хода по глобальной карте, исходя из веса
        public float extraWeight;//Написать геттер и сеттер, который будет считать вес из всех частей тела

    }
}