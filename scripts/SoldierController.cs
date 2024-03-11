using Units.Soldier;
using UnityEngine;
using Game.Core;

namespace Controller.Units.SoldierC
{
    public class SoldierController : MonoBehaviour
    {
        private Soldier soldier;

        // Start is called before the first frame update
        void Start()
        {
            soldier = null;
        }

        void FixedUpdate()
        {
            if (soldier)
            {
                soldier.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.LeftShift), Input.GetMouseButton(1));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (soldier)
            {
                GameDomain.RotateWithMouse(soldier.GetComponent<Transform>());
                soldier.DrawLineOfFire(Input.GetMouseButton(1), GameDomain.MousePositionToScreen());
            }
        }

        public void SetControlledSoldier(Soldier activeSoldier)
        {
            soldier = activeSoldier;
        }
    }
}