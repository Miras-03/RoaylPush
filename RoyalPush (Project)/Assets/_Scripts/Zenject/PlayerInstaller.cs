using Zenject;
using PlayerSpace;

public sealed class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
}