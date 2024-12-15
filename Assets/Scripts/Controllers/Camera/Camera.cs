using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float minVertAngle = 30f;
    [SerializeField] float maxVertAngle = 60f;

    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoomSize = 3f;
    [SerializeField] private float maxZoomSize = 6f;

    private GameObject ball;
    private float verticalRotation = 0f;
    private float distanceToBall;
    private bool isPlay = false;

    private void OnEnable()
    {
        BallController.OnBallCreated += SetBall;
    }
    private void OnDisable()
    {
        BallController.OnBallCreated -= SetBall;
    }

    private void LateUpdate()
    {
        if (isPlay)
        {
            if (Input.GetMouseButton(1))
            {
                RotateCameraAroundBall();
            }
            transform.position = ball.transform.position + offset;
            transform.LookAt(ball.transform);
            ZoomCamera();
        }
    }
    private void SetBall(Transform ballTransform)
    {
        ball = ballTransform.gameObject;
        distanceToBall = offset.magnitude;
        transform.position = ball.transform.position + offset;
        isPlay = true;
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

        UnityEngine.Camera.main.orthographicSize -= scrollInput * zoomSpeed;
        UnityEngine.Camera.main.orthographicSize = Mathf.Clamp(UnityEngine.Camera.main.orthographicSize, minZoomSize, maxZoomSize);
    }
}
