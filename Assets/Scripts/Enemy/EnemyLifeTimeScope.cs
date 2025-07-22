using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentOn<SpriteRenderer>(gameObject);
        builder.RegisterComponentOn<Animator>(gameObject);
        builder.RegisterComponentOn<Rigidbody2D>(gameObject);
        builder.RegisterComponentOn<EnemyAnimation>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);
    }  
}
