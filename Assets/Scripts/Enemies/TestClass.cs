using System;
using Enemies;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private GameObject playerGameObject;
    
    private EnemyLineOfSight _enemyLineOfSight;

    private void Start()
    {
        _enemyLineOfSight = new EnemyLineOfSight();
    }

    private void FixedUpdate()
    {
        _enemyLineOfSight.HasLineOfSight(enemyGameObject.transform.position, playerGameObject.transform.position, "Player");
    }
}
