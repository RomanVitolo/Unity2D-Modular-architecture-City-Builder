using Modules.GameEngine.Core.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildController : MonoBehaviour
    {
        private Camera camera;
        private GlobalBuildingTypeSO globalBuildingsContainerList;
        private BuildingTypeSO activeBuildingType;
      
        private void Start()
        {
            camera = Camera.main;
            globalBuildingsContainerList = Resources.Load<GlobalBuildingTypeSO>(nameof(GlobalBuildingTypeSO));
            
            GameMotor.Instance.OnDeviceButtonDownEvent(InstantiateCircle);
        }
       
        private void OnDisable()
        {
            GameMotor.Instance.OnRemoveDeviceButtonDownEvent(InstantiateCircle);
        }

        private void InstantiateCircle()
        {
            if(activeBuildingType is not null && !EventSystem.current.IsPointerOverGameObject())
                Instantiate(activeBuildingType.PrefabType, GetCurrentDeviceWorldPosition(), Quaternion.identity);
        }

        private Vector3 GetCurrentDeviceWorldPosition()
        {
            Vector3 currentPosition = camera.ScreenToWorldPoint(GameMotor.Instance.GetPointPosition);
            currentPosition.z = 0;
            return currentPosition;
        }

        public void SetActiveBuildingType(BuildingTypeSO buildingType)
        {
            activeBuildingType = buildingType;
        }

        public BuildingTypeSO GetActiveBuildingType()
        {
            return activeBuildingType;
        }
    }
}