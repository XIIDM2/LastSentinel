using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private GameObject mainCameraPrefab;
    [SerializeField] private GameObject globalLight2DPrefab;

    [SerializeField] private AssetReferenceGameObject levelPrefab;
    [SerializeField] private AssetReferenceGameObject playerPrefab;

    private GameObject mainCamera;
    private GameObject globalLight2D;
    private GameObject level; 
    private GameObject player;

    private async void Awake()
    {
        await CreateObjects();
        PrepareLevel();
    }

    private void OnDestroy()
    {
        if (level != null) Addressables.ReleaseInstance(level);

        if (player != null) Addressables.ReleaseInstance(player);
    }

    private async UniTask CreateObjects()
    {
        mainCamera = Instantiate(mainCameraPrefab);
        globalLight2D = Instantiate(globalLight2DPrefab);

        level = await Addressables.InstantiateAsync(levelPrefab).ToUniTask();
        player = await Addressables.InstantiateAsync(playerPrefab).ToUniTask();


    }

    private void PrepareLevel()
    {
       player.transform.position = new Vector3(-9.0f, -2.4f, 0);
    }
}
