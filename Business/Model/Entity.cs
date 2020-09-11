using System;

namespace Business.Model
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
