using Game.Scripts._Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts._UI
{
    public class BalanceViewImage : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Image _balance;
        
        private BalanceManager _balanceManager;

        private void Start()
        {
            _balanceManager = BalanceManager.Instance;
            
            if (_balanceManager)
            {
                _balanceManager.OnChangeNormalized += ViewBalance;
                ViewBalance(_balanceManager.BalanceNormalized);
            }
        }

        private void OnDestroy()
        {
            if (_balanceManager) _balanceManager.OnChangeNormalized -= ViewBalance;
        }

        private void ViewBalance(float value)
        {
            _balance.fillAmount = Mathf.Clamp01(value);
        }
    }
}