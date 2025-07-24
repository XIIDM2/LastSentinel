using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifeTimeScope : CharacterLifeScope
{
    [SerializeField] private CharacterData enemyData;
    protected override void ConfigureCharacterSpecifics(IContainerBuilder builder)
    {
        builder.RegisterInstance(enemyData);
        builder.RegisterComponentFromChildOn<EnemyDetection>(gameObject);
        builder.RegisterComponentOn<EnemyAnimation>(gameObject);
        builder.RegisterComponentOn<EnemyAttack>(gameObject);
        builder.RegisterComponentOn<EnemyMovement>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);    
        builder.RegisterComponentOn<EnemyBehaviour>(gameObject);
  

    }  
}


