using System.Collections.Generic;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "New Resource Type Container", menuName = "Modules/PlacementAPI/ResourceTypeContainer", order = 1)]
    public class GlobalResourceTypeSO : ScriptableObject
    {
        [field: SerializeField] public List<ResourceTypeSO> ResourceTypeContainer { get;  set; }
    }
}