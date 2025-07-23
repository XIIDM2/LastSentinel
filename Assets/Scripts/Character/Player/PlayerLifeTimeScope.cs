using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterData PlayerData;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(PlayerData);
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
