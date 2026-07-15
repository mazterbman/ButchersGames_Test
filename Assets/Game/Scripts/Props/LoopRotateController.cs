using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Props
{
    public class LoopRotateController : MonoBehaviour, IDisposable
    {
        [Header("Settings")] 
        [SerializeField] [Range(0, 10)] private float _speedRotate = 2;
        [SerializeField] private AnimationCurve _curveRotation;

        [Space] 
        [SerializeField] private KindRotate _kindRotate;
        [SerializeField] private bool _rotateOnStart;
        
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            try
            {
                _cancellationTokenSource?.Dispose();
            }
            catch
            {
                // ignored
            }

            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            if (!_rotateOnStart)
                return;

            StartLoopRotateAsync(_cancellationTokenSource.Token).Forget();
        }

        private void OnDestroy() => Dispose();

        public void Dispose()
        {
            try
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        private async UniTask StartLoopRotateAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                transform.Rotate(Vector3.up * _speedRotate * Time.deltaTime * (_kindRotate == KindRotate.Left ? -1 : 1));
                await UniTask.Yield(cancellationToken);
            }
        }

        private enum KindRotate
        {
            Left = 0,
            Right
        }
    }
}
