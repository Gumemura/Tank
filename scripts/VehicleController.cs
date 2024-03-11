using Units.Vehicle;
using Units.Vehicle.Tank;
using UnityEngine;

namespace Controller.Units.Vehicle
{
    public class VehicleController : MonoBehaviour
    {
        private Tank tank;

        // Start is called before the first frame update
        void Start()
        {
            tank = null;
        }

        void FixedUpdate()
        {
            if (tank)
            {
                tank.Accelerate(Input.GetAxisRaw("Vertical"), Input.GetKey(KeyCode.LeftShift));
                tank.Steer(Input.GetAxis("Horizontal"));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (tank)
            {
                tank.ChangeCannonHeight(Input.GetAxisRaw("AltVertical"));
                tank.DrawLineOfFire();
                tank.RotateTurret(Input.GetAxisRaw("AltHorizontal"));
                tank.Fire(Input.GetKeyDown(KeyCode.Space));
            }
        }

        public void SetControlledTank(Tank activeTank)
        {
            tank = activeTank;
        }
    }
}