using System.Collections;
using System.Collections.Generic;
using TWCore.Variables;
using UnityEngine;

public class ParallaxAnimator : MonoBehaviour
{
    #region BLACKBOARD VARIABLES
    [Header("Movement Variables")]
    [SerializeField] private Vector3Reference CenterPosition;
    [SerializeField] private Vector3Reference MovementVelocity;
    #endregion

    [Header("Parent Objects")]
    public Transform farObjects;
    public Transform middleObjects;
    public Transform nearObjects;

    [Header("Parent Speeds")]
    public float farSpeed = 0.5f;
    public float middleSpeed = 1f;
    public float nearSpeed = 2f;

    [Header("Area Size")]
    public float areaSide;

    private List<(Transform, float)> farList;
    private List<(Transform, float)> middleList;
    private List<(Transform, float)> nearList;

    public Bounds areaBounds => new Bounds(transform.position, new Vector3(areaSide, areaSide, 1f));

    private void Awake()
    {
        farList = new();
        foreach (Transform child in farObjects)
        {
            farList.Add((child, Random.Range(0.95f, 1.05f)));
        }

        middleList = new();
        foreach (Transform child in middleObjects)
        {
            middleList.Add((child, Random.Range(0.95f, 1.05f)));
        }

        nearList = new();
        foreach (Transform child in nearObjects)
        {
            nearList.Add((child, Random.Range(0.95f, 1.05f)));
        }
    }

    private void Update()
    {
        transform.position = CenterPosition;

        farList.ForEach((t) => Move(
            t.Item1,
            -farSpeed * t.Item2 * MovementVelocity,
            areaBounds,
            Time.deltaTime
        ));

        middleList.ForEach((t) => Move(
            t.Item1,
            -middleSpeed * t.Item2 * MovementVelocity,
            areaBounds,
            Time.deltaTime
        ));

        nearList.ForEach((t) => Move(
            t.Item1,
            -nearSpeed * t.Item2 * MovementVelocity,
            areaBounds,
            Time.deltaTime
        ));
    }

    private void Move(Transform t, Vector3 v, Bounds b, float deltaTime)
    {
        t.position += deltaTime * v;

        // is the image inside the area
        if (b.Contains(t.position)) return;

        var r = new Ray(t.position - deltaTime * v, v);
        b.IntersectRay(r, out float distance);
        t.position = r.GetPoint(distance * 0.99f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSide, areaSide, 1f));
    }
}
