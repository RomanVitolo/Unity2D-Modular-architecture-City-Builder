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
        public Vector2 GetPlayerInputMovement => InputReader.MovementInputValue;
        public Vector2 GetPlayerZoom => InputReader.ZoomInputValue;

        protected override void Awake()
        {
            base.Awake();
            
            InputReader.InitializeInputs();
        }

        private void OnDestroy()
        {
            if(InputReader is not null)
                InputReader.DeinitializeInputs();
        }
    }
}