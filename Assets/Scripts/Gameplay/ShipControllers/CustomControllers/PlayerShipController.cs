using Gameplay.ShipSystems;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers
{
    public class PlayerShipController : ShipController
    {
        [SerializeField] 
        private bl_Joystick Joystick;

        [SerializeField]
        private bool autofire;

        [SerializeField]
        private float _minYBound, _maxYBound, _minXBound, _maxXBound;

        private int HP;
        protected override void ProcessHandling(MovementSystem movementSystem)
        {
#if UNITY_EDITOR
            movementSystem.LateralMovement(Input.GetAxis("Horizontal") * Time.deltaTime);
            movementSystem.LongitudinalMovement(Input.GetAxis("Vertical") * Time.deltaTime);
#endif
            movementSystem.LateralMovement(Joystick.Horizontal / 5f * Time.deltaTime);
            movementSystem.LongitudinalMovement(Joystick.Vertical / 5f * Time.deltaTime);
            //bounds
            var pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, _minXBound, _maxXBound);
            pos.y = Mathf.Clamp(pos.y, _minYBound, _maxYBound);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        protected override void ProcessFire(WeaponSystem fireSystem)
        {
            if (Input.GetKey(KeyCode.Space) || autofire)
            {
                fireSystem.TriggerFire();
            }
        }

        public int getHp() {
            return HP;
        }
    }
}
