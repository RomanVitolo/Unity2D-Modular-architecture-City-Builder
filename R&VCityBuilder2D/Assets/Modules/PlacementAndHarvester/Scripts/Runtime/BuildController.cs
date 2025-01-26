using System;
using Modules.GameEngine.Core.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildController : MonoBehaviour
    {
        public event EventHandler<OnActiveBuildingTypeChangedEvent> OnActiveBuildingTypeChanged;
        public class OnActiveBuildingTypeChangedEvent : EventArgs { public BuildingTypeSO ActiveBuilding { get; set; } }
        
        private GlobalBuildingTypeSO globalBuildingsContainerList;
        private BuildingTypeSO activeBuildingType;
        private void Start()
        {
            globalBuildingsContainerList = Resources.Load<GlobalBuildingTypeSO>(nameof(GlobalBuildingTypeSO));
            
            GameMotor.Instance.OnDeviceButtonDownEvent(InstantiateCircle);
        }
        private void InstantiateCircle()
        {
            if(activeBuildingType is not null && !EventSystem.current.IsPointerOverGameObject())
                Instantiate(activeBuildingType.PrefabType, GameMotor.Instance.GetCurrentDeviceWorldPosition(), Quaternion.identity);
        }

        public void SetActiveBuildingType(BuildingTypeSO buildingType)
        {
            activeBuildingType = buildingType;
            OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEvent
            {
                ActiveBuilding = activeBuildingType
            });
        }
        public BuildingTypeSO GetActiveBuildingType() => activeBuildingType;
        private void OnDisable() => GameMotor.Instance.OnRemoveDeviceButtonDownEvent(InstantiateCircle);
    }
}