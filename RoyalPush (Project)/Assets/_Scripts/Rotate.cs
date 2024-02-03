using System.Collections;
using UnityEngine;

public sealed class Rotate : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotateTime;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(rotateTime);
            transform.LookAt(target);
        }
    }
}