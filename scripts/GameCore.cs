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

        public static void DrawLineOfFire(Transform lineEmiter, Vector3 endingPoint)
        {
            LineRenderer lineRenderer = lineEmiter.GetComponent<LineRenderer>();
            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, lineEmiter.position);
                lineRenderer.SetPosition(1, endingPoint);
            }
            else
            {
                Debug.Log("Not able to find LineRenderer from " + lineEmiter.gameObject.name);
            }
        }

        public static void RotateWithMouse(Transform t)
        {
            Vector3 mouse_pos;
            Vector3 object_pos;

            mouse_pos = Input.mousePosition;
            mouse_pos.z = -20;
            object_pos = Camera.main.WorldToScreenPoint(t.position);
            float angle = Mathf.Atan2(mouse_pos.y - object_pos.y, 
                                      mouse_pos.x - object_pos.x) * Mathf.Rad2Deg;

            t.rotation = Quaternion.Euler(0, 0, angle);
        }

        public static Vector3 MousePositionToScreen()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane + 1;

            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}