using TMPro;
using UnityEngine;
using VContainer;

public enum EnemyState
{ 
    Idle,
    RunToTarget,
    AttackTarget,
    Jump
}

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float stopDistance = 0.5f;

    private EnemyDetection enemyDetection;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyAnimation enemyAnimation;

    [Inject]
    private void Construct(EnemyDetection enemyDetection, EnemyMovement enemyMovement, EnemyAttack enemyAttack, EnemyAnimation enemyAnimation)
    {
        this.enemyDetection = enemyDetection;
        this.enemyMovement = enemyMovement;
        this.enemyAttack = enemyAttack;
        this.enemyAnimation = enemyAnimation;
    }
}
