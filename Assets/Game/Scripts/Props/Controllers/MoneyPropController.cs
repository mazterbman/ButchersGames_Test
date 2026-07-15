using Game.Scripts.Props.Controllers.General;
using Game.Scripts.Props.Interfaces;
using UnityEngine;

namespace Game.Scripts.Props.Controllers
{
    public class MoneyPropController : MonoBehaviour, IProp
    {
        [Header("References")] 
        [SerializeField] private BalancePropController _balancePropController;
        [SerializeField] private ParticalPropController _particalPropController;
        [SerializeField] private SoundPropController _soundPropController;

        public void Interact()
        {
            _balancePropController.ChangeBalance();
            _particalPropController.StartPartical();
            _soundPropController.Play();
        }
    }
}
