using System;
using Graph_and_Map;

namespace Player
{
    public interface IPlayer 
    {
        public int MaxHp { get; }
        public int Hp { get; set; }
        public int MaxEnergy { get; }
        public int Energy { get; set; }
        public int MaxSatiety { get; }
        public int Satiety { get; set; }
        public int MaxWater { get; }
        public int Water { get; set; }
        public Node Position { get;}

        public event Action PlayerDied;
        public event Action PlayerTired;
        public event Action PlayerHungry;
        public event Action PlayerThirsty;
        public void Move(Node target);
    }
}
