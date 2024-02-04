using Zenject;

public sealed class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<PlayerSpace.Player>().FromComponentInHierarchy().AsSingle();
}