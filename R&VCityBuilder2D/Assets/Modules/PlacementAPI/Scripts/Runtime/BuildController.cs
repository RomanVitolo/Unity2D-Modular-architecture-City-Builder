using Modules.GameEngine.Core.Scripts;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildController : MonoBehaviour
    {
        private Camera camera;
        private GlobalBuildingTypeSO globalBuildingsContainerList;
        private BuildingTypeSO buildingType;
      
        private void Start()
        {
            camera = Camera.main;
            globalBuildingsContainerList = Resources.Load<GlobalBuildingTypeSO>(nameof(GlobalBuildingTypeSO));
            buildingType = globalBuildingsContainerList.BuildingTypesContainer[0];
            
            GameMotor.Instance.OnDeviceButtonDownEvent(InstantiateCircle);
        }
       
        private void OnDisable()
        {
            GameMotor.Instance.OnRemoveDeviceButtonDownEvent(InstantiateCircle);
        }

        private void InstantiateCircle()
        {
            Instantiate(buildingType.PrefabType, GetCurrentDeviceWorldPosition(), Quaternion.identity);
        }

        private Vector3 GetCurrentDeviceWorldPosition()
        {
            Vector3 currentPosition = camera.ScreenToWorldPoint(GameMotor.Instance.GetPointPosition);
            currentPosition.z = 0;
            return currentPosition;
        }
    }
}