using UnityEngine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Windows;
using System.Collections.Generic;
using System.Collections;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FS.Gameplay.PlayerVehicle
{
    public class MovementController : MonoBehaviour
    {
        #region Components

        [Header("Basic Components")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform com;
        [SerializeField] private Transform teleportPointAfterIncline;

        [Header("Custom Components")]
        [SerializeField] private WheelViewController wheelViewController;

        #endregion

        #region Variables

        [Header("Input")]
        [SerializeField] private Vector2 driveAxis;

        [Header("Main")]
        [SerializeField] private WheelCollider[] wheels;

        [Header("Stats")]
        [SerializeField] private float velocityAngle;
        [SerializeField] private float currentSpeed;
        [SerializeField] private int moveDirection;

        [Header("Steering")]
        // customizable
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float steeringSpeed;
        // auto
        [SerializeField] private float targetSteerAngle;
        [SerializeField] private float currentSteerAngle;

        [Header("Acceleration")]
        // customizable
        [SerializeField] private float maxForwardSpeedInKmpH;
        [SerializeField] private float maxBackSpeedInKmpH;
        [SerializeField] private float maxWheelRPM;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float gasAccelerationRate;
        // auto
        [SerializeField] private float currentMotorTorque;
        [SerializeField] private float currentGasAngle;

        [Header("Brakes")]
        [SerializeField] private float brakingForce;
        [SerializeField] private float currentBrake;

        [Header("Other")]
        [SerializeField] private float currentInclineAngle;
        [SerializeField] private float inclineAngleToTeleport;


        [SerializeField] private bool isPreparingToTeleport = false;
        [SerializeField] bool isInclined = false;

        private IEnumerator lastRoutine = null;

        #endregion

        #region Properties

        public Transform GetPosition => rb.transform;
        public int GetMoveDirection => moveDirection;
        public float GetSpeedInKmpH => currentSpeed * 3.6f;

        public float MaxForwardSpeedInKmpH => maxForwardSpeedInKmpH;

        #endregion

        #region Initialize

        public void Init()
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

            if (com) rb.centerOfMass = com.localPosition;
            wheelViewController.Init();
            SetWheels(wheelViewController.WheelsColliders);

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

            if (rb.velocity.magnitude > maxForwardSpeedInKmpH && moveDirection == 1)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxForwardSpeedInKmpH);
            }
            else if (rb.velocity.magnitude > maxBackSpeedInKmpH && moveDirection <= 0)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxBackSpeedInKmpH);
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

        private void TeleportPlayer(Transform teleportPoint)
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
                currentBrake = Mathf.Abs(currentGasAngle * brakingForce);
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
            maxForwardSpeedInKmpH += speedToAdd;
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