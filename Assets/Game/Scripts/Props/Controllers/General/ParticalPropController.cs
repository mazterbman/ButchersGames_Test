using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Props.Interfaces;
using UnityEngine;

namespace Game.Scripts.Props.Controllers.General
{
    public class ParticalPropController : MonoBehaviour, IPropPartical
    {
        [Header("References")] 
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private CancellationTokenSource _tokenSource;
        private bool _isPlayed = false;

        private void Awake()
        {
            try
            {
                _tokenSource?.Dispose();
            }
            catch
            {
                // ignored
            }
            _tokenSource = new CancellationTokenSource();
            _particleSystem.Stop();
        }

        private void OnDestroy()
        {
            try
            {
                _tokenSource?.Cancel();
                _tokenSource?.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        public void StartPartical()
        {
            if (_isPlayed)
                return;

            _isPlayed = true;
            PlayParticalAsync(_tokenSource.Token).Forget();
        }
        
        private async UniTask PlayParticalAsync(CancellationToken cancellationToken)
        {
            _meshRenderer.gameObject.SetActive(false);
            _particleSystem.Play();
            await UniTask.WaitWhile(_particleSystem.IsAlive, cancellationToken: cancellationToken);
        }
    }
}