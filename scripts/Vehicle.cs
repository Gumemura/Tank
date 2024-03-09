using UnityEngine;
using System;

namespace Units.Vehicle
{
    public abstract class Vehicle: MonoBehaviour
    {
        private float horsepowerWeightRatio = 20;

        [SerializeField]
        public float stearingSpeed; // Turning speed

        [SerializeField]
        private float currentAcceleration, maxAcceleration, maxDeceleration, enginePower, vehicleMass;

        private float currentSpeed;

        public bool hasMovimentAnimation = false;

        public void Start()
        {
            maxAcceleration = enginePower / vehicleMass * 100;
            maxDeceleration = maxAcceleration / 3 * -1;
            stearingSpeed = maxAcceleration * 2.5f;
        }

        // public void Update()
        // {
        //     Accelerate();
        //     Steer();
        // }

        private float SpeedCap(float speedCapButtonPressed)
        {
            if (speedCapButtonPressed != 0)
            {
                return 0.25f;
            }
            return 1;
        }

        private void CalculateAcceleration(float accelerationCommand)
        {
            float inputVertical = accelerationCommand;
            float accelerationTarget;
            float oppositeForce = 1;

            if (accelerationCommand > 0){
                accelerationTarget = maxAcceleration * SpeedCap(accelerationCommand);
            }
            else if (accelerationCommand < 0)
            {
                accelerationTarget = maxDeceleration;
            }
            else
            {
                accelerationTarget = 0;
            }

            if (Math.Sign(currentAcceleration) != Math.Sign(accelerationCommand))
            {
                oppositeForce = 3;
            }
            currentAcceleration = Mathf.MoveTowards(currentAcceleration, accelerationTarget, ((enginePower * horsepowerWeightRatio)/vehicleMass) * Time.deltaTime * oppositeForce);
        }

        public void Accelerate(float accelerationCommand)
        {
            CalculateAcceleration(accelerationCommand);

            currentSpeed = currentAcceleration * Time.deltaTime;

            MoveFoward(currentSpeed);
        }

        public void MoveFoward(float speed)
        {
            transform.position += transform.up * speed;
            if (hasMovimentAnimation)
            {
                if(currentSpeed != 0)
                {
                    MoveFowardAnimation(true);
                }
                else
                {
                    MoveFowardAnimation(false);
                }
            }
        }

        public abstract void Steer(float turningForce);

        public abstract void MoveFowardAnimation(bool activateMovimentAnimation);
    }
}