using System.Collections.Generic;

namespace Model.GameEntity
{
    public interface IWearClothes
    {
        public void WearOrUnWear(Clothes clothToWear, bool shouldUnWear, out bool isSuccessful);
        public IEnumerable<Clothes> GetClothes();
    }
}