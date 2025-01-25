using UnityEngine;

namespace Modules.CameraSystem.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "Camera Settings", menuName = "Modules/Camera System/Camera Configuration")]
    public class CameraConfigurationSO : ScriptableObject
    {
        [field: SerializeField] public float CameraMoveSpeed { get; set; }
        [field: SerializeField] public float CameraZoomAmount { get; set; }
        [field: SerializeField] public float CameraZoomSpeed { get; set; }
        [field: SerializeField, Range(10,15)] public float CameraMinZoomSize { get; private set; }
        [field: SerializeField, Range(15,30)] public float CameraMaxZoomSize { get; private set; }
        [field: SerializeField] public float OrthographicZoomSize { get;  set; }
    }
}
