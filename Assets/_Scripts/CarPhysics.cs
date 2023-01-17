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
    [SerializeField] private float tireGripFactor = 0.5f;
    [SerializeField] private float tireMass = 0.5f;
    [SerializeField] private float maxTirenAngleRotationInDegrees = 33f;
    [SerializeField] private Transform frontLeftTire;
    [SerializeField] private Transform frontRightTire;
    [SerializeField] private Transform backLeftTire;
    [SerializeField] private Transform backRightTire;

    private void OnValidate()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var gasAndBreak = Input.GetAxis("Vertical");
        var inputRotation = Input.GetAxis("Horizontal");

        Debug.Log(inputRotation);

        frontLeftTire.forward = carRigidBody.transform.forward;
        frontRightTire.forward = carRigidBody.transform.forward;

        frontLeftTire.localRotation = Quaternion.Euler(0f, maxTirenAngleRotationInDegrees * inputRotation, 0f);
        frontRightTire.localRotation = Quaternion.Euler(0f, maxTirenAngleRotationInDegrees * inputRotation, 0f);
    }

    private void FixedUpdate()
    {
        CalculateAllTireForces(frontLeftTire, true, true, true);
        CalculateAllTireForces(frontRightTire, true, true, true);
        CalculateAllTireForces(backLeftTire, true, true, false);
        CalculateAllTireForces(backRightTire, true, true, false);
    }

    private void CalculateAllTireForces(Transform tireTransform, bool suspensionEnabled,
        bool steeringEnabled, bool torqueEnabled)
    {
        var down = tireTransform.TransformDirection(Vector3.down);
        var rayDidHit = Physics.Raycast(tireTransform.position, 2 * down * suspensionResDistance,
            out RaycastHit wheelRaycastHit);

        if (isDebugEnabled) Debug.DrawRay(tireTransform.position, down * suspensionResDistance, Color.green);
        if (!rayDidHit) return;

        var tireWorldVelocity = carRigidBody.GetPointVelocity(tireTransform.position);
        if (suspensionEnabled) CalculateTireSuspension(tireTransform, tireWorldVelocity, wheelRaycastHit);
        if (steeringEnabled) CalculateTireSteering(tireTransform, tireWorldVelocity);
    }


    private void CalculateTireSuspension(Transform tireTransform, Vector3 tireWorldVelocity,
        RaycastHit wheelRaycastHit)
    {
        // world-space direction of the spring force
        var springDirection = tireTransform.up;
        // calculate offset from the raycast
        float offset = suspensionResDistance - wheelRaycastHit.distance;
        // calculate velocity along the spring direction
        // note that springDirection is a unit vector, so this returns the magnitude of 
        // wheelWorldVelocity as projected onto springDirection
        float velocity = Vector3.Dot(springDirection, tireWorldVelocity);
        // calculate the magnitude of the dampened spring force
        float force = (offset * springStrength) - (velocity * springDamper);

        // apply the force at the location of this tire, int the direction of the suspension
        carRigidBody.AddForceAtPosition(springDirection * force, tireTransform.position);
    }

    private void CalculateTireSteering(Transform tireTransform, Vector3 tireWorldVelocity)
    {
        // world-space direction of the steering force
        var steeringDirection = tireTransform.right;
        // what is the tire's velocity in the steering direction?
        // note that steeringDirection is a unit vector, so this returns the magnitude of
        // wheelWorldVelocity as projected onto steeringDirection
        float steeringVelocity = Vector3.Dot(steeringDirection, tireWorldVelocity);
        // the change in velocity that we're looking for is -steeringVelocity * gripFactor
        // gripFactor is in range 0-1, being 1 full grip and 0 no grip at all
        float desiredVelocityChange = -steeringVelocity * tireGripFactor;
        // turn change in velocity into an acceleration (acceleration = change in vel / time)
        // this will produce the acceleration necessary to change the velocity by 
        // desiredVelocityChange in 1 physics step
        float desiredAcceleration = desiredVelocityChange / Time.fixedDeltaTime;

        // Force = Mass * Acceleration
        carRigidBody.AddForceAtPosition(steeringDirection * tireMass * desiredAcceleration,
            tireTransform.position);
    }
}
