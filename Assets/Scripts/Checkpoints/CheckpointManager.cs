using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;

    private const string _playerID = "Player";

    [Inject] private readonly Factory _factory;

    private Vector2 _currentSpawnpoint;

    private GameObject _currentPlayerInstance;

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

    public async UniTask SpawnPlayerAtPoint()
    {
        if (_currentPlayerInstance != null)
        {
            _factory.ReleaseCharacterInstance(_currentPlayerInstance);
        }

        _currentPlayerInstance = await _factory.CreateCharacter(_playerID);

        if (_currentPlayerInstance != null)
        {
            _currentPlayerInstance.transform.SetPositionAndRotation(_currentSpawnpoint, Quaternion.identity);
        }
    }

    private void SetSpawnPoint(Vector2 spawnpoint)
    {
        _currentSpawnpoint = spawnpoint;
    }
}
