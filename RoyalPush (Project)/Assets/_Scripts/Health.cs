using System.Collections.Generic;

namespace HealthSpace
{
    public sealed class Health
    {
        private List<IHealthable> observers = new List<IHealthable>();

        private int health = 0;

        public Health(int maxHP = 100) => health = maxHP;

        public int TakeHealth
        {
            get => health;
            set
            {
                if (health > -1)
                {
                    health = value;
                    NotifyToTakeDamage();
                }
                else
                {
                    health = 0;
                    NotifyToDie();
                }
            }
        }

        public void Add(IHealthable healthable) => observers.Add(healthable);

        public void Remove(IHealthable healthable) => observers.Remove(healthable);

        public void Clear() => observers.Clear();

        public void NotifyToTakeDamage()
        {
            foreach (var observer in observers)
                observer.TakeDamage(health);
        }

        public void NotifyToDie()
        {
            foreach (var observer in observers)
                observer.Die();
        }
    }
}