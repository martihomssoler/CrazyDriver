using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarPhysics : MonoBehaviour
{
    public bool isDebugEnabled = true;

    [SerializeField] private Rigidbody carRigidBody;
    [SerializeField] private float springStrength = 100f;
    [SerializeField] private float springDamper = 15f;
    [SerializeField] private float suspensionResDistance = 0.5f;
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform backLeftWheel;
    [SerializeField] private Transform backRightWheel;

    private void OnValidate()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // calculate suspension for all 4 wheels
        CalculateWheelSuspension(frontLeftWheel);
        CalculateWheelSuspension(frontRightWheel);
        CalculateWheelSuspension(backLeftWheel);
        CalculateWheelSuspension(backRightWheel);
    }

    private void CalculateWheelSuspension(Transform wheel)
    {
        var down = wheel.TransformDirection(Vector3.down);
        var rayDidHit = Physics.Raycast(wheel.position, down * suspensionResDistance, out RaycastHit wheelRay);

        if (isDebugEnabled) Debug.DrawRay(wheel.position, down * suspensionResDistance, Color.green);

        if (!rayDidHit) return;

        // world-space direction of the spring force
        var springDirection = wheel.up;
        // world-space velocity of this wheel
        var wheelWorldVelocity = carRigidBody.GetPointVelocity(wheel.position);
        // calculate offset from the raycast
        float offset = suspensionResDistance - wheelRay.distance;
        // calculate velocity along the spring direction
        // note that springDirection is a unit vector, so this returns the magnitude of 
        // wheelWorldVelocity as projected onto springDirection
        float velocity = Vector3.Dot(springDirection, wheelWorldVelocity);
        // calculate the magnitude of the dampened spring force
        float force = (offset * springStrength) - (velocity * springDamper);

        // apply the force at the location of this tire, int the direction of the suspension
        carRigidBody.AddForceAtPosition(springDirection * force, wheel.position);
    }
}
