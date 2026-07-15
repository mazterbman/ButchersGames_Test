using System;
using System.Collections.Generic;
using Game.Scripts._UI.Interfaces;
using UnityEngine;

namespace Game.Scripts._UI
{
    public class CanvasController : MonoBehaviour, ICanvas
    {
        [Header("References")] 
        [SerializeField] private List<GameObject> _objects;

        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            _objects.ForEach(o => o.SetActive(true));
        }

        public void Hide()
        {
            _objects.ForEach(o => o.SetActive(false));
        }
    }
}
