﻿using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyFindClosestChasePointCommand : ICommand
    {
        private readonly EnemyFindClosestChasePoint _enemyFindClosestChasePoint;
        private readonly Vector3 _position;
        private readonly Vector3 _playerPosition;
        private readonly float _offset;
        
        public EnemyFindClosestChasePointCommand(EnemyFindClosestChasePoint enemyFindClosestChasePoint, Vector3 position, Vector3 playerPosition, float offset)
        {
            _enemyFindClosestChasePoint = enemyFindClosestChasePoint;
            _position = position;
            _playerPosition = playerPosition;
            _offset = offset;
        }
        
        public void Execute()
        {
            _enemyFindClosestChasePoint.GetClosestChasePoint(_position, _playerPosition, _offset);
        }

        public T Execute<T>()
        {
            return default;
        }

        public void Undo(){}
    }
}