using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifeTimeScope : CharacterLifeScope
{
    [SerializeField] private CharacterData enemyData;
    protected override void ConfigureCharacterSpecifics(IContainerBuilder builder)
    {
        builder.RegisterInstance(enemyData);
        builder.RegisterComponentOn<EnemyAnimation>(gameObject);
        builder.RegisterComponentOn<EnemyAttack>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);    
  

    }  
}


