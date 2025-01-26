using Modules.GameEngine.Core.Scripts;
using UnityEngine;

namespace Modules.PlacementAPI.Scripts.Runtime
{
    public class BuildingAlpha : MonoBehaviour
    {
        [SerializeField] private BuildController _buildController;
        [SerializeField] private GameObject _spriteGameObject;

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
                Hide();
            else
                Show(e.ActiveBuilding.BuildingSrite);
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