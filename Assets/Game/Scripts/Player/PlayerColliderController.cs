using Game.Scripts.Props.Controllers;
using Game.Scripts.Props.Interfaces;
using UnityEngine;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerColliderController : MonoBehaviour
    {
        private CapsuleCollider _capsuleCollider;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }
        
        public bool ColliderEnable 
        {
            get => _capsuleCollider.enabled;
            set => _capsuleCollider.enabled = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.parent.TryGetComponent<IProp>(out var prop))
                return;
            
            prop.Interact();
        }
    }
}