using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifeTimeScope : CharacterLifeScope
{
    [SerializeField] private CharacterData playerData;

    protected override void ConfigureCharacterSpecifics(IContainerBuilder builder)
    {
        builder.RegisterInstance(playerData);
        builder.RegisterComponentOn<PlayerInputController>(gameObject);
        builder.RegisterComponentOn<PlayerAttack>(gameObject);
        builder.RegisterComponentOn<PlayerMovement>(gameObject);
        builder.RegisterComponentOn<PlayerAnimation>(gameObject);
        builder.RegisterComponentOn<Health>(gameObject);
    }

}
