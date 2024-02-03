using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemySpace;

namespace EnemySpace.Attack
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        private Animator anim;
        private Enemy enemy;
        private PunchAttack punchAttack;
        private CrashAttack crashAttack;
        private RangedAttack rangedAttack;
        private Dictionary<int, IAttackAbility> attackabilities = new Dictionary<int, IAttackAbility>();

        [SerializeField] private int respiringTime = 5;
        [SerializeField] private int powerGatherTime = 5;
        private bool isRespiring = false;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemy = GetComponent<Enemy>();

            punchAttack = new PunchAttack(anim);
            crashAttack = new CrashAttack(anim);
            rangedAttack = new RangedAttack(anim);
        }

        private IEnumerator Start()
        {
            SetRandAttackAbility();
            while (true)
            {
                while (!isRespiring)
                {
                    yield return new WaitForSeconds(powerGatherTime);
                    isRespiring = true;
                }
                yield return new WaitForSeconds(respiringTime);
                SetRandAttackAbility();
                isRespiring = false;
            }
        }

        private void OnEnable()
        {
            attackabilities.Add(0, punchAttack);
            attackabilities.Add(1, crashAttack);
            attackabilities.Add(2, rangedAttack);
        }

        private void OnDestroy() => attackabilities.Clear();

        private void SetRandAttackAbility()
        {
            int index = Random.Range(0, attackabilities.Count);
            enemy.SetAttackAbility(attackabilities[index]);
        }
    }
}