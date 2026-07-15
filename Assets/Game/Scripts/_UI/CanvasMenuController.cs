using Game.Scripts._UI.Interfaces;
using UnityEngine;

namespace Game.Scripts._UI
{
    public class CanvasMenuController : MonoBehaviour, ICanvas
    {
        [Header("References")] 
        [SerializeField] private GameObject _exitGroup;
        [SerializeField] private GameObject _settingsGroup;
        [SerializeField] private GameObject _donateGroup;
        [SerializeField] private GameObject _panelStart;
        
        public void Show()
        {
            _panelStart.SetActive(true);
            _exitGroup.SetActive(false);
            _donateGroup.SetActive(true);
            _settingsGroup.SetActive(true);
        }

        public void Hide()
        {
            _panelStart.SetActive(false);
            _exitGroup.SetActive(true);
            _donateGroup.SetActive(false);
            _settingsGroup.SetActive(false);
        }
    }
}
