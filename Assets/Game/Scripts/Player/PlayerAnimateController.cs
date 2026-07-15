using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerAnimateController : MonoBehaviour
    {
        private static readonly int State = Animator.StringToHash("State");

        [Header("Reference")] 
        [SerializeField] private List<Animator> _animator;

        public void IdleAnimate()
        {
            _animator.ForEach(a => a.SetInteger(State, 0));
        }

        public void WalkAnimate()
        {
            _animator.ForEach(a => a.SetInteger(State, 1));
        }

        public void WinAnimate()
        {
            _animator.ForEach(a => a.SetInteger(State, 3));
        }

        public void LooseAnimate()
        {
            _animator.ForEach(a => a.SetInteger(State, 2));
        }
    }
}