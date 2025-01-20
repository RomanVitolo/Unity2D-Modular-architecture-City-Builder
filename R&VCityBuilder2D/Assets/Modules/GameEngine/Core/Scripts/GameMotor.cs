using System;
using Modules.Inputs.Scripts.Runtime;
using UnityEngine;

namespace Modules.GameEngine.Core.Scripts
{
    public class GameMotor : Singleton<GameMotor>
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        public void OnDeviceButtonDownEvent(Action registerAction) => InputReader.OnClickButtonDown += registerAction;
        public void OnRemoveDeviceButtonDownEvent(Action removeAction) => InputReader.OnClickButtonDown -= removeAction;
        public Vector2 GetPointPosition => InputReader.PointPosition;

        protected override void Awake()
        {
            base.Awake();
            
            InputReader.InitializeInputs();
        }

        private void OnDestroy() => InputReader.DeinitializeInputs();
    }
}