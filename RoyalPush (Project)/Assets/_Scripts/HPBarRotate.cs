using Zenject;

public sealed class HPBarRotate : Rotate
{
    [Inject]
    public void Constructor(CameraFollow camera) => target = camera.transform;

    private void FixedUpdate() => RotateToward();

    protected override void RotateToward() => transform.LookAt(-target.position);
}