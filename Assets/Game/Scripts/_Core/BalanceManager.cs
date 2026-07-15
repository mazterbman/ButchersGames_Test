using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts._Core
{
    public class BalanceManager : MonoBehaviour
    {
        // TODO можно потом использовать DI(Zenject/VContainer)
        public static BalanceManager Instance { get; private set; }
        public UnityAction<int> OnChange;
        public UnityAction<float> OnChangeNormalized;
        
        [Header("Settings")]
        [SerializeField] private int _minBalance = 0;
        [SerializeField] private int _maxBalance = 150;

        [Header("Debug")]
        [SerializeField] [TextArea(2, 5)] private string _debugString;
        
        // TODO можно нчальное кол-во вывнести в ScriptableObject
        private int _currentBalance = 30;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(Instance.gameObject);
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
            OnChangeNormalized?.Invoke(Mathf.Clamp01((float)_currentBalance / (float)_maxBalance));
        }

        public int Balance => _currentBalance;
        public float BalanceNormalized => Mathf.Clamp01((float)_currentBalance / (float)_maxBalance);
    }
}
