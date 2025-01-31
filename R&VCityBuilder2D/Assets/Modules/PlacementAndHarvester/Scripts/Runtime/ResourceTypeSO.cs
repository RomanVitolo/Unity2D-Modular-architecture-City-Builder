using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "New Resource Type", menuName = "Modules/PlacementAPI/ResourceType")]
    public class ResourceTypeSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite ResourceSprite { get; set; }
        [field: SerializeField] public string ColorHex { get; set; }
    }
}