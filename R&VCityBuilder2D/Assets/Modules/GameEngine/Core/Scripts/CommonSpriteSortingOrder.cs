using System;
using UnityEngine;

namespace Modules.GameEngine.Core.Scripts
{
    public class CommonSpriteSortingOrder : MonoBehaviour
    {
            
        private SpriteRenderer spriteRenderer;
        private bool runOnce = true;
        private void Awake() => spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        private void LateUpdate()
        {
            var precisionMultiplier = 5f;
            spriteRenderer.sortingOrder = (int)(-transform.position.y  * precisionMultiplier);
            
            if (runOnce) Destroy(this);
        }
    }
}