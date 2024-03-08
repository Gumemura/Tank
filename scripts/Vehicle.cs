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

        public void Update()
        {
            Accelerate();
            Steer();
        }

        private float SpeedCap()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return 0.25f;
            }
            return 1;
        }

        private void CalculateAcceleration()
        {
            float inputVertical = Input.GetAxisRaw("Vertical");
            float accelerationTarget;
            float oppositeForce = 1;

            if (inputVertical > 0){
                accelerationTarget = maxAcceleration * SpeedCap();
            }
            else if (inputVertical < 0)
            {
                accelerationTarget = maxDeceleration;
            }
            else
            {
                accelerationTarget = 0;
            }

            if (Math.Sign(currentAcceleration) != Math.Sign(inputVertical))
            {
                oppositeForce = 3;
            }
            currentAcceleration = Mathf.MoveTowards(currentAcceleration, accelerationTarget, ((enginePower * horsepowerWeightRatio)/vehicleMass) * Time.deltaTime * oppositeForce);
        }

        protected void Accelerate()
        {
            CalculateAcceleration();

            currentSpeed = currentAcceleration * Time.deltaTime;

            MoveFoward(currentSpeed);
        }

        public void MoveFoward(float speed)
        {
            transform.position += transform.up * speed;
            if (hasMovimentAnimation)
            {dsdassdas
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

        public abstract void Steer();

        public abstract void MoveFowardAnimation(bool activateMovimentAnimation);
    }
}