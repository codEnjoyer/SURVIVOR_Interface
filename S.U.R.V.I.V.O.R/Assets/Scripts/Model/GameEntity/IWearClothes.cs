using System.Collections.Generic;

namespace Model.GameEntity
{
    public interface IWearClothes
    {
        public bool Wear(Clothes clothesToWear);
        public Clothes UnWear(ClothType clothType);
        
        public IEnumerable<Clothes> GetClothes();
        
    }
}