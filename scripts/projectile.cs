using System;
using UnityEngine;

namespace Units.Resources.Projectile
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 startingPos;
        private Vector3 destination;
        private float startTime;
        private float journeyLength;

        [SerializeField]
        private float speed;

        [SerializeField]
        private GameObject explosion;

        public void SetProjectile(Vector3 startingPosition, Vector3 targetPos, float speedProjectile = 25)
        {
            transform.position = startingPos = startingPosition;
            destination = targetPos;
            speed = speedProjectile;
            startTime = Time.time;
            
            Vector2 triangle = new Vector2(destination.x - startingPos.x, destination.y - startingPos.y);
            double angle = Math.Atan(triangle.y/triangle.x) * (180 / Math.PI);
            transform.rotation = Quaternion.Euler(0, 0, (float)angle);
        }

        void Update()
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / Vector3.Distance(startingPos, destination);
            Vector3 newPos =  Vector3.Lerp(startingPos, destination, fractionOfJourney);

            transform.position = newPos;

            if (transform.position == destination)
            {
                Instantiate(explosion, destination, new Quaternion(0, 0, 0, 1));
                Destroy(gameObject);
            }
        }
    }
}