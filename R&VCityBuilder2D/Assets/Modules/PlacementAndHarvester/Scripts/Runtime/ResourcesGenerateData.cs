using System;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    [Serializable]
    public class ResourcesGenerateData
    {
        public float MaxTimer;
        public ResourceTypeSO ResourceType;
        public float ResourceDetectionRadius;
        public int MaxResourceAmount;
    }
}