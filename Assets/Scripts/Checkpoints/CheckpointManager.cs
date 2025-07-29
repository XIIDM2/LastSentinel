using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private AssetReferenceGameObject playerPrefab;

    private Vector2 currentSpawnpoint;

    private GameObject currentPlayerInstance;

    private bool loadedFromAddressables = false; // Œ¡ﬂ«¿“≈À‹ÕŒ œŒ‘» —“»“‹, —ƒ≈À¿“‹ —œ¿¬Õ ◊≈–≈« ADDRESSABLES

    private void Start()
    {
        currentPlayerInstance = FindFirstObjectByType<PlayerController>().gameObject;

        if (checkpoints != null && checkpoints.Length > 0)
        {
            currentSpawnpoint = checkpoints[0].GetSpawnPointPosition();
        }
        Debug.Log(loadedFromAddressables);
    }

    private void OnEnable()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.PlayerEnteredCheckpoint += SetSpawnPoint;
        }
    }

    private void OnDisable()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.PlayerEnteredCheckpoint -= SetSpawnPoint;
        }
    }

    public void SpawnPlayerAtPoint()
    {
        if (currentPlayerInstance != null)
        {
            if (!loadedFromAddressables)
            {
                Destroy(currentPlayerInstance);
            }
            else
            {
                Addressables.ReleaseInstance(currentPlayerInstance);
            }

        }

        Addressables.InstantiateAsync(playerPrefab, currentSpawnpoint, Quaternion.identity).Completed += OnPlayerSpawned;
        loadedFromAddressables = true;
    }

    private void OnPlayerSpawned(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentPlayerInstance = handle.Result;
        }
        else
        {
            Debug.LogError("Failed to Instantiate PlayerGameObject from Addressables");
        }
    }

    private void SetSpawnPoint(Vector2 spawnpoint)
    {
        currentSpawnpoint = spawnpoint;
    }
}
