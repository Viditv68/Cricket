using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private Camera cam;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Transform point;
    [SerializeField] private LayerMask layer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int lineSegment;

    private void Start()
    {
        cam = Camera.main;
        lineRenderer.positionCount = lineSegment;
    }

    private void Update()
    {
        DisplayTrajectory();
    }

    private void DisplayTrajectory()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.Log(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layer)) 
        {
            Debug.Log("Hit some thing");
            cursor.SetActive(true);
            cursor.transform.position = hitInfo.point + Vector3.up * 0.1f;

            Vector3 velocity = CalculateVelocity(hitInfo.point, point.position, 1f);

            Visualize(velocity);

            transform.rotation = Quaternion.LookRotation(velocity);

            if(Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(rb, point.position, Quaternion.identity);
                obj.velocity = velocity;
            }
        }

    }

    private Vector3 CalculateVelocity(Vector3 _target, Vector3 _origin, float _time)
    {
        Vector3 distance = _target - _origin;


        float xzVelocity = distance.magnitude * _time;
        float yVelocity = (distance.y / _time) + (0.5f * Mathf.Abs(Physics.gravity.y) * _time); ;

        Vector3 result = distance.normalized;
        result *= xzVelocity;
        result.y = yVelocity;

        return result;
    
    }

    private void Visualize(Vector3 _velocity)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 position = CalculatePositionInTime(_velocity, i / (float)lineSegment);
            lineRenderer.SetPosition(i, position);
        }
    }


    private Vector3 CalculatePositionInTime(Vector3 _velocity, float _time)
    {
        Vector3 xzVelocity = _velocity;
        xzVelocity.y = 0;

        Vector3 result = point.position + _velocity * _time;
        float yPosition = (-0.5f * Mathf.Abs(Physics.gravity.y) * (_time * _time) + (_velocity.y * _time) + point.position.y);


        result.y = yPosition;

        return result;
    }
}
