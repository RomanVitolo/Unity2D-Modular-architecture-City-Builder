using System;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class ResourceGenerator : MonoBehaviour
    {
        private ResourcesGenerateData resourcesGenerateData;
        private ResourcesController resourcesController;
        
        private float timer;
        private float maxTimer;

        private void Awake()
        {
            resourcesController = FindAnyObjectByType<ResourcesController>();
            resourcesGenerateData = GetComponent<BuildingTypeHolder>().BuildingType.ResourcesGenerateData;
            maxTimer = resourcesGenerateData.MaxTimer;
        }

        private void Start()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, 
                resourcesGenerateData.ResourceDetectionRadius);
            int nearbyResourceAmount = 0;
            foreach (var collider in colliders)
            {
                var resourceNode = collider.GetComponent<ResourceNode>();
                if (resourceNode is not null)
                {
                    if (resourceNode.ResourceType == resourcesGenerateData.ResourceType)
                    {
                        nearbyResourceAmount++;
                    }
                }
            }
            
            nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourcesGenerateData.MaxResourceAmount);
            
            if(nearbyResourceAmount == 0)
            {
                enabled = false;
            }
            else
            {
                maxTimer = (resourcesGenerateData.MaxTimer / 2f) + resourcesGenerateData.MaxTimer * 
                    (1 - (float) nearbyResourceAmount / resourcesGenerateData.MaxResourceAmount);
            }
            Debug.Log("NearbyResourceAmount " + nearbyResourceAmount + "; " + maxTimer);
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += maxTimer;
                resourcesController.AddResource(resourcesGenerateData.ResourceType, 1);
            }
        }
    }
}