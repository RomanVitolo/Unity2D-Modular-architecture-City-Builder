using System;
using TMPro;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class ResourceNearbyOverlay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private SpriteRenderer _icon;
        private ResourcesGenerateData resourcesGenerateData;

        private void Awake() => Hide();
        private void Update()
        {
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourcesGenerateData, 
                transform.position);
            float percent = Mathf.RoundToInt((float) nearbyResourceAmount / resourcesGenerateData.MaxResourceAmount * 100f);
            _text.SetText(percent + "%");
        }

        public void Show(ResourcesGenerateData resourcesGenerateData)
        {
            this.resourcesGenerateData = resourcesGenerateData;
            gameObject.SetActive(true);

            _icon.sprite = resourcesGenerateData.ResourceType.ResourceSprite;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}