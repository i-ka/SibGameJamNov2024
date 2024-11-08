using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.PlayerVehicle
{
    [CreateAssetMenu(fileName = "NewRobotControllerConfig" , menuName = "RobotMovementConfig/Controller")]

    public class RobotControllerConfig : ScriptableObject
    {
        [Header("Steering")]
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float steeringSpeed;

        [Header("Acceleration")]
        [SerializeField] private float maxForwardSpeedInKmpH;
        [SerializeField] private float maxBackSpeedInKmpH;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float gasAccelerationRate;

        [Header("Brakes")]
        [SerializeField] private float maxBrakingForce;

        // properties
        public float MaxSteerAngle => maxSteerAngle;
        public float SteeringSpeed => steeringSpeed;

        public float MaxForwardSpeed => maxForwardSpeedInKmpH / 3.6f;    
        public float MaxBackSpeed => maxBackSpeedInKmpH / 3.6f;
        public float MaxMotorTorque => maxMotorTorque;
        public float GasAccelerationRate => gasAccelerationRate;

        public float MaxBrakingForce => maxBrakingForce;
    }
}

