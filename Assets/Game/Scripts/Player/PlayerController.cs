using System;
using Game.Scripts._Core;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private PlayerColliderController _colliderController;
        [SerializeField] private PlayerTouchActionController _touchActionController;
        [SerializeField] private PlayerMoveController _moveController;
        [SerializeField] private PlayerModelsController _modelsController;
        [SerializeField] private PlayerAnimateController _animateController;
        [SerializeField] private SplineAnimate _splineAnimate;
        
        private void Start()
        {
            StopControlPlayer();
            _animateController.IdleAnimate();
            _touchActionController.OnMove += _moveController.UpdatePosition;
            _splineAnimate.Restart(false);
        }

        private void FixedUpdate()
        {
            if (!_splineAnimate.IsPlaying)
                return;
            
            if (_splineAnimate.NormalizedTime <= 0.990f)
                return;
            
            GameManager.Instance?.WinGame();
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
            _animateController.WalkAnimate();
            _splineAnimate.Play();
        }

        public void StopControlPlayer()
        {
            _touchActionController.CanMove = false;
            _colliderController.ColliderEnable = false;
            _splineAnimate.Pause();
        }

        public void Loose()
        {
            StopControlPlayer();
            _animateController.LooseAnimate();
        }
        
        public void Win()
        {
            StopControlPlayer();
            _animateController.WinAnimate();
        }
    }
}