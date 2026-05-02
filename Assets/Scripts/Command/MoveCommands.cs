using UnityEngine;

namespace CommandPattern
{
    public class MoveCommand : ICommand
    {
        private readonly PlayerController _player;
        private readonly Vector3 _direction;

        public MoveCommand(PlayerController player, Vector3 direction)
        {
            _player    = player;
            _direction = direction;
        }

        public bool Execute() => _player.TryMove(_direction);

        public void Undo() => _player.TryMove(-_direction);
    }
}