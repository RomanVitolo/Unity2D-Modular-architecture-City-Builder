using System;
using System.Linq;
using Modules.GameEngine.Core.Scripts;
using Modules.PlacementAPI.Scripts.Runtime.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildController : MonoBehaviour
    {
        public event EventHandler<OnActiveBuildingTypeChangedEvent> OnActiveBuildingTypeChanged;
        public class OnActiveBuildingTypeChangedEvent : EventArgs { public BuildingTypeSO ActiveBuilding { get; set; } }
        
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private ToolTipUI _toolTip;
        
        private GlobalBuildingTypeSO globalBuildingsContainerList;
        private BuildingTypeSO activeBuildingType;

        private void Awake()
        {
            _toolTip ??= FindAnyObjectByType<ToolTipUI>();
        }

        private void Start()
        {
            globalBuildingsContainerList = Resources.Load<GlobalBuildingTypeSO>(nameof(GlobalBuildingTypeSO));
            
            GameMotor.Instance.OnDeviceButtonDownEvent(InstantiateCircle);
        }
        private void InstantiateCircle()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (activeBuildingType is not null)
                {
                    if(CanSpawnBuilding(activeBuildingType, GameMotor.Instance.GetCurrentDeviceWorldPosition(),
                           out string errorMessage))
                    {
                        if (_resourcesController.CanAfford(activeBuildingType.ConstructionsResourceCost))
                        {
                            _resourcesController.SpendResources(activeBuildingType.ConstructionsResourceCost);
                            Instantiate(activeBuildingType.PrefabType, GameMotor.Instance.GetCurrentDeviceWorldPosition(),
                                Quaternion.identity);
                        }
                        else
                        {
                            _toolTip.Show("Cannot afford " +
                                          activeBuildingType.ShowConstructionsResourceCost(), 2f);
                        }
                    }
                    else
                    {
                        _toolTip.Show(errorMessage, 2f);
                    }
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

        private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
        {
            var colliderType = buildingType.PrefabType.GetComponent<BoxCollider2D>();
            
            var buildingColliders = Physics2D.OverlapBoxAll(position + (Vector3)colliderType.offset,
                colliderType.size, 0f);
            
            var isAreaClear = buildingColliders.Length == 0;
            if (!isAreaClear)
            {
                errorMessage = "Area is not Clear!";
                return false;
            }
            
            var getCircleColliders = Physics2D.OverlapCircleAll(position, buildingType.MinPlacementRadius);
            
            if (getCircleColliders.Select(collider2D => 
                    collider2D.GetComponent<BuildingTypeHolder>())
                .Where(buildingTypeHolder => buildingTypeHolder is not null)
                .Any(buildingTypeHolder => buildingTypeHolder.BuildingType == buildingType))
            {
                errorMessage = "Too close to another building of the same Type!";
                return false;
            }
            
            float maxPlacementRadius = 25f;
            var maxCollidersRadius = Physics2D.OverlapCircleAll(position, maxPlacementRadius);
            
            foreach (var collider2D in maxCollidersRadius)
            {
                var buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
                if (buildingTypeHolder is not null)
                {
                    errorMessage = string.Empty;
                    return true;
                }
            }
            errorMessage = "Too far from any other Building!";
            return false;
        }
    }
}