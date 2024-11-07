using UnityEngine;
using System.Collections.Generic;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Gameplay.PlayerVehicle
{
    public class MovementController : MonoBehaviour
    {
        #region Components

        [Header("Basic Components")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform com;
        public Transform teleportPointAfterIncline;

        [Header("Custom Components")]
        [SerializeField] private WheelViewController wheelViewController;

        #endregion

        #region Variables

        [Header("Input")]
        private Vector2 driveAxis;

        [Header("Main")]
        private WheelCollider[] wheels;

        private float velocityAngle;
        private float currentSpeed;
        private int moveDirection;

        [Header("Steering")]
        // customizable
        private float maxSteerAngle;
        private float steeringSpeed;
        // auto
        private float targetSteerAngle;
        private float currentSteerAngle;

        [Header("Acceleration")]
        // customizable
        private float maxForwardSpeed;
        private float maxBackSpeed;
        private const float maxWheelRPM = 9000;
        private float maxMotorTorque;
        [SerializeField] private float gasAccelerationRate;
        // auto
        private float currentMotorTorque;
        private float currentGasAngle;

        [Header("Brakes")]
        private float maxBrakingForce;
        private float currentBrake;

        [Header("Other")]
        private float currentInclineAngle;
        [SerializeField] private float inclineAngleToTeleport;


        private bool isPreparingToTeleport = false;
        bool isInclined = false;

        private IEnumerator lastRoutine = null;

        #endregion

        #region Properties

        public Transform GetPosition => rb.transform;
        public int GetMoveDirection => moveDirection;
        public float GetSpeedInKmpH => currentSpeed * 3.6f;
        public float MaxForwardSpeedInKmpH => maxForwardSpeed;

        #endregion

        #region Initialize

        public void Init(RobotControllerConfig config)
        {
            if (CheckComponents())
            {
                Debug.Log($"{gameObject.name} initialized successfully!");
            }
            else
            {
                Debug.LogError($"{gameObject.name} can't initialize!");

#if UNITY_EDITOR
                EditorApplication.isPaused = true;
#endif
            }

            teleportPointAfterIncline = GameObject.FindGameObjectWithTag("PositionToRestart").transform;
            if (com) rb.centerOfMass = com.localPosition;
            wheelViewController.Init();
            SetWheels(wheelViewController.WheelsColliders);
            SetParametersFromConfig(config);
        }

        private bool CheckComponents()
        {
            if (!rb)
            {
                Debug.LogError($"No Rigidbody in {gameObject.name}");
                return false;
            }
            if (!wheelViewController)
            {
                Debug.LogError($"No {wheelViewController} in {gameObject.name}");
                return false;
            }

            return true;
        }

        private void SetWheels(WheelCollider[] wheels)
        {
            this.wheels = wheels;
        }

        private void SetParametersFromConfig(RobotControllerConfig config)
        {
            maxSteerAngle = config.MaxSteerAngle;
            steeringSpeed = config.SteeringSpeed;
            maxForwardSpeed = config.MaxForwardSpeed;
            maxBackSpeed = config.MaxBackSpeed;
            maxMotorTorque = config.MaxMotorTorque;
            gasAccelerationRate = config.GasAccelerationRate;
            maxBrakingForce = config.MaxBrakingForce;
        }

        #endregion

        #region Update Methods

        public void UpdateController()
        {
            wheelViewController.UpdateWheelsTransform();
            VelocityControl();
        }

        public void SetInput(Vector2 driveAxis)
        {
            this.driveAxis = driveAxis;
        }

        private void VelocityControl()
        {
            velocityAngle = -Vector3.SignedAngle(rb.velocity, rb.transform.TransformDirection(Vector3.forward), Vector3.up);
            currentSpeed = rb.velocity.magnitude;
            moveDirection = currentSpeed < 0.1f ? 0 : (velocityAngle < 90 && velocityAngle > -90 ? 1 : -1);

            if (rb.velocity.magnitude > maxForwardSpeed && moveDirection == 1)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxForwardSpeed);
            }
            else if (rb.velocity.magnitude > maxBackSpeed && moveDirection <= 0)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxBackSpeed);
            }
        }

        public void PositionAndRotationController()
        {
            currentInclineAngle = Vector3.Angle(rb.transform.up, Vector3.up);

            if (currentInclineAngle > inclineAngleToTeleport)
            {
                isInclined = true;
            }
            else
            {
                isInclined = false;
                isPreparingToTeleport = false;
                lastRoutine = PrepareToTeleport();
                StopCoroutine(lastRoutine);
                StopAllCoroutines();
            }

            if (isPreparingToTeleport == false && isInclined == true)
            {
                StartCoroutine(PrepareToTeleport());
            }

            if (isInclined == false)
            {
                lastRoutine = PrepareToTeleport();
                StopCoroutine(lastRoutine);
                StopAllCoroutines();
                isPreparingToTeleport = false;
            }

        }

        private IEnumerator PrepareToTeleport()
        {

            isPreparingToTeleport = true;
            int prepareTime = 3;
            int time = prepareTime;

            while (time > 0)
            {
                time -= 1;
                yield return new WaitForSeconds(1);
            }


            TeleportPlayer(teleportPointAfterIncline);
            isPreparingToTeleport = false;
        }

        public void TeleportPlayer(Transform teleportPoint)
        {
            isInclined = false;
            isPreparingToTeleport = false;
            lastRoutine = PrepareToTeleport();
            StopCoroutine(lastRoutine);
            StopAllCoroutines();
            rb.position = teleportPoint.position;
            rb.rotation = teleportPoint.rotation;
            rb.velocity = Vector3.zero;

            foreach (var wheel in wheels)
            {
                wheel.brakeTorque = 10000;
            }
        }

        #endregion

        #region Fixed Update Methods

        public void Acceleration()
        {
            currentGasAngle = driveAxis.x;

            if (currentGasAngle != 0)
            {
                currentBrake = 0;
                currentMotorTorque = Mathf.Lerp(currentMotorTorque, Mathf.Sign(currentGasAngle) * maxMotorTorque, gasAccelerationRate * Time.deltaTime);
            }
            else if (moveDirection != 0 && Mathf.Approximately(0, currentGasAngle) || moveDirection == 0 && Mathf.Approximately(0, currentGasAngle))
            {
                currentMotorTorque = 0;
                // autobrake
                // currentBrake = 0.01f * brakingForce;
            }

            if (Mathf.Sign(moveDirection) != Mathf.Sign(currentGasAngle) && moveDirection != 0)
            {
                currentMotorTorque = 0;
                currentBrake = Mathf.Abs(currentGasAngle * maxBrakingForce);
            }

            if (moveDirection == 0 && Mathf.Approximately(0, currentSpeed))
            {
                currentMotorTorque = 0;
            }

            WheelControl(currentMotorTorque, currentBrake);
        }

        private void WheelControl(float torque, float brake)
        {
            for (int i = 0; i < wheels.Length; ++i)
            {
                //Debug.Log(brake);
                if (Mathf.Abs(wheels[i].rpm) <= maxWheelRPM)
                {
                    wheels[i].motorTorque = torque;
                    
                }
                else
                {
                    wheels[i].motorTorque = 0;
                    wheels[i].rotationSpeed = maxWheelRPM;
                }
                wheels[i].brakeTorque = brake;
            }
        }

        public void Steering()
        {
            targetSteerAngle = driveAxis.y * maxSteerAngle;
            currentSteerAngle = Mathf.MoveTowards(currentSteerAngle, targetSteerAngle, steeringSpeed * Time.deltaTime);

            // mirror rotation of all wheels
            for (int i = 0; i < wheels.Length; ++i)
            {
                WheelCollider wheel = wheels[i];

                if (wheel.transform.localPosition.z > 0)
                {
                    wheel.steerAngle = currentSteerAngle;
                }
                else
                {
                    wheel.steerAngle = -currentSteerAngle;
                }
            }
        }

        public void UpgradeMaxForwardSpeed(float speedToAdd)
        {
            maxForwardSpeed += speedToAdd;
        }

        #endregion

        #region Editor Methods

        private void OnValidate()
        {
            wheelViewController ??= GetComponentInChildren<WheelViewController>();
        }

        #endregion
    }
}