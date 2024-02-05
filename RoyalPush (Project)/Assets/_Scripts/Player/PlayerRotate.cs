using EnemySpace;
using Zenject;

public sealed class PlayerRotate : Rotate
{
    [Inject]
    public void Constructor(Enemy enemy) => target = enemy.transform;

    private void FixedUpdate() => RotateToward();

    protected override void RotateToward() => transform.LookAt(target);
}