using TMPro;
using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime.UI
{
    public class ResourcesGeneratorOverlay : MonoBehaviour
    {
        [SerializeField] private ResourceGenerator _resourceGenerator;
        [SerializeField] private SpriteRenderer _iconSprite;
        [SerializeField] private Transform _bar;
        [SerializeField] private TextMeshPro _text;

        private void Start()
        {
            _resourceGenerator ??= GetComponentInParent<ResourceGenerator>();
            
            var resourceGeneratorData = _resourceGenerator.GetResourceGeneratorData();
            
            _iconSprite.sprite = resourceGeneratorData.ResourceType.ResourceSprite;
            _text.SetText(_resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        }
        private void Update() => _bar.localScale = new Vector3(1 - _resourceGenerator.GetTimerNormalized(), 1,1);
    }
}