using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CharacterLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterData _characterData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_characterData);
    }  
}


