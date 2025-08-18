using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private GameObject _mainCameraPrefab;
    [SerializeField] private GameObject _globalLight2DPrefab;

    [SerializeField] private AssetReferenceGameObject _levelPrefab;
    [SerializeField] private AssetReferenceGameObject _playerPrefab;

    private GameObject _mainCamera;
    private GameObject _globalLight2D;
    private GameObject _level; 
    private GameObject _player;

    private async void Awake()
    {
        await CreateObjects();
        PrepareLevel();
    }

    private void OnDestroy()
    {
        if (_level != null) Addressables.ReleaseInstance(_level);

        if (_player != null) Addressables.ReleaseInstance(_player);
    }

    private async UniTask CreateObjects()
    {
        _mainCamera = Instantiate(_mainCameraPrefab);
        _globalLight2D = Instantiate(_globalLight2DPrefab);

        _level = await Addressables.InstantiateAsync(_levelPrefab).ToUniTask();
        _player = await Addressables.InstantiateAsync(_playerPrefab).ToUniTask();
    }

    private void PrepareLevel()
    {
       _player.transform.position = new Vector3(-9.0f, -2.4f, 0);
    }

}
