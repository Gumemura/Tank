using Controller.Units.SoldierC;
using Controller.Units.VehicleNS;
using Game.UI;
using Units.Soldier;
using Units.VehicleNS;
using Units.VehicleNS.TankNS;
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
        public CircleFillHandler interactionLoader;

        private VehicleController vehicleController;
        private SoldierController soldierController;

        // Start is called before the first frame update
        void Start()
        {
            hero = transform.Find(HERO).GetComponent<Soldier>();
            currentSoldier = hero;

            vehicleController = transform.Find(VEHICLE_CONTROLER).GetComponent<VehicleController>();
            soldierController = transform.Find(SOLDIER_CONTROLER).GetComponent<SoldierController>();

            soldierController.SetControlledSoldier(currentSoldier);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Transform GetCurrentActiveEntity()
        {
            if (currentSoldier) 
            {
                return currentSoldier.GetComponent<Transform>();
            }
            else if (currentTank)
            {
                return currentTank.GetComponent<Transform>();
            }
            return null;
        }

        public Soldier GetDefaultHero()
        {
            return hero;
        }

        private void RenderInteractionLoading(float interactonLoaderStatus)
        {
            interactionLoader.FillCircleValue(interactonLoaderStatus);
        }

        private void GetInTank(Tank tank)
        {
            currentTank = tank;
            currentSoldier.gameObject.SetActive(false);

            vehicleController.SetControlledTank(currentTank);
            soldierController.SetControlledSoldier(null); 
        }
    }    
}