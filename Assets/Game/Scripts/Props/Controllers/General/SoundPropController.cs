using Game.Scripts.Props.Interfaces;
using UnityEngine;

namespace Game.Scripts.Props.Controllers.General
{
    public class SoundPropController : MonoBehaviour, IPropSound
    {
        [Header("References")] 
        [SerializeField] private AudioSource _source;

        private void Awake()
        {
            _source.Stop();
        }

        public void Play()
        {
            _source.Play();
        }
    }
}