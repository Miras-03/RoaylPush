using EnemySpace.Attack;
using PlayerSpace;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class EnemyRotate : MonoBehaviour
{
    private Transform target;
    private Animator anim;

    private bool isBreathing = false;
    private const int breathingWaitTime = 1;

    [Inject]
    public void Constructor(Player player) => target = player.transform;

    private void Awake() => anim = GetComponent<Animator>();

    private IEnumerator Start()
    {
        while (true)
        {
            while (!anim.GetBool(nameof(UppercutAttack)) && !anim.GetBool(nameof(CrashAttack)))
            {
                transform.LookAt(target);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(breathingWaitTime);
        }
    }

    public bool IsBreathing { get => isBreathing; set => isBreathing = value; }
}