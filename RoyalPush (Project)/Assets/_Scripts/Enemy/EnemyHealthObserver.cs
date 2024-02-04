using HealthSpace;
using TMPro;
using UnityEngine;

namespace EnemySpace
{
    public sealed class EnemyHealthObserver : IHealthable
    {
        private TextMeshProUGUI hpIndicator;

        public EnemyHealthObserver(TextMeshProUGUI hpIndicator) => this.hpIndicator = hpIndicator;

        public void TakeDamage(int takeValue) => ShowHP(takeValue);

        public void Die() => Debug.Log("Die!");

        private void ShowHP(int hp) => hpIndicator.text = $"HP: {hp}";
    }
}