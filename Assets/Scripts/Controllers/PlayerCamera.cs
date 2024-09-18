using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Vector3 offset;

    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float minVertAngle = 30f;
    [SerializeField] float maxVertAngle = 60f;

    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoomSize = 3f;
    [SerializeField] private float maxZoomSize = 6f;

    private float verticalRotation = 0f;
    private float distanceToBall;

    private void Start()
    {
        distanceToBall = offset.magnitude;
        transform.position = ball.transform.position + offset;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            RotateCameraAroundBall();
        }
        transform.position = ball.transform.position + offset;
        transform.LookAt(ball.transform);
        ZoomCamera();
    }

    private void RotateCameraAroundBall()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        transform.RotateAround(ball.transform.position, Vector3.up, horizontalInput * rotationSpeed);

        verticalRotation -= verticalInput * rotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, minVertAngle, maxVertAngle);
        transform.position = new Vector3(transform.position.x, ball.transform.position.y + verticalRotation, transform.position.z);

        // ----- For offset.magnitude == distanceToBall ----- //
        Vector3 vectorToBall = ball.transform.position - transform.position;
        float delta = vectorToBall.magnitude - distanceToBall;
        float koef = delta / vectorToBall.magnitude;

        offset = (transform.position - ball.transform.position) + vectorToBall * koef;
    }
    private void ZoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize -= scrollInput * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoomSize, maxZoomSize);
    }
}
