using System.Collections.Generic;
using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime.BuildingSystem
{
    [CreateAssetMenu(fileName = "New Building Type Container", menuName = "Modules/PlacementAPI/BuildingTypeContainer", order = 0)]
    public class GlobalBuildingTypeSO : ScriptableObject
    {
        public List<BuildingTypeSO> BuildingTypesContainer;
    }
}