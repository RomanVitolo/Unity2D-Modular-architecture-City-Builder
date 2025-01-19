using System;
using Modules.Inputs.Scripts.Runtime;
using UnityEngine;

namespace Modules.GameEngine.Core.Scripts
{
    public class GameMotor : MonoBehaviour
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }

        public static GameMotor Instance;

        public void OnDeviceButtonDownEvent(Action RegisterAction) => InputReader.OnClickButtonDown += RegisterAction;
        public void OnRemoveDeviceButtonDownEvent(Action RemoveAction) => InputReader.OnClickButtonDown -= RemoveAction;

        public Vector2 GetPointPosition => InputReader.PointPosition;
       
    
        private void Awake()
        {
            Instance = this;
        
            InputReader.InitializeInputs();
        }

        private void OnDestroy()
        {
            InputReader.DeinitializeInputs();
        }
    }
}