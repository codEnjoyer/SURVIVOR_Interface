using global::System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public interface IMedicament 
    {
        public void UseTo(Health target)
        {
            //TODO метод должен убирать у тела свойства из списка Possible... и добавлять свойства из списка Additional...
        }
    }
} 