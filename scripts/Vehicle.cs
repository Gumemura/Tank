using System;
using UnityEngine;

namespace Units.Vehicle
{
    public abstract class Vehicle: MonoBehaviour
    {
        private const float TURNING_SPEED_MULTIPLIER = 50;
        private const float FORCE_MULTIPLIER = 2000;
        private const float HORSE_POWER_RATIO = 100;
        private const float REVER_MAX_ACCELERATION_RATIO = 3;

        [SerializeField]
        public float stearingSpeed; // Turning speed

        [SerializeField]
        private float currentAcceleration, maxAcceleration, maxDeceleration, enginePower, vehicleMass;

        private float currentSpeed;

        [HideInInspector]
        public Rigidbody2D vehicleRigidbody;

        public bool hasMovimentAnimation = false;

        public void Start()
        {
            maxAcceleration = (enginePower * HORSE_POWER_RATIO)/ vehicleMass;
            maxDeceleration = maxAcceleration / REVER_MAX_ACCELERATION_RATIO * -1;
            stearingSpeed = maxAcceleration * TURNING_SPEED_MULTIPLIER;
            vehicleRigidbody = transform.GetComponent<Rigidbody2D>();
        }

        public void Update()
        {

        }

        private float SpeedCap(bool speedCapButtonPressed)
        {
            if (speedCapButtonPressed)
            {
                return 0.25f;
            }
            return 1;
        }

        private void CalculateAcceleration(float accelerationCommand, bool speedCapCommand)
        {
            float accelerationTarget;

            if (accelerationCommand > 0){
                accelerationTarget = maxAcceleration * SpeedCap(speedCapCommand);
            }
            else if (accelerationCommand < 0)
            {
                accelerationTarget = maxDeceleration;
            }
            else
            {
                accelerationTarget = 0;
            }

            currentAcceleration = Mathf.MoveTowards(currentAcceleration, accelerationTarget, 1);
        }

        public void Accelerate(float accelerationCommand, bool speedCapCommand = false)
        {
            if (vehicleRigidbody)
            {
                CalculateAcceleration(accelerationCommand, speedCapCommand);

                vehicleRigidbody.AddRelativeForce(Vector2.up * currentAcceleration * enginePower * FORCE_MULTIPLIER);
            }

            if (hasMovimentAnimation)
            {
                if(currentAcceleration != 0)
                {
                    MoveFowardAnimation(true);
                }
                else
                {
                    MoveFowardAnimation(false);
                }
            }
        }

        public abstract void Steer(float turningCommand);

        public abstract void MoveFowardAnimation(bool activateMovimentAnimation);

        //public abstract void SetRigidodyParams(float mass, float lienarDrag);
    }
}