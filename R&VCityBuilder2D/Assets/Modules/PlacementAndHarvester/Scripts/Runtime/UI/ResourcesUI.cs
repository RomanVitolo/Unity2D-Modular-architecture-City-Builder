using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private ResourcesController _resourcesController;
        private GlobalResourceTypeSO globalResourcesContainerList;

        private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;
        private void Awake()
        {
            _resourcesController ??= FindAnyObjectByType<ResourcesController>();
            resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
            
            globalResourcesContainerList = Resources.Load<GlobalResourceTypeSO>(nameof(GlobalResourceTypeSO));

            var resourceTemplate = transform.Find("ResourceHolderComponent");
            resourceTemplate.gameObject.SetActive(false);

            int index = 0;
            foreach (var resourceType in globalResourcesContainerList.ResourceTypeContainer)
            {
                var resourceTransform = Instantiate(resourceTemplate, transform);
                resourceTransform.gameObject.SetActive(true);
                var offsetAmount = -160f;
                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
                
                resourceTransform.Find("ResourceImage").GetComponent<Image>().sprite = resourceType.ResourceSprite;
                
                resourceTypeTransformDictionary[resourceType] = resourceTransform;
                index++;
            }
        }

        private void Start()
        {
            _resourcesController.OnResourceAmountChanged += ResourceAmountChanged;

            UpdateResourceAmount();
        }

        private void ResourceAmountChanged(object sender, EventArgs e) => UpdateResourceAmount();

        private void UpdateResourceAmount()
        {
            foreach (var resourceType in globalResourcesContainerList.ResourceTypeContainer)
            {
                int resourceAmount = _resourcesController.GetResourceAmount(resourceType);
                var resourceTransform = resourceTypeTransformDictionary[resourceType];
                resourceTransform.Find("ResourceText").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            }
        }
        private void OnDestroy() => _resourcesController.OnResourceAmountChanged -= ResourceAmountChanged;
    }
}