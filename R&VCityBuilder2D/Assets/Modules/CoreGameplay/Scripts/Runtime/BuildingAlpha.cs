using Modules.CoreGameplay.Scripts.Runtime.UI;
using Modules.GameEngine.Runtime.Scripts;
using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime
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
            if (e.ActiveBuilding == null)
            {
                Hide();
                _resourceNearbyOverlay.Hide();
            }
            else
            {
                Show(e.ActiveBuilding.BuildingSprite);
                if(e.ActiveBuilding.HasResourceData)
                    _resourceNearbyOverlay.Show(e.ActiveBuilding.ResourcesGenerateData);
                else
                    _resourceNearbyOverlay.Hide();
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