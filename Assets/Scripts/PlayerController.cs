using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    private Vector3 movementDirection;

    [SerializeField] private float speed = 3f;

    private bool canThowBall = false;


    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform point;
    [SerializeField] private LayerMask layer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int lineSegment;

    private void Start()
    {
        lineRenderer.positionCount = lineSegment;
    }

    private void Update()
    {
        ApplyMovement();

        if(canThowBall)
            DisplayTrajectory();
    }


    private void ApplyMovement()
    {
        movementDirection = new Vector3(0, 0, 1);
        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * Time.deltaTime * speed);
        }
    }

    #region[======= Line Renderer =========]

    private void DisplayTrajectory()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layer))
        {
            Debug.Log("Hit some thing");
            Vector3 velocity = CalculateVelocity(hitInfo.point, point.position, 1f);

            Visualize(velocity);

            transform.rotation = Quaternion.LookRotation(velocity);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(rb, point.position + new Vector3(0f, 0f, 3f), Quaternion.identity);
                obj.velocity = velocity;
            }
        }

    }

    private Vector3 CalculateVelocity(Vector3 _target, Vector3 _origin, float _time)
    {
        Vector3 distance = _target - _origin;


        float xzVelocity = distance.magnitude * _time;
        float yVelocity = (distance.y / _time) + (0.5f * Mathf.Abs(Physics.gravity.y) * _time);

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

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        speed = 0;
        canThowBall = true;
    }



}
