using EnemySpace;
using Zenject;

public sealed class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Enemy>().FromComponentInHierarchy().AsSingle();
}