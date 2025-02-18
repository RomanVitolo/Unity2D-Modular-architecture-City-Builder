using System;

namespace Modules.CoreGameplay.Scripts.Runtime.ResourcesSystem
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