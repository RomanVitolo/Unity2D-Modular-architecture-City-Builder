using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime.ResourcesSystem
{
    public class ResourceNode : MonoBehaviour
    {
       [field: SerializeField] public ResourceTypeSO ResourceType { get; set; }
    }
}