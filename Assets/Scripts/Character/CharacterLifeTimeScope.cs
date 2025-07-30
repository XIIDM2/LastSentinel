using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CharacterLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterData _enemyData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_enemyData);
    }  
}


