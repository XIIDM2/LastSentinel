using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

public class CharactersSpawner : MonoBehaviour
{
    [System.Serializable]
    private class SpawnPoint
    { 
        [SerializeField] private CharactersID _characterID;
        [SerializeField] private Transform _spawnPosition;

        public CharactersID CharacterID => _characterID;
        public Vector2 SpawnPosition => new Vector2(_spawnPosition.position.x, _spawnPosition.position.y);
    }

    [SerializeField] private SpawnPoint[] _spawnPoints;

    [Inject] private readonly Factory _factory;
    [Inject] private CinemachineCamera _cinemachineCamera;

    private async void Start()
    {
        List<UniTask> spawnTasks = new();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            spawnTasks.Add(SpawnAndLocateCharacter(_spawnPoints[i].CharacterID.ToString(), _spawnPoints[i].SpawnPosition));
        }

        await UniTask.WhenAll(spawnTasks);
    }

    private async UniTask SpawnAndLocateCharacter(string ID, Vector2 spawnPosition)
    {
        GameObject _character = await _factory.CreateCharacter(ID);

        if (_character != null)
        {
            _character.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

            if (ID == CharactersID.Player.ToString())
            {
                _cinemachineCamera.Follow = _character.gameObject.transform;
            }
        }        
    }
}
