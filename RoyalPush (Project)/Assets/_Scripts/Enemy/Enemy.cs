using HealthSpace;
using PlayerSpace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EnemySpace
{
    public sealed class Enemy : MonoBehaviour, IDieable
    {
        [SerializeField] private Slider hpBar;
        private Health health;
        private Animator anim;
        private List<Rigidbody> bones;

        [SerializeField] private int maxHP = 100;

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
            health.TakeHealth -= 10;
        }

        public void SetHealth(int takeValue) => health.TakeHealth = takeValue;

        public void ExecuteDeath()
        {
            Destroy(hpBar.gameObject);
            PushDown(true);
        }

        private void PushDown(bool shouldFall)
        {
            anim.enabled = !shouldFall;
            foreach (var b in bones)
                b.isKinematic = !shouldFall;
        }

        public Health Health => health;
    }
}