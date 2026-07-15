using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts._Core
{
    public class BalanceManager : MonoBehaviour
    {
        // TODO можно потом использовать DI(Zenject/VContainer)
        public static BalanceManager Instance { get; private set; }
        public UnityAction<int> OnChange;

        [Header("Debug")]
        [SerializeField] [TextArea(2, 5)] private string _debugString;
        
        // TODO можно нчальное кол-во вывнести в ScriptableObject
        private int _currentBalance = 30;

        private static readonly int _minBalance = 0;
        private static readonly int _maxBalance = 100;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _debugString = $"Balance = {_currentBalance}";
            OnChange += b => _debugString = $"Balance = {b}";
        }

        public void ChangeBalance(int money)
        {
            var balanceNew= Mathf.Clamp(_currentBalance + money, _minBalance, _maxBalance);
            if (balanceNew == _currentBalance)
                return;

            _currentBalance = balanceNew;
            OnChange?.Invoke(_currentBalance);
        }

        public int Balance => _currentBalance;
    }
}
