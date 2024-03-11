using Controller.Units.SoldierC;
using Controller.Units.Vehicle;
using Units.Soldier;
using Units.Vehicle.Tank;
using UnityEngine;

namespace Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const string VEHICLE_CONTROLER = "PlayerVechicleController";
        private const string SOLDIER_CONTROLER = "PlayerSoldierController";
        private const string HERO = "PlayerHero";

        public Tank currentTank;
        public Soldier currentSoldier;
        public Soldier hero;

        private VehicleController vehicleController;
        private SoldierController soldierController;

        // Start is called before the first frame update
        void Start()
        {
            hero = transform.Find(HERO).GetComponent<Soldier>();
            currentSoldier = hero;

            vehicleController = transform.Find(VEHICLE_CONTROLER).GetComponent<VehicleController>();
            soldierController = transform.Find(SOLDIER_CONTROLER).GetComponent<SoldierController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentTank)
            {
                vehicleController.SetControlledTank(currentTank);
            }
            else if (currentSoldier)
            {
                soldierController.SetControlledSoldier(currentSoldier);
            }
        }

        // private void GetInVehicle(Vehicle vehicle)
        // {
        //     currentTank = vehicle;
        //     currentSoldier = null;
        // }

        // private void GetOutVehicle()
        // {
        //     currentTank = null;
        //     currentSoldier = hero;
        // }
    }    
}