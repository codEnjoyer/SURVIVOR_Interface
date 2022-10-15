using global::System;
using global::System.Collections.Generic;

namespace Assets.Scripts.Model{
    public class ManBody: Body
    {
        public readonly ManHead Head = new();
        public readonly ManChest Chest = new();
        public readonly ManStomach Stomach = new();
        public readonly ManArm LeftArm = new();
        public readonly ManArm RightArm = new();
        public readonly ManLeg LeftLeg = new();
        public readonly ManLeg RightLeg = new();

        public ManBody()
        {
            BodyParts = new List<BodyPart>{Head, Chest, Stomach, LeftArm, RightArm, LeftLeg, RightLeg};
        }

        public readonly int MaxEnergy = 10;
        public readonly int MaxHunger = 10;
        public readonly int MaxWater = 10;
        public float Endurance => throw new NotImplementedException();

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

        public event Action? PlayerTired;
        public event Action? PlayerHungry;
        public event Action? PlayerThirsty;
    }
}