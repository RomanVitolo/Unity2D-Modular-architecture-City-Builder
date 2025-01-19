using Modules.GameEngine.Core.Scripts;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildController : MonoBehaviour
    {
        
        [SerializeField] private Transform _cursorTransform;

        private Camera camera;
      
        private void Start() => camera = Camera.main;

        private void OnEnable()
        {
            GameMotor.Instance.OnDeviceButtonDownEvent(InstantiateCircle);
        }

        private void OnDisable()
        {
            GameMotor.Instance.OnRemoveDeviceButtonDownEvent(InstantiateCircle);
        }

        private void InstantiateCircle()
        {
            Instantiate(_cursorTransform, GetCurrentDeviceWorldPosition(), Quaternion.identity);
        }

        private Vector3 GetCurrentDeviceWorldPosition()
        {
            Vector3 currentdPosition = camera.ScreenToWorldPoint(GameMotor.Instance.GetPointPosition);
            currentdPosition.z = 0;
            return currentdPosition;
        }
    }
}