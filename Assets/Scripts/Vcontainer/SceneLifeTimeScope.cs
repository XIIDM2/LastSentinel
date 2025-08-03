using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifeTimeScope : LifetimeScope
{
    [SerializeField] private CinemachineCamera _camera;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_camera);
    }
}
