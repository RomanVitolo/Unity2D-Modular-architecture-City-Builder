using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.PlacementAPI.Scripts.Runtime.UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        [SerializeField] private Sprite _arrowSprite;
        [SerializeField] private BuildController _buildController;
        [SerializeField] private Transform _templateButton;

        private Dictionary<BuildingTypeSO, Transform> _buttonTransformsDictionary;
        private Transform arrowButton;
        
        public event Action OnActiveBuildingTypeChanged;
        private void Awake()
        {
            _buttonTransformsDictionary = new Dictionary<BuildingTypeSO, Transform>();
            if(_buildController is null) _buildController = FindAnyObjectByType<BuildController>();
            
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
            index++;
            
            foreach (var buildingType in globalBuildingsContainerList.BuildingTypesContainer)
            {
                var btnTransform = Instantiate(_templateButton, transform);
                btnTransform.gameObject.SetActive(true);

                offsetAmount = +130f;
                btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

                btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.BuildingSrite;

                btnTransform.GetComponent<Button>().onClick.AddListener(() =>
                {
                    _buildController.SetActiveBuildingType(buildingType);
                    OnActiveBuildingTypeChanged?.Invoke();
                });
                
                _buttonTransformsDictionary[buildingType] = btnTransform;
                index++;
            }
        }

        private void Start()
        {
            OnActiveBuildingTypeChanged += ActiveBuildingTypeButton;
        }

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
        
        private void OnDestroy()
        {
            OnActiveBuildingTypeChanged -= ActiveBuildingTypeButton;
        }
    }
}