using Game.Core;
using UnityEngine;

namespace Units.Soldier
{
    public class Soldier : MonoBehaviour
    {
        private const string LINE_OF_FIRE_NAME = "LineOfFireEmiter";
        private const float SPRINT_BONUS = .8f;
        private const float AIMING_DEBUFF =.5f;

        private Transform lineOfFireEmiter;
        private Rigidbody2D soldierRigidbody;

        public float speed;

        // Start is called before the first frame update
        void Start()
        {
            lineOfFireEmiter = transform.Find(LINE_OF_FIRE_NAME);
            soldierRigidbody = transform.GetComponent<Rigidbody2D>();
        }

        private float SprintOrAiming(bool sprintButtonPressed, bool aimingButtonPressed)
        {
            if (aimingButtonPressed)
            {
                return AIMING_DEBUFF;
            }
            else if (sprintButtonPressed)
            {
                return 1 + SPRINT_BONUS;
            }
            return 1;
        }

        public void Move(float verticalWalkCommand, float horizontalWalkCommand, bool isSprinting, bool isAiming)
        {
            // transform.position += (Vector3.up * speed * verticalWalkCommand * Time.deltaTime * sprintBonus);
            // transform.position += (Vector3.right * speed * horizontalWalkCommand * Time.deltaTime * sprintBonus);

            float sprintBonus = SprintOrAiming(isSprinting, isAiming);
            soldierRigidbody.AddForce(Vector2.up * speed * verticalWalkCommand * sprintBonus);
            soldierRigidbody.AddForce(Vector2.right * speed * horizontalWalkCommand * sprintBonus);
        }

        public void DrawLineOfFire(bool aimingButtonPressed, Vector3 mousePos)
        {
            LineRenderer lineRenderer = lineOfFireEmiter.GetComponent<LineRenderer>();
            if (aimingButtonPressed)
            {
                lineRenderer.enabled = true;
                GameDomain.DrawLineOfFire(lineOfFireEmiter, mousePos);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    } 
}