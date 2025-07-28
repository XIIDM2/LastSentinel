using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifeTimeScope : LifetimeScope
{
    [SerializeField] private CharacterData playerData;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(playerData);

        builder.RegisterComponentOn<PlayerInputController>(gameObject);
        builder.RegisterComponentOn<PlayerAttack>(gameObject);
        builder.RegisterComponentOn<PlayerMovement>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);
    }

}
