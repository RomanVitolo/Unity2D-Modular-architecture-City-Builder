﻿using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime.ResourcesSystem
{
    [CreateAssetMenu(fileName = "New Resource Type", menuName = "Modules/PlacementAPI/ResourceType")]
    public class ResourceTypeSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite ResourceSprite { get; set; }
        [field: SerializeField] public string ColorHex { get; set; }
    }
}