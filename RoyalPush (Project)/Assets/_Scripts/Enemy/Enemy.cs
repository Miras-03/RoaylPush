using HealthSpace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnemySpace
{
    public sealed class Enemy : MonoBehaviour, IDieable
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private ParticleSystem punchParticle;
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
            health.AddHPObserver(new HealthObserver(health, hpBar, hpText));
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

        public void TakeDamage(int damage)
        {
            health.TakeHealth -= damage;
            punchParticle.Play();
        }

        public void ExecuteDeath()
        {
            Destroy(hpBar.gameObject);
            Destroy(hpText.gameObject);
            PushDown(true);
        }

        public Health Health => health;
    }
}