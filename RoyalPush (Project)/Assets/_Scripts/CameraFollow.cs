using UnityEngine;

public sealed class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    [SerializeField] private Vector3 locationOffset;
    private const float smoothSpeed = 0.125f;
    private const float xOffset = 45;

    private void FixedUpdate()
    {
        ChangePosition();
        ChangeRotation();
    }

    private void ChangePosition()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void ChangeRotation()
    {
        Quaternion desiredrotation = target.rotation * Quaternion.Euler(xOffset, target.rotation.y, 0);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;
    }
}