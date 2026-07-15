using Game.Scripts._Core;
using Game.Scripts.Props.Interfaces;
using UnityEngine;

namespace Game.Scripts.Props.Controllers.General
{
    public class BalancePropController : MonoBehaviour, IPropBalance
    {
        [Header("Settings")]
        // TODO Можно также вынести в ScriptableObject
        [SerializeField] [Range(-100, 100)] private int _coastInteract; 
        
        private BalanceManager _balanceManager;

        private void Start()
        {
            _balanceManager = BalanceManager.Instance;
        }

        public void ChangeBalance()
        {
            if (!_balanceManager)
                return;
            
            _balanceManager.ChangeBalance(_coastInteract);
        }
    }
}
