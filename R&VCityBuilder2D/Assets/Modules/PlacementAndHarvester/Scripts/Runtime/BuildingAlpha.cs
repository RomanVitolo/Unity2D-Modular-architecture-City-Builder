using Modules.GameEngine.Core.Scripts;
using Modules.PlacementAPI.Scripts.Runtime.UI;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildingAlpha : MonoBehaviour
    {
        [SerializeField] private BuildController _buildController;
        [SerializeField] private GameObject _spriteGameObject;
        [SerializeField] private ResourceNearbyOverlay _resourceNearbyOverlay;
        private void Awake()
        {
            _buildController ??= FindAnyObjectByType<BuildController>();
            Hide();
        }

        private void Start() => _buildController.OnActiveBuildingTypeChanged += ActionWhenBuildingTypeChanged;
        private void Update() => transform.position = GameMotor.Instance.GetCurrentDeviceWorldPosition();

        private void ActionWhenBuildingTypeChanged(object sender, BuildController.OnActiveBuildingTypeChangedEvent e)
        {
            if (e.ActiveBuilding is null)
            {
                Hide();
                _resourceNearbyOverlay.Hide();
            }
            else
            {
                Show(e.ActiveBuilding.BuildingSprite);
                _resourceNearbyOverlay.Show(e.ActiveBuilding.ResourcesGenerateData);
            }
        }
        private void Show(Sprite alphaSprite)
        {
            _spriteGameObject.SetActive(true);
            _spriteGameObject.GetComponent<SpriteRenderer>().sprite = alphaSprite;
        }
        private void Hide() => _spriteGameObject.SetActive(false);

        private void OnDestroy() => _buildController.OnActiveBuildingTypeChanged -= ActionWhenBuildingTypeChanged;
    }
}