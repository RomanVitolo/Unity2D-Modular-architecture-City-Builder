using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime
{
    public class ResourceNode : MonoBehaviour
    {
       [field: SerializeField] public ResourceTypeSO ResourceType { get; set; }
    }
}