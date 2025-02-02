using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "New BuildingType", menuName = "Modules/PlacementAPI/BuildingType")]
    public class BuildingTypeSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite BuildingSprite { get; private set; }
        [field: SerializeField] public Transform PrefabType { get; private set; }
        [field: SerializeField] public bool HasResourceData { get; private set; }
        [field: SerializeField] public ResourcesGenerateData ResourcesGenerateData { get; private set; }
        [field: SerializeField] public float MinPlacementRadius { get; private set; }
        [field: SerializeField] public ResourceAmount[] ConstructionsResourceCost { get; private set; }
        [field: SerializeField] public int MaxHealthAmount { get; set; }

        public string ShowConstructionsResourceCost()
        {
            var text = string.Empty;
            foreach (var resourceAmount in ConstructionsResourceCost)
            {
                text += "<color=#" + resourceAmount.ResourceType.ColorHex + ">" + 
                        resourceAmount.ResourceType.Name + " " + resourceAmount.Amount + "</color> ";
            }
            return text;
        }
    }
}