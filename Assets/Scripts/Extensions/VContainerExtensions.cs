using UnityEngine;
using VContainer;
using VContainer.Unity;

public static class VContainerExtensions
{
    public static void RegisterComponentOn<T>(this IContainerBuilder builder, GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError($"Component of type {typeof(T).Name} not found on GameObject {gameObject.name}");
        }

        builder.RegisterComponent(component).As<T>();
    }
}
