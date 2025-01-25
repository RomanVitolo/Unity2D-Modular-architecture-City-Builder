using System;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class ResourceGenerator : MonoBehaviour
    {
        private BuildingTypeSO buildingType;
        private ResourcesController resourcesController;
        
        private float timer;
        private float maxTimer;

        private void Awake()
        {
            resourcesController = FindAnyObjectByType<ResourcesController>();
            buildingType = GetComponent<BuildingTypeHolder>().BuildingType;
            maxTimer = buildingType.ResourcesGenerateData.MaxTimer;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += maxTimer;
                resourcesController.AddResource(buildingType.ResourcesGenerateData.ResourceType, 1);
            }
                
        }
    }
}