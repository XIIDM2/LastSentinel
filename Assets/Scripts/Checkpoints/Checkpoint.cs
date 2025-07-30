using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public event UnityAction<Vector2> _PlayerEnteredCheckpoint;

    [SerializeField] private Transform _spawnPointPosition;

    private int _playerLayerIndex;

    private void Start()
    {
        _playerLayerIndex = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _playerLayerIndex)
        {
            _PlayerEnteredCheckpoint?.Invoke(new Vector2(_spawnPointPosition.position.x, _spawnPointPosition.position.y));
        }
    }

    public Vector2 GetSpawnPointPosition()
    {
        return new Vector2(_spawnPointPosition.position.x, _spawnPointPosition.position.y);
    }
}
