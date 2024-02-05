using UnityEngine;

public abstract class Rotate : MonoBehaviour
{
    protected Transform target;

    protected abstract void RotateToward();
}