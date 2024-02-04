using EnemySpace;
using EnemySpace.Attack;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class PlayerRotate : MonoBehaviour
{
    private Transform target;

    [Inject]
    public void Constructor(Enemy enemy) => target = enemy.transform;

    private IEnumerator Start()
    {
        while (true)
        {
            transform.LookAt(target);
            yield return new WaitForFixedUpdate();
        }
    }
}