using Game.Scripts._UI.Interfaces;
using UnityEngine;

namespace Game.Scripts._UI
{
    public class CanvasInterfaceController : MonoBehaviour, ICanvas
    {
        [Header("References")] 
        [SerializeField] private GameObject _balanceGroup;
        [SerializeField] private GameObject _levelInfoGroup;
        
        public void Show()
        {
            _balanceGroup.SetActive(true);
            _levelInfoGroup.SetActive(true);
        }

        public void Hide()
        {
            _balanceGroup.SetActive(false);
            _levelInfoGroup.SetActive(false);
        }
    }
}