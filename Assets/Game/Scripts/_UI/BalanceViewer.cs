using Game.Scripts._Core;
using TMPro;
using UnityEngine;

namespace Game.Scripts._UI
{
    public class BalanceViewer : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TMP_Text _balance;
        
        private BalanceManager _balanceManager;

        private void Start()
        {
            _balanceManager = BalanceManager.Instance;
            
            if (_balanceManager)
            {
                _balanceManager.OnChange += ViewBalance;
                ViewBalance(_balanceManager.Balance);
            }
        }

        private void OnDestroy()
        {
            if (_balanceManager) _balanceManager.OnChange -= ViewBalance;
        }

        private void ViewBalance(int value)
        {
            _balance.text = value.ToString("F0");
        }
    }
}
