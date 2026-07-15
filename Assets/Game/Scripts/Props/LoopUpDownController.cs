using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Props
{
    public class LoopUpDownController : MonoBehaviour, IDisposable
    {
        [Header("Settings")] 
        [SerializeField] [Range(0,10)] private float _timeTransform = 5;
        [SerializeField] [Range(0, 5)] private float _powerTransform = 2;
        [SerializeField] private AnimationCurve _curveTransform;

        [Space] 
        [SerializeField] private KindTransform _kindTransform;
        [SerializeField] private bool _transformOnStart;

        private Vector3 _startPosition;
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _startPosition = transform.localPosition;
            
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
            if (!_transformOnStart)
                return;

            StartLoopTransformAsync(_cancellationTokenSource.Token).Forget();
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

        private async UniTask StartLoopTransformAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var timeTransform = _timeTransform / 4.0f;
                
                await TransformAsync(timeTransform, cancellationToken);
                await TransformAsync(timeTransform, cancellationToken);
            }
        }

        private async UniTask TransformAsync(float timeTransform, CancellationToken cancellationToken)
        {
            var endPosition = _startPosition + Vector3.up * (_kindTransform == KindTransform.UpDown ? 1 : -1) * _powerTransform;
            _kindTransform = _kindTransform == KindTransform.UpDown ? KindTransform.DownUp : KindTransform.UpDown;
            await TransformMethodAsync(_startPosition, endPosition, timeTransform, cancellationToken);
            await TransformMethodAsync(endPosition, _startPosition, timeTransform, cancellationToken);
        }

        private async UniTask TransformMethodAsync(Vector3 startPosition, Vector3 endPosition, float endTime, CancellationToken cancellationToken)
        {
            var timePast = 0.0f;
            while (timePast < endTime)
            {
                timePast += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(startPosition, endPosition,
                    _curveTransform.Evaluate(timePast / endTime));
                await UniTask.Yield(cancellationToken);
            }
            transform.localPosition = endPosition;
        }

        private enum KindTransform
        {
            UpDown = 0,
            DownUp
        }
    }
}
