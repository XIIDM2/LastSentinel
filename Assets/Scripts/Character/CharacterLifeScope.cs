using UnityEngine;
using VContainer;
using VContainer.Unity;

public abstract class CharacterLifeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentOn<SpriteRenderer>(gameObject);
        builder.RegisterComponentOn<Rigidbody2D>(gameObject);
        builder.RegisterComponentOn<Animator>(gameObject);
        builder.RegisterComponentOn<ChangeVisualDirection>(gameObject);

        ConfigureCharacterSpecifics(builder);
    }

    protected abstract void ConfigureCharacterSpecifics(IContainerBuilder builder);
}