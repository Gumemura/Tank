using UnityEngine;
using Units.Vehicle;
using Units.Vehicle.Tank;

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

        // Update is called once per frame
        void Update()
        {
            if (tank)
            {
                tank.Accelerate(Input.GetAxisRaw("Vertical"));
                tank.Steer(Input.GetAxis("Horizontal"));
                tank.ChangeCannonHeight(Input.GetAxisRaw("AltVertical"));
                tank.DrawLineOfFire();
                tank.RotateTurret(Input.GetAxisRaw("AltHorizontal"));
                tank.Fire(Input.GetKeyDown(KeyCode.Space));
            }
        }

        public void GetControledTank(Tank activeTank)
        {
            tank = activeTank;
        }
    }
}