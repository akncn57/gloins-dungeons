using System;
using Enemies;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    [SerializeField] private Collider2D enemyGameObject;
    [SerializeField] private Collider2D playerGameObject;
    [SerializeField] private LayerMask layerMask;
    
    private EnemyLineOfSight _enemyLineOfSight;

    private void Start()
    {
        _enemyLineOfSight = new EnemyLineOfSight();
    }

    private void FixedUpdate()
    {
        // _enemyLineOfSight.HasLineOfSight(
        //     enemyGameObject,
        //     playerGameObject.transform.position + (Vector3)playerGameObject.offset,
        //     "Player",
        //     layerMask);
    }
}
