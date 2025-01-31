using System;
using Modules.GameEngine.Core.Scripts;
using TMPro;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class ToolTipUI : MonoBehaviour
    {   
        [SerializeField] private RectTransform _canvasTransform;
        [SerializeField] private TextMeshProUGUI _tooltipText;
        [SerializeField] private RectTransform _backgroundTransform;
        
        private RectTransform _rectTransform;
        private float? tooltipTimer;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            Hide();
        }

        private void Update()
        {
           HandleMouseFollow();

           tooltipTimer -= Time.deltaTime;
           if(tooltipTimer <= 0) Hide();
        }

        private void HandleMouseFollow()
        {
            var anchoredPosition= GameMotor.Instance.GetPointPosition
                                  / _canvasTransform.localScale.x;

            if (anchoredPosition.x + _backgroundTransform.rect.width > _canvasTransform.rect.width)
                anchoredPosition.x = _canvasTransform.rect.width - _backgroundTransform.rect.width;
           
            if (anchoredPosition.y + _backgroundTransform.rect.height > _canvasTransform.rect.height)
                anchoredPosition.y = _canvasTransform.rect.height - _backgroundTransform.rect.height;
           
            _rectTransform.anchoredPosition = anchoredPosition;
        }

        private void AssignText(string tooltipText)
        {
            
            _tooltipText.SetText(tooltipText);
            _tooltipText.ForceMeshUpdate();

            var textSize = _tooltipText.GetRenderedValues(false);
            var padding = new Vector2(8, 8);
            _backgroundTransform.sizeDelta = textSize + padding;
        }

        public void Show(string tooltipText, float? timer = null)
        {
            tooltipTimer = timer;
            gameObject.SetActive(true);
            AssignText(tooltipText);
            HandleMouseFollow();
        }
        public void Hide() => gameObject.SetActive(false);
    }
}