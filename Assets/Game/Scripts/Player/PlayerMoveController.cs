using Game.Scripts.Player.Interfaces;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerMoveController : MonoBehaviour, IUpdatePosition
    {
        [Header("References")] 
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;

        [Space] 
        [SerializeField] private Transform _playerTransform;

        [Header("Settings")] 
        [SerializeField] [Range(0, 100)] private float _speed = 10;

        public void UpdatePosition(Vector2 deltaMove)
        {
            // Работаем тольлко с x
            var positionNow = _playerTransform.localPosition;
            positionNow.x += deltaMove.x * Time.deltaTime * _speed;
            positionNow.x = Mathf.Clamp(positionNow.x, _leftBorder.localPosition.x, _rightBorder.localPosition.x);
            
            _playerTransform.localPosition = positionNow;
        }
    }
}