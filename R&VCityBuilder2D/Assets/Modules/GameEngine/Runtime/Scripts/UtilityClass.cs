using UnityEngine;

namespace Modules.GameEngine.Runtime.Scripts
{
    public static class UtilityClass
    {
        public static float GetAngleFromVector(Vector3 vector)
        {
            float radians = Mathf.Atan2(vector.y, vector.x);
            float degrees = radians * Mathf.Rad2Deg;
            return degrees;
        }   
    }
}