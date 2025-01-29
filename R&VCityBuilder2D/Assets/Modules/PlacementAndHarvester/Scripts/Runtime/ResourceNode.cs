using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class ResourceNode : MonoBehaviour
    {
       [field: SerializeField] public ResourceTypeSO ResourceType { get; set; }
    }
}