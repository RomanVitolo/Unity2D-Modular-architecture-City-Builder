using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class IdentifyResourceType : MonoBehaviour
    {
        [field: SerializeField] public ResourceTypeSO ResourceType { get; private set; }
    }
}