using System;
using Game.Scripts._UI;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.Splines;
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
        [SerializeField] private Button _buttonStart;

        [Space] 
        [SerializeField] private SplineAnimate _splineAnimate;
        [SerializeField] private PlayerController _playerController;

        private void Awake()
        {
            if (Instance && Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _canvasInterface.Hide();
            _canvasMenu.Show();
            
            _splineAnimate.Restart(false);

            _buttonStart.interactable = true;
            _buttonStart.onClick.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(StartGame);
            _playerController.StopControlPlayer();
        }

        public void LooseGame()
        {
            _playerController.StopControlPlayer();
        }

        public void WinGame()
        {
            _playerController.StopControlPlayer();
        }

        private void StartGame()
        {
            _buttonStart.interactable = false;
            
            _canvasInterface.Show();
            _canvasMenu.Hide();
            
            _splineAnimate.Play();
            _playerController.StartControlPlayer();
        }
    }
}
