using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime.ResourcesSystem
{
    public class ResourcesController : MonoBehaviour
    {
        public event EventHandler OnResourceAmountChanged; 
        
        [SerializeField] private List<ResourceAmount> _resourceAmounts = new List<ResourceAmount>();
        
        private readonly Dictionary<ResourceTypeSO, int> resourcesAmountDict = new Dictionary<ResourceTypeSO, int>();

        private GlobalResourceTypeSO globalResourcesContainerList;

        private void Start()
        {
            globalResourcesContainerList = Resources.Load<GlobalResourceTypeSO>(nameof(GlobalResourceTypeSO));

            foreach (var recourseType in globalResourcesContainerList.ResourceTypeContainer)
            {
                resourcesAmountDict[recourseType] = 0;
            }

            foreach (var resourceAmount in _resourceAmounts)
            {
                AddResource(resourceAmount.ResourceType, resourceAmount.Amount);
            }
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            globalResourcesContainerList = Resources.Load<GlobalResourceTypeSO>(nameof(GlobalResourceTypeSO));
            AddResource(globalResourcesContainerList.ResourceTypeContainer[0], 2);
        }

        public void AddResource(ResourceTypeSO resourceTypeSO, int amount)
        {
            resourcesAmountDict[resourceTypeSO] += amount;
            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        }
        public int GetResourceAmount(ResourceTypeSO resourceTypeSo) => resourcesAmountDict[resourceTypeSo];

        public bool CanAfford(ResourceAmount[] resourceAmounts)
        {
            foreach (var resourceAmount in resourceAmounts)
            {
                if (GetResourceAmount(resourceAmount.ResourceType) >= resourceAmount.Amount)
                {
                    
                }
                else
                    return false;
            }
            return true;
        }
        
        public void SpendResources(ResourceAmount[] resourceAmounts)
        {
            foreach (var resourceAmount in resourceAmounts)
                resourcesAmountDict[resourceAmount.ResourceType] -= resourceAmount.Amount;
        }
    }
}