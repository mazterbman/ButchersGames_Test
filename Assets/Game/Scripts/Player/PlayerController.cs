using System;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private PlayerColliderController _colliderController;
        [SerializeField] private PlayerTouchActionController _touchActionController;
        [SerializeField] private PlayerMoveController _moveController;
        
        private void Start()
        {
            StopControlPlayer();
            _touchActionController.OnMove += _moveController.UpdatePosition;
        }

        private void OnDestroy()
        {
            _touchActionController.OnMove -= _moveController.UpdatePosition;
        }
        
        private void OnDisable()
        {
            StopControlPlayer();
        }

        public void StartControlPlayer()
        {
            _touchActionController.CanMove = true;
            _colliderController.ColliderEnable = true;
        }

        public void StopControlPlayer()
        {
            _touchActionController.CanMove = false;
            _colliderController.ColliderEnable = false;
        }
    }
}