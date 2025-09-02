using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public UnityAction<Health> OnPlayerSpawned;

    [SerializeField] private Checkpoint[] _checkpoints;

    private const string _playerID = "Player";

    [Inject] private readonly Factory _factory;

    [Inject] private CinemachineCamera _cinemachineCamera;

    private Vector2 _currentSpawnpoint;

    private GameObject _currentPlayerInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
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
        SpawnPlayerAtPointAsync().Forget();
    }

    public async UniTask SpawnPlayerAtPointAsync()
    {
        if (_currentPlayerInstance == null)
        {
            _currentPlayerInstance = FindFirstObjectByType<PlayerController>().gameObject;
        }

        if (_currentPlayerInstance != null)
        {
            _factory.ReleaseCharacterInstance(_currentPlayerInstance);
        }

        _currentPlayerInstance = await _factory.CreateCharacter(_playerID);

        if (_currentPlayerInstance != null)
        {
            _currentPlayerInstance.transform.SetPositionAndRotation(_currentSpawnpoint, Quaternion.identity);

            _cinemachineCamera.Follow = _currentPlayerInstance.transform;

            OnPlayerSpawned?.Invoke(_currentPlayerInstance.GetComponent<Health>());
        }
    }



    private void SetSpawnPoint(Vector2 spawnpoint)
    {
        _currentSpawnpoint = spawnpoint;
    }
}
