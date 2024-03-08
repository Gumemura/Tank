using UnityEngine;
using Units.Vehicle;
using Game.Core;
using Units.Resources.Projectile;
using System.Collections;

namespace Units.Vehicle.Tank
{
    public class Tank : Vehicle
    {
        private const int INVERTER = -1;
        private const float CANNON_RECOIL = .35f;
        private const float CANNON_RECOIL_SPEED = .6f;
        private const float TANK_RECOIL_SPEED = -.4f;
        private const float TANK_RECOIL_TIME = .05f;
        private const float ACCURACY_PENALTY_RANGE =.3f;
        private const string TURRET_NAME = "turret";
        private const string TRACKS_NAME = "tracks";
        private const string CANNON_NAME = "cannon";
        private const string TRACK_LEFT_NAME = "trackLeft";
        private const string TRACK_RIGHT_NAME = "trackRight";
        private const string SHOOT_EFFECT_NAME = "shootEffect";

        private Transform turret;
        private Transform tracks;
        private Transform cannon;
        private Animator shootEffect;
        private float shootInstant = int.MinValue;
        private float turretRotationOnShoot;
        private float cannonElevation = 10;
        private float cannonElevationChangeRate = 1;
        public float accuracy = .2f;
        private Vector2 accuracyCap = new Vector2(.1f, .9f);
        private Vector2 cannonCapElevation = new Vector2(4, 20);

        private Vector3 cannonOriginalLocalPos;
        private Vector3 aimingArea;
        private bool cannonReady = true;
        private bool projectileLoaded = false;


        [SerializeField]
        private float turretTurningSpeed;

        [SerializeField]
        private Projectile loadedProjectile;

        void Start()
        {
            base.Start();
            turret = transform.Find(TURRET_NAME);
            tracks = transform.Find(TRACKS_NAME);
            cannon = turret.Find(CANNON_NAME);
            shootEffect = turret.Find(SHOOT_EFFECT_NAME).GetComponent<Animator>();
        }

        void Update()
        {
            base.Update();
            ChangeCannonHeight();
            DrawLineOfFire();
            RotateTurret();
            Fire();
        }

        public override void Steer()
        {
            transform.rotation = GameDomain.NewRotation(transform, Input.GetAxis("Horizontal") * stearingSpeed, INVERTER);
        }

        private void RotateTurret()
        {
            turret.rotation = GameDomain.NewRotation(turret, Input.GetAxisRaw("AltHorizontal") * turretTurningSpeed, INVERTER);
        }

        public override void MoveFowardAnimation(bool activateMovimentAnimation)
        {
            if(hasMovimentAnimation)
            {
                Transform trackLeft = tracks.Find(TRACK_LEFT_NAME);
                Transform trackRight = tracks.Find(TRACK_RIGHT_NAME);
                
                trackLeft.GetComponent<Animator>().SetBool("isTankMoving", activateMovimentAnimation);
                trackRight.GetComponent<Animator>().SetBool("isTankMoving", activateMovimentAnimation);
            }
        }

        private void DrawLineOfFire()
        {
            LineRenderer lineRenderer = turret.GetComponent<LineRenderer>();

            lineRenderer.SetPosition(0, turret.position);
            lineRenderer.SetPosition(1, aimingArea);
        }

        private void ChangeCannonHeight()
        //This method also calculates the aiming point (algo used to draw the aiming line)
        {
            cannonElevation += Input.GetAxisRaw("AltVertical") * cannonElevationChangeRate * Time.deltaTime;
            cannonElevation = Mathf.Clamp(cannonElevation, cannonCapElevation.x, cannonCapElevation.y);

            Vector3 referencePoint = turret.position;
            Vector3 direction = (cannon.position - turret.position).normalized;
            Vector3 displacement = direction * cannonElevation;
            aimingArea = referencePoint + displacement;
        }

        private void Fire()
        {
            if (Input.GetKeyDown(KeyCode.Space) && cannonReady)
            {
                cannonOriginalLocalPos = cannon.localPosition;
                shootInstant = Time.time;
                cannonReady = false;
                ShootProjectile();
                CannonRecoil();
                TriggerShootEffect();
            }
            ReturnCannon();
            TankRecoilFromShoot();
        }

        public void ShootProjectile()
        {
            float newTargetRange = (1 - accuracy) * (ACCURACY_PENALTY_RANGE * Mathf.Abs(cannonElevation));

            aimingArea += new Vector3(Random.Range(-newTargetRange, newTargetRange),
                                      Random.Range(-newTargetRange, newTargetRange),
                                      0);
            Projectile projectileFired = Instantiate(loadedProjectile);
            projectileFired.SetProjectile(transform.position, aimingArea);
        }

        private void CannonRecoil()
        {
            cannon.localPosition = Vector3.Lerp(cannon.localPosition, cannon.localPosition - (Vector3.up * CANNON_RECOIL), 1);
        }

        private void TriggerShootEffect()
        {
            shootEffect.SetTrigger("triggerShootEffect");
        }

        private void TankRecoilFromShoot()
        {
            turretRotationOnShoot = turret.localEulerAngles.z;
            if (Time.time < shootInstant + TANK_RECOIL_TIME)
            {
                MoveFoward(TANK_RECOIL_SPEED * Time.deltaTime * Mathf.Cos(turretRotationOnShoot * (Mathf.PI/180)));
            }
        }

        private void ReturnCannon()
        {
            if (cannon.localPosition != cannonOriginalLocalPos && !cannonReady)
            {
                cannon.localPosition = Vector3.MoveTowards(cannon.localPosition, cannonOriginalLocalPos, CANNON_RECOIL_SPEED * Time.deltaTime);
            }
            else
            {
                cannonReady = true;
            }
        }
    }
}