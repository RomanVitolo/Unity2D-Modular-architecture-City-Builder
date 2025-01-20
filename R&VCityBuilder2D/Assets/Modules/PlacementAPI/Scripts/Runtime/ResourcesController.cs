﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class ResourcesController : MonoBehaviour
    {
        private Dictionary<ResourceTypeSO, int> resourcesAmountDict = new Dictionary<ResourceTypeSO, int>();

        private GlobalResourceTypeSO globalResourcesContainerList;
        private void Start()
        {
            globalResourcesContainerList = Resources.Load<GlobalResourceTypeSO>(nameof(GlobalResourceTypeSO));

            foreach (var recourseType in globalResourcesContainerList.ResourceTypeContainer)
            {
                resourcesAmountDict[recourseType] = 0;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                globalResourcesContainerList = Resources.Load<GlobalResourceTypeSO>(nameof(GlobalResourceTypeSO));
                AddResource(globalResourcesContainerList.ResourceTypeContainer[0], 2);
                TestResourcesLog();
            }
                
        }

        private void TestResourcesLog()
        {
            foreach (var resourceType in resourcesAmountDict.Keys)
            {
                Debug.Log(resourceType.Name + ": " + resourcesAmountDict[resourceType]);
            }
        }

        public void AddResource(ResourceTypeSO resourceTypeSO, int amount)
        {
            resourcesAmountDict[resourceTypeSO] += amount;
            TestResourcesLog();
        }
    }
}