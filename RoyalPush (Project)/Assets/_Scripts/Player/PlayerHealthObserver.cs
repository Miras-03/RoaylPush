using HealthSpace;
using TMPro;
using UnityEngine;

namespace PlayerSpace
{
    public sealed class PlayerHealthObserver : IHealthable
    {
        private TextMeshProUGUI hpIndicator;

        public PlayerHealthObserver(TextMeshProUGUI hpIndicator) => this.hpIndicator = hpIndicator;

        public void TakeDamage(int takeValue) => ShowHP(takeValue);

        public void Die() => Debug.Log("Die!");

        private void ShowHP(int hp) => hpIndicator.text = $"HP: {hp}";
    }
}