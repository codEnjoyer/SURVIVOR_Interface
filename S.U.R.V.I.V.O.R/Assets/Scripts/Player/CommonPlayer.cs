using System;
using Graph_and_Map;
using UnityEngine;
namespace Player
{
    public class CommonPlayer: IPlayer
    {
        public int MaxHp => 100;
        private int hp;
        public int Hp
        {
            get => hp;
            set
            {
                if (value <= 0)
                {
                    hp = 0;
                    PlayerDied?.Invoke();
                }
                else if (value > MaxHp)
                    hp = MaxHp;
                else
                    hp = value;
            }
        }
        public int MaxEnergy => 10;
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
        public int MaxSatiety => 100;
        private int satiety;
        public int Satiety
        {
            get => satiety;
            set
            {
                if (value <= 0)
                {
                    satiety = 0;
                    PlayerHungry?.Invoke();
                }
                else if (value > MaxSatiety)
                    satiety = MaxSatiety;
                else
                    satiety = value;
            }
        }
        public int MaxWater => 100;
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
        private Node position;
        public Node Position => position;
        public event Action PlayerDied;
        public event Action PlayerTired;
        public event Action PlayerHungry;
        public event Action PlayerThirsty;

        public void Move(Node target)
        {
            
        }

    }
}