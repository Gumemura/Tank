using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Units.VehicleNS;
using Units.VehicleNS.TankNS;
using UnityEngine;

namespace Units.Soldier
{
    public class Soldier : MonoBehaviour
    {
        private const string LINE_OF_FIRE_NAME = "LineOfFireEmiter";
        private const string INTERAZTION_ZONE_NAME = "AreaOfInteraction";
        private const float SPRINT_BONUS = .8f;
        private const float AIMING_DEBUFF = .4f;
        private const float RAYCAST_DISTANCE = 1;
        private const float INTERACT_BUTTON_PRESSING_NEED = 2;

        private Transform lineOfFireEmiter;
        private Rigidbody2D soldierRigidbody;
        private AreaOfInteraction areaOfInteraction;
        private List<Transform> interactibles = new List<Transform>();
        private bool interacting = false;
        private float startingInteractButtonPressingTime;

        public float speed;

        // Start is called before the first frame update
        void Start()
        {
            lineOfFireEmiter = transform.Find(LINE_OF_FIRE_NAME);
            soldierRigidbody = transform.GetComponent<Rigidbody2D>();
            areaOfInteraction = transform.Find(INTERAZTION_ZONE_NAME).GetComponent<AreaOfInteraction>();
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
            float sprintBonus = SprintOrAiming(isSprinting, isAiming);
            float forceToBeApplied = speed * sprintBonus * Time.fixedDeltaTime;
            soldierRigidbody.AddForce(new Vector2(forceToBeApplied * horizontalWalkCommand, forceToBeApplied * verticalWalkCommand));
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

        public void GetInteractable()
        {
            interactibles = areaOfInteraction.GetContacts();
        }

        public void InteractWithInteractable(bool interactCommand)
        {
            if (interactibles.Count > 0 && interactCommand)
            {
                if (!interacting)
                {
                    startingInteractButtonPressingTime = Time.time;
                    interacting = true;
                }
                else
                {   
                    SendMessageUpwards("RenderInteractionLoading", (Time.time - startingInteractButtonPressingTime) / INTERACT_BUTTON_PRESSING_NEED);
                    if (startingInteractButtonPressingTime + INTERACT_BUTTON_PRESSING_NEED < Time.time && interacting)
                    {
                        Transform interactable = interactibles.First();
                        string tag = interactable.gameObject.tag;

                        if (GameDomain.tags.Contains(tag))
                        {
                            switch(tag)
                            {
                                case "Tank":
                                    GetInTank(interactable.GetComponent<Tank>());
                                    break;

                                default:
                                    break;
                            }
                        }
                        interacting = false;
                        SendMessageUpwards("RenderInteractionLoading", -1);
                    }
                }
            }
            else
            {
                interacting = false;
                SendMessageUpwards("RenderInteractionLoading", -1);
            }
        }

        private void GetInTank(Tank tank)
        {
            print("Entrando no tanque");
            SendMessageUpwards("GetInTank", tank);
        }
    }
}