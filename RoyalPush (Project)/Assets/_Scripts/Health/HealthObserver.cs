using TMPro;
using UnityEngine.UI;

namespace HealthSpace
{
    public sealed class HealthObserver : IHealthable
    {
        private Slider hpBar;
        private TextMeshProUGUI hpText;

        private readonly int maxHP;

        public HealthObserver(Health health, Slider hpBar, TextMeshProUGUI hpText)
        {
            this.hpBar = hpBar;
            this.hpText = hpText;
            maxHP = health.MaxHP;
            this.hpBar.value = maxHP;
            UpdateHPBar(maxHP);
        }

        public void TakeDamage(int takeValue) => UpdateHPBar(takeValue);

        private void UpdateHPBar(float currentHP)
        {
            hpBar.value = currentHP / maxHP;
            hpText.text = currentHP.ToString();
        }
    }
}