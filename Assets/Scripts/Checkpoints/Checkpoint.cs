using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public event UnityAction<Vector2> PlayerEnteredCheckpoint;

    [SerializeField] private Transform spawnPointPosition;

    private int playerLayerIndex;

    private void Start()
    {
        playerLayerIndex = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerIndex)
        {
            PlayerEnteredCheckpoint?.Invoke(new Vector2(spawnPointPosition.position.x, spawnPointPosition.position.y));
        }
    }

    public Vector2 GetSpawnPointPosition()
    {
        return new Vector2(spawnPointPosition.position.x, spawnPointPosition.position.y);
    }
}
