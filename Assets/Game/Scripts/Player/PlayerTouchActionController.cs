using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class PlayerTouchActionController : MonoBehaviour
    {
        public UnityAction<Vector2> OnMove;
        
        [Header("References")] 
        [SerializeField] private InputActionReference _actionReference;

        [Header("Debug")] 
        [SerializeField] [TextArea(2,4)] private string _debugString;
        
        private void Start()
        {
            _actionReference.action.performed += StartTracking;
        }

        private void OnDestroy()
        {
            _actionReference.action.started -= StartTracking;
        }

        public bool CanMove { get; set; }

        private void StartTracking(InputAction.CallbackContext callbackContext)
        {
            if (!CanMove)
                return;
            
            var deltaTouch = _actionReference.action.ReadValue<Vector2>().normalized;
            _debugString = $"Tracking ={deltaTouch}";
            OnMove?.Invoke(deltaTouch);
        }
    }
}
