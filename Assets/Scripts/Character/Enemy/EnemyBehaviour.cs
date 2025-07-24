using System.Collections;
using TMPro;
using UnityEngine;
using VContainer;
using static UnityEngine.GraphicsBuffer;

public enum EnemyState
{ 
    Idle,
    RunToTarget,
    AttackTarget,
    Jump
}

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float stopDistance = 1.0f;

    private float distanceToTarget;

    private EnemyDetection enemyDetection;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyAnimation enemyAnimation;
    private Health health;

    private CharacterData characterData;

    [Inject]
    private void Construct(CharacterData characterData, EnemyDetection enemyDetection, EnemyMovement enemyMovement, EnemyAttack enemyAttack, EnemyAnimation enemyAnimation, Health health)
    {
        this.health = health;
        this.enemyDetection = enemyDetection;
        this.enemyMovement = enemyMovement;
        this.enemyAttack = enemyAttack;
        this.enemyAnimation = enemyAnimation;
        this.characterData = characterData;
    }

    private void Awake()
    {
        enemyMovement.InitData(characterData);
        enemyAttack.InitData(characterData);
        health.InitData(characterData);
    }

    private void FixedUpdate()
    {
        if (enemyDetection.Target != null)
        {
            distanceToTarget = Vector2.Distance(transform.position, enemyDetection.Target.position);

            if (distanceToTarget > stopDistance)
            {
                enemyMovement.MoveToTarget(enemyDetection.Target);

            }
            else 
            {
                if (enemyMovement.isMoving)
                {
                    enemyMovement.StopMove();
                }

            }
           
        }
        else
        {
            enemyMovement.StopMove();
        }


    }


}
