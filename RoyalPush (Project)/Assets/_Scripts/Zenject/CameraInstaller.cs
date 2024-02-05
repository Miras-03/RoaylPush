using Zenject;

public class CameraInstaller : MonoInstaller
{
    public override void InstallBindings() => Container.Bind<CameraFollow>().FromComponentInHierarchy().AsSingle();
}