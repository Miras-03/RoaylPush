using HealthSpace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnemySpace
{
    public sealed class Enemy : MonoBehaviour, IDieable
    {
        [SerializeField] private Slider hpBar;
        private Health health;
        private Animator anim;
        private List<Rigidbody> bones;

        [SerializeField] private int maxHP = 300;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            bones = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            health = new Health(maxHP);
        }

        private void Start() => PushDown(false);

        private void OnEnable()
        {
            health.AddHPObserver(new HealthObserver(health, hpBar));
            health.AddDeathObserver(this);
        }

        private void OnDestroy() => health.RemoveDeathObserver(this);

        public void SetHealth(int takeValue) => health.TakeHealth = takeValue;

        private void PushDown(bool shouldFall)
        {
            anim.enabled = !shouldFall;
            foreach (var b in bones)
                b.isKinematic = !shouldFall;
        }

        public void TakeDamage(int damage) => health.TakeHealth -= damage;

        public void ExecuteDeath()
        {
            Destroy(hpBar.gameObject);
            PushDown(true);
        }

        public Health Health => health;
    }
}