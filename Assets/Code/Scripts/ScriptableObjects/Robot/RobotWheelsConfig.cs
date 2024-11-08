using UnityEngine;

namespace Code.Gameplay.PlayerVehicle
{
    [CreateAssetMenu(fileName = "NewRobotWheelsConfig", menuName = "RobotMovementConfig/Wheels")]
    public class RobotWheelsConfig : ScriptableObject
    {
        [Header("Main")]
        [SerializeField] private float wheelMass;
        public float WheelMass => wheelMass;

        [SerializeField] private float wheelDampingRate;
        public float WheelDampingRate => wheelDampingRate;

        [SerializeField] private float suspensionDistance;
        public float SuspensionDistance => suspensionDistance;

        [SerializeField] private float forceAppPointDistance;
        public float ForceAppPointDistance => forceAppPointDistance;

        [Header("Suspension Spring")]
        [SerializeField] private float spring;
        public float Spring => spring;

        [SerializeField] private float damper;
        public float Damper => damper;

        [SerializeField] private float targetPosition;
        public float TargetPosition => targetPosition;

        [Header("Forward friction")]

        [SerializeField] private float extremumSlip_F;
        public float ExtremumSlip_F => extremumSlip_F;

        [SerializeField] private float extremumValue_F;
        public float ExtremumValue_F => extremumValue_F;

        [SerializeField] private float asymptoteSlip_F;
        public float AsymptoteSlip_F => asymptoteSlip_F;

        [SerializeField] private float asymptoteValue_F;
        public float AsymptoteValue_F => asymptoteValue_F;

        [SerializeField] private float stiffness_F;
        public float Stiffness_F => stiffness_F;

        [Header("Sideways friction")]

        [SerializeField] private float extremumSlip_S;
        public float ExtremumSlip_S => extremumSlip_S;

        [SerializeField] private float extremumValue_S;
        public float ExtremumValue_S => extremumValue_S;

        [SerializeField] private float asymptoteSlip_S;
        public float AsymptoteSlip_S => asymptoteSlip_S;

        [SerializeField] private float asymptoteValue_S;
        public float AsymptoteValue_S => asymptoteValue_S;

        [SerializeField] private float stiffness_S;
        public float Stiffness_S => stiffness_S;
    }
}
