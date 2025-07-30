using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;
    [SerializeField] private AssetReferenceGameObject _playerPrefab;

    private Vector2 _currentSpawnpoint;

    private GameObject _currentPlayerInstance;

    private bool _loadedFromAddressables = false; // Œ¡ﬂ«¿“≈À‹ÕŒ œŒ‘» —“»“‹, —ƒ≈À¿“‹ —œ¿¬Õ ◊≈–≈« ADDRESSABLES

    private void Start()
    {
        _currentPlayerInstance = FindFirstObjectByType<PlayerController>().gameObject;

        if (_checkpoints != null && _checkpoints.Length > 0)
        {
            _currentSpawnpoint = _checkpoints[0].GetSpawnPointPosition();
        }
    }

    private void OnEnable()
    {
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            checkpoint._PlayerEnteredCheckpoint += SetSpawnPoint;
        }
    }

    private void OnDisable()
    {
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            checkpoint._PlayerEnteredCheckpoint -= SetSpawnPoint;
        }
    }

    public void SpawnPlayerAtPoint()
    {
        if (_currentPlayerInstance != null)
        {
            if (!_loadedFromAddressables)
            {
                Destroy(_currentPlayerInstance);
            }
            else
            {
                Addressables.ReleaseInstance(_currentPlayerInstance);
            }

        }

        Addressables.InstantiateAsync(_playerPrefab, _currentSpawnpoint, Quaternion.identity).Completed += OnPlayerSpawned;
        _loadedFromAddressables = true;
    }

    private void OnPlayerSpawned(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _currentPlayerInstance = handle.Result;
        }
        else
        {
            Debug.LogError("Failed to Instantiate PlayerGameObject from Addressables");
        }
    }

    private void SetSpawnPoint(Vector2 spawnpoint)
    {
        _currentSpawnpoint = spawnpoint;
    }
}
