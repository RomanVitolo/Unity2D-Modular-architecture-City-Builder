using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime
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
        private void Start() => ManageResourceNodes();
        private void Update() => ManageResourcesTimer();
        private void ManageResourcesTimer()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += maxTimer;
                resourcesController.AddResource(resourcesGenerateData.ResourceType, 1);
            }
        }

        public static int GetNearbyResourceAmount(ResourcesGenerateData resourcesGenerateData, Vector3 position)
        {
            var colliders = Physics2D.OverlapCircleAll(position, 
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
            return nearbyResourceAmount;
        }

        private void ManageResourceNodes()
        {
            int nearbyResourceAmount = GetNearbyResourceAmount(resourcesGenerateData, transform.position);
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
        public ResourcesGenerateData GetResourceGeneratorData() => resourcesGenerateData;
        public float GetTimerNormalized() => timer / maxTimer;
        public float GetAmountGeneratedPerSecond() => 1 / maxTimer;
    }
}