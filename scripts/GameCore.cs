using UnityEngine;

namespace Game.Core
{
    public static class GameDomain
    {
        public static Quaternion NewRotation(Transform obj, float rotatingSpeed, int inverter = 1)
        {
            float newRotation = obj.rotation.eulerAngles.z + (rotatingSpeed * Time.deltaTime * inverter);

            return Quaternion.Euler(0, 0, newRotation);
        }

        public static void Explosion(Vector3 center)
        {

        }
    }
}