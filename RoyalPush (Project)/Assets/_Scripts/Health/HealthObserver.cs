using UnityEngine.UI;

namespace HealthSpace
{
    public sealed class HealthObserver : IHealthable
    {
        private Slider hpBar;

        private readonly int maxHP;

        public HealthObserver(Health health, Slider hpBar)
        {
            this.hpBar = hpBar;
            maxHP = health.MaxHP;
            this.hpBar.value = maxHP;
        }

        public void TakeDamage(int takeValue) => UpdateHPBar(takeValue);

        private void UpdateHPBar(float currentHP) => hpBar.value = currentHP / maxHP;
    }
}