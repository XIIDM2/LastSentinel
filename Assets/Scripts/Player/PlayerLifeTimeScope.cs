using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentOn<SpriteRenderer>(gameObject);
        builder.RegisterComponentOn<Rigidbody2D>(gameObject);
        builder.RegisterComponentOn<Animator>(gameObject);
        builder.RegisterComponentOn<PlayerInputController>(gameObject);
        builder.RegisterComponentOn<PlayerAttack>(gameObject);
        builder.RegisterComponentOn<PlayerMovement>(gameObject);
        builder.RegisterComponentOn<PlayerAnimation>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);
        builder.RegisterComponentOn<FlipSprite>(gameObject);


    }
}
