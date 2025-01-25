using Modules.GameEngine.Core.Scripts;
using Unity.Cinemachine;
using UnityEngine;

namespace Modules.CameraSystem.Scripts.Runtime
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _virtualCamera;
        [SerializeField] private CameraConfigurationSO _cameraConfigurationSo;
        
        private float targetOrthographicSize;
        private void Awake() => _virtualCamera ??= GetComponentInChildren<CinemachineCamera>();
        private void Start()
        {
            _virtualCamera.Lens.OrthographicSize = _cameraConfigurationSo.OrthographicZoomSize;
            targetOrthographicSize = _cameraConfigurationSo.OrthographicZoomSize;
        }

        private void LateUpdate()
        {
            var moveDir = new Vector3(GameMotor.Instance.GetPlayerInputMovement.x,
                GameMotor.Instance.GetPlayerInputMovement.y, 0f).normalized;
            
            transform.position += moveDir * (_cameraConfigurationSo.CameraMoveSpeed * Time.deltaTime);
           
            targetOrthographicSize += -GameMotor.Instance.GetPlayerZoom.y * _cameraConfigurationSo.CameraZoomAmount;
            
            targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, _cameraConfigurationSo.CameraMinZoomSize, 
                _cameraConfigurationSo.CameraMaxZoomSize);
            
            _cameraConfigurationSo.OrthographicZoomSize = Mathf.Lerp(_cameraConfigurationSo.OrthographicZoomSize, 
                targetOrthographicSize, Time.deltaTime * 
                _cameraConfigurationSo.CameraZoomSpeed);
            
            _virtualCamera.Lens.OrthographicSize = _cameraConfigurationSo.OrthographicZoomSize; 
        }
    }
}