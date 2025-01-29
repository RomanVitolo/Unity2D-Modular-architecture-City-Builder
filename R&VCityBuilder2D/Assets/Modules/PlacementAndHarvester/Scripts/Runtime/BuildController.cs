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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (activeBuildingType is not null 
                    && CanSpawnBuilding(activeBuildingType, GameMotor.Instance.GetCurrentDeviceWorldPosition()))
                {
                    Instantiate(activeBuildingType.PrefabType, GameMotor.Instance.GetCurrentDeviceWorldPosition(),
                        Quaternion.identity);
                }
            }
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

        private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
        {
            var colliderType = buildingType.PrefabType.GetComponent<BoxCollider2D>();
            
            var buildingColliders = Physics2D.OverlapBoxAll(position + (Vector3)colliderType.offset,
                colliderType.size, 0f);
            
            var isAreaClear = buildingColliders.Length == 0;
            if(!isAreaClear) return false;
            
            var getCircleColliders = Physics2D.OverlapCircleAll(position, buildingType.MinPlacementRadius);
            
            foreach (var collider2D in getCircleColliders)
            {
               var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
               if (buildingTypeHolder is not null)
               {
                   if (buildingTypeHolder.BuildingType == buildingType)
                   {
                       return false;
                   }
               }
            }
            
            float maxPlacementRadius = 25f;
            var maxCollidersRadius = Physics2D.OverlapCircleAll(position, maxPlacementRadius);
            
            foreach (var collider2D in maxCollidersRadius)
            {
                var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder is not null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}