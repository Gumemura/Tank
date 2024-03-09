using Units.Vehicle.Tank;
using Controller.Units.Vehicle;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string VEHICLE_CONTROLER = "PlayerVechicleController";
    public Tank currentTank;
    private VehicleController vehicleController;

    // Start is called before the first frame update
    void Start()
    {
        vehicleController = transform.Find(VEHICLE_CONTROLER).GetComponent<VehicleController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTank)
        {
            vehicleController.GetControledTank(currentTank);
        }
    }
}
