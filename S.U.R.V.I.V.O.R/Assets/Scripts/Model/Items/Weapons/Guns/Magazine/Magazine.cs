using global::System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Magazine<TCaliber> : Item where TCaliber : Caliber
    {
        public Magazine(int capacity) 
        {
            Capacity = capacity;
        }

        protected Stack<TCaliber> AmmoStack;
        protected int Capacity; //TODO Нельзя загрузить больше патронов чем Capacity

        public bool IsEmpty => AmmoStack.Count == 0;

        public Caliber DeLoad()
        {
            return AmmoStack.Pop();
        }

        public void Reload(List<TCaliber> ammoList)
        {
            foreach (var ammo in ammoList)
                AmmoStack.Push(ammo);
        }
    }
}