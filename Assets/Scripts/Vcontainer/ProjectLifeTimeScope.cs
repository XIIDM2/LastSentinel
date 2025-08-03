using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterDataCollection _charactersDataCollection;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_charactersDataCollection);
        builder.Register<Factory>(Lifetime.Singleton);
    }
}
