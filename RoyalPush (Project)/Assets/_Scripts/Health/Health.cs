using System.Collections.Generic;

namespace HealthSpace
{
    public sealed class Health
    {
        private List<IHealthable> healthObservers = new List<IHealthable>();
        private List<IDieable> deathObservers = new List<IDieable>();

        private int maxHP;
        private int health;

        public Health(int maxHP = 100)
        {
            this.maxHP = maxHP;
            health = maxHP;
            NotifyToTakeDamage();
        }

        public void AddHPObserver(IHealthable healthable) => healthObservers.Add(healthable);
        public void RemoveHPObserver(IHealthable healthable) => healthObservers.Remove(healthable);
        public void ClearHPObservers() => healthObservers.Clear();

        public void AddDeathObserver(IDieable diable) => deathObservers.Add(diable);
        public void RemoveDeathObserver(IDieable diable) => deathObservers.Remove(diable);
        public void ClearDeathObservers() => deathObservers.Clear();

        public void NotifyToTakeDamage()
        {
            foreach (var observer in healthObservers)
                observer.TakeDamage(health);
        }

        public void NotifyToDie()
        {
            foreach (var observer in deathObservers)
                observer.ExecuteDeath();
        }

        public int TakeHealth
        {
            get => health;
            set
            {
                health = value;
                if (health > -1)
                    NotifyToTakeDamage();
                else
                {
                    health = 0;
                    NotifyToDie();
                }
            }
        }

        public int MaxHP => maxHP;
    }
}