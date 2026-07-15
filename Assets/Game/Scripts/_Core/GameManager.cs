using System.Collections.Generic;
using Game.Scripts._UI;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts._Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [Header("References")] 
        [SerializeField] private CanvasInterfaceController _canvasInterface;
        [SerializeField] private CanvasMenuController _canvasMenu;

        [Space] 
        [SerializeField] private CanvasController _looseController;
        [SerializeField] private CanvasController _winController;

        [Space] 
        [SerializeField] private Button _buttonStart;
        [SerializeField] private List<Button> _buttonsReset;

        [Space] 
        
        [SerializeField] private PlayerController _playerController;

        private BalanceManager _balanceManager;
        private bool _isResetting = false;

        private void Awake()
        {
            if (Instance && Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _balanceManager = BalanceManager.Instance;
            _balanceManager.OnChange += CheckBalanceGame;
            
            _canvasInterface.Hide();
            _canvasMenu.Show();
            _winController.Hide();
            _looseController.Hide();

            _buttonStart.interactable = true;
            _buttonStart.onClick.AddListener(StartGame);
            
            _buttonsReset.ForEach(b => b.interactable = false);
            _buttonsReset.ForEach(b => b.onClick.AddListener(ResetGame));
        }

        private void OnDestroy()
        {
            _balanceManager.OnChange -= CheckBalanceGame;
            
            _buttonStart.onClick.RemoveListener(StartGame);
            _buttonsReset.ForEach(b => b.onClick.RemoveListener(ResetGame));
            _playerController.StopControlPlayer();
        }

        public void WinGame()
        {
            _playerController.Win();
            _looseController.Hide();
            _winController.Show();
            _buttonsReset.ForEach(b => b.interactable = true);
        }
        
        private void LooseGame()
        {
            _playerController.Loose();
            _looseController.Show();
            _winController.Hide();
            _buttonsReset.ForEach(b => b.interactable = true);
        }

        private void ResetGame()
        {
            if (_isResetting)
                return;
            
            _isResetting = true;
            _buttonsReset.ForEach(b => b.interactable = false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void StartGame()
        {
            _buttonStart.interactable = false;
            
            _canvasInterface.Show();
            _canvasMenu.Hide();
            
            _playerController.StartControlPlayer();
        }

        private void CheckBalanceGame(int balance)
        {
            if (balance > 0)
                return;
            
            LooseGame();
        }
    }
}
