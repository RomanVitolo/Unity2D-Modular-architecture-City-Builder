using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private TextMeshProUGUI _jelliesText;
        [SerializeField] private TextMeshProUGUI _jelliesBeansText;
        [SerializeField] private TextMeshProUGUI _cherryText;
        private void Awake()
        {
            _resourcesController ??= FindAnyObjectByType<ResourcesController>();
            
            var globalResourcesContainerList = Resources.Load<GlobalResourceTypeSO>(nameof(GlobalResourceTypeSO));
            
            
            
            foreach (var resourceType in globalResourcesContainerList.ResourceTypeContainer)
            {
                int setAmount = _resourcesController.ResourceAmount(resourceType);

            }
        }
    }
}