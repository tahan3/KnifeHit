using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.UI
{
    public class ClickPanel : MonoBehaviour, IPointerDownHandler, IClickPanel
    {
        public event Action OnClick;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}