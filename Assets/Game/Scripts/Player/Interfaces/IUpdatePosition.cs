using UnityEngine;

namespace Game.Scripts.Player.Interfaces
{
    public interface IUpdatePosition
    {
        void UpdatePosition(Vector2 deltaMove);
    }
}