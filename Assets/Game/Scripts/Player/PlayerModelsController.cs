using Game.Scripts._Core;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerModelsController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private SkinnedMeshRenderer _poorModel;
        [SerializeField] private SkinnedMeshRenderer _normalModel;
        [SerializeField] private SkinnedMeshRenderer _reachModel;
        [SerializeField] private SkinnedMeshRenderer _millionModel;

        private BalanceManager _balanceManager;

        // Пока так
        private const float PoorBorder = 0.25f;
        private const float NormalBorder = 0.55f;
        private const float ReachBorder = 0.85f;

        private void Start()
        {
            if (!BalanceManager.Instance)
                return;
            
            _balanceManager = BalanceManager.Instance;
            _balanceManager.OnChangeNormalized += TrackBalance;
            TrackBalance(_balanceManager.BalanceNormalized);
        }

        private void OnDestroy()
        {
            if (!_balanceManager)
                return;
            
            _balanceManager.OnChangeNormalized -= TrackBalance;
        }

        private void TrackBalance(float balance)
        {
            OffAllModel();
            switch (balance)
            {
                case <= PoorBorder:
                    _poorModel.gameObject.SetActive(true);
                    break;
                
                case <= NormalBorder:
                    _normalModel.gameObject.SetActive(true);
                    break;
                
                case <= ReachBorder:
                    _reachModel.gameObject.SetActive(true);
                    break;
                
                default:
                    _millionModel.gameObject.SetActive(true);
                    break;
            }
        }

        private void OffAllModel()
        {
            _poorModel.gameObject.SetActive(false);
            _normalModel.gameObject.SetActive(false);
            _reachModel.gameObject.SetActive(false);
            _millionModel.gameObject.SetActive(false);
        }
    }
}
