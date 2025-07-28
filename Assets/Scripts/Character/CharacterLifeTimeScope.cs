using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CharacterLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterData enemyData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(enemyData);
    }  
}


