﻿using UnityEngine;

namespace Modules.GameEngine.Runtime.Scripts
{
    public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    Debug.LogWarning("[Singleton] Instance of " + typeof(T) + " is not set up in the scene.");
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Debug.LogWarning("[Singleton] Duplicate instance of " + typeof(T) + " found. Destroying the new one.");
                    Destroy(gameObject);
                }
            }
        }
    }
}
    
