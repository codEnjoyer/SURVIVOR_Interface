using System.Collections.Generic;
using System.Linq;
using Model.GameEntity;

namespace Model.Entities
{
    public class SimpleRat: Entity
    {
        private Body body;
        public override Body Body => body;
        
        private void Awake()
        {
            body = new Body(new[] {new BodyPart(100, 10)});
        }
        
    }
}