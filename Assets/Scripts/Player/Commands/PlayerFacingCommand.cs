using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerFacingCommand : ICommand
    {
        private readonly GameObject _parentObject;
        private readonly float _horizontalMove;
        
        public PlayerFacingCommand(GameObject parentObject, float horizontalMove)
        {
            _parentObject = parentObject;
            _horizontalMove = horizontalMove;
        }

        public void Execute()
        {
            _parentObject.transform.localScale = _horizontalMove switch
            {
                > 0 => new Vector3(1f, 1f, 1f),
                < 0 => new Vector3(-1f, 1f, 1f),
                _ => _parentObject.transform.localScale
            };
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}