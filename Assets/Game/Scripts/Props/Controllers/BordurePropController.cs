using Game.Scripts.Props.Interfaces;
using UnityEngine;

namespace Game.Scripts.Props.Controllers
{
    public class BordurePropController : MonoBehaviour, IProp
    {
        [Header("References")]
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _animator.enabled = false;
        }

        public void Interact()
        {
            _animator.enabled = true;
        }
    }
}
