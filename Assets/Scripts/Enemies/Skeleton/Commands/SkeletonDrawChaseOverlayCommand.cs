using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Skeleton.Commands
{
    public class SkeletonDrawChaseOverlayCommand : ICommand
    {
        private readonly SkeletonDrawChaseOverlay _skeletonDrawChaseOverlay;
        private readonly Vector3 _position;
        private readonly float _radius;
        private readonly SkeletonStateMachine _skeletonStateMachine;
        
        public SkeletonDrawChaseOverlayCommand(SkeletonDrawChaseOverlay skeletonDrawChaseOverlay, Vector3 position, float radius, SkeletonStateMachine skeletonStateMachine)
        {
            _skeletonDrawChaseOverlay = skeletonDrawChaseOverlay;
            _position = position;
            _radius = radius;
            _skeletonStateMachine = skeletonStateMachine;
        }
        
        public void Execute()
        {
            _skeletonDrawChaseOverlay.DrawChaseOverlay(_position, _radius, _skeletonStateMachine);
        }

        public void Undo(){}
    }
}