using System;
using System.Collections.Generic;
using CodiceApp.EventTracking;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        public event Action OnActiveBuildingTypeChanged;

        [SerializeField] private ToolTipUI _toolTip;
        [SerializeField] private Sprite _arrowSprite;
        [SerializeField] private BuildController _buildController;
        [SerializeField] private Transform _templateButton;
        [SerializeField] private List<BuildingTypeSO> _ignoredBuildingTypes = new List<BuildingTypeSO>();

        private Dictionary<BuildingTypeSO, Transform> _buttonTransformsDictionary;
        private Transform arrowButton;
        private void Awake()
        {
            _buttonTransformsDictionary = new Dictionary<BuildingTypeSO, Transform>();
            if(_buildController is null) _buildController = FindAnyObjectByType<BuildController>();
            if(_toolTip is null) _toolTip = FindAnyObjectByType<ToolTipUI>();
            
            _templateButton.gameObject.SetActive(false);
            
            var globalBuildingsContainerList = Resources.Load<GlobalBuildingTypeSO>(nameof(GlobalBuildingTypeSO));

            int index = 0;
            
            arrowButton = Instantiate(_templateButton, transform);
            arrowButton.gameObject.SetActive(true);

            float offsetAmount = +130f;
            arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            arrowButton.Find("Image").GetComponent<Image>().sprite = _arrowSprite;

            arrowButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                _buildController.SetActiveBuildingType(null);
                OnActiveBuildingTypeChanged?.Invoke();
            });
            
            var mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object EventSender, EventArgs e) =>
            {
                _toolTip.Show("Arrow");
            };
                
            mouseEnterExitEvents.OnMouseExit += (object EventSender, EventArgs e) =>
            {
                _toolTip.Hide();
            };
            
            index++;
            
            foreach (var buildingType in globalBuildingsContainerList.BuildingTypesContainer)
            {
                if(_ignoredBuildingTypes.Contains(buildingType)) continue;
                
                var btnTransform = Instantiate(_templateButton, transform);
                btnTransform.gameObject.SetActive(true);

                offsetAmount = +130f;
                btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

                btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.BuildingSprite;

                btnTransform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    _buildController.SetActiveBuildingType(buildingType);
                    OnActiveBuildingTypeChanged?.Invoke();
                });

                mouseEnterExitEvents = btnTransform.GetComponentInChildren<MouseEnterExitEvents>();
                mouseEnterExitEvents.OnMouseEnter += (object EventSender, EventArgs e) =>
                {
                    _toolTip.Show(buildingType.Name + "\n" + buildingType.ShowConstructionsResourceCost());
                };
                
                mouseEnterExitEvents.OnMouseExit += (object EventSender, EventArgs e) =>
                {
                    _toolTip.Hide();
                };
                
                _buttonTransformsDictionary[buildingType] = btnTransform;
                index++;
            }
        }
        private void Start() => OnActiveBuildingTypeChanged += ActiveBuildingTypeButton;
        private void ActiveBuildingTypeButton()
        {
            arrowButton.Find("Selected").gameObject.SetActive(false);
            foreach (var buildingType in _buttonTransformsDictionary.Keys)
            {
                var btnTransform = _buttonTransformsDictionary[buildingType];
                btnTransform.Find("Selected").gameObject.SetActive(false);
            }
            
            var activeBuildingType = _buildController.GetActiveBuildingType();
            if (activeBuildingType is null)
            {
                arrowButton.Find("Selected").gameObject.SetActive(true);
            }
            else
                _buttonTransformsDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
        }
        private void OnDestroy() => OnActiveBuildingTypeChanged -= ActiveBuildingTypeButton;
    }
}