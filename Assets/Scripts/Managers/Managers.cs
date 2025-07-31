using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance
        { 
        get 
        { 
            return _instance; 
        } 
    }

    public static ImpactEffectManager ImpactEffecmanager { get; private set; }

    private static Managers _instance;

    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        ImpactEffecmanager = GetComponentInChildren<ImpactEffectManager>();

        DontDestroyOnLoad(gameObject.transform.root.gameObject);
    }
}
