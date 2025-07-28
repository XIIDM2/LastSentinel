using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterData enemyData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(enemyData);

        builder.RegisterComponentFromChildOn<EnemyDetection>(gameObject);
        builder.RegisterComponentFromChildOn<EnemyAnimation>(gameObject);
        builder.RegisterComponentOn<EnemyAttack>(gameObject);
        builder.RegisterComponentOn<EnemyMovement>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);    
        builder.RegisterComponentOn<EnemyBehaviour>(gameObject);
  

    }  
}


