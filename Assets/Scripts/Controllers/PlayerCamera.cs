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
    private float currentZoomDistance;

    private List<XrayTarget> targets;

    private void Start()
    {
        targets = new List<XrayTarget>();
        targets.Add(new XrayTarget(ball));

        transform.position = ball.transform.position + offset;
        currentZoomDistance = Vector3.Distance(transform.position, ball.transform.position);
    }

    private void LateUpdate()
    {
        UpdateTargets(null);

        if (Input.GetMouseButton(2))
        {
            RotateCameraAroundObject();
        }
        transform.LookAt(ball.transform);
        ZoomCamera();
    }

    private void RotateCameraAroundObject()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        transform.RotateAround(ball.transform.position, Vector3.up, horizontalInput * rotationSpeed);

        verticalRotation -= verticalInput * rotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, minVertAngle, maxVertAngle);
        transform.position = new Vector3(transform.position.x, ball.transform.position.y + verticalRotation, transform.position.z);
    }
    private void ZoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize -= scrollInput * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoomSize, maxZoomSize);
    }
    public void UpdateTargets(GameObject newTarget, bool shouldDestroy = false)
    {
        if (newTarget != null)
        {
            targets.Add(new XrayTarget(newTarget));
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (shouldDestroy && targets[i].Obj == newTarget)
            {
                targets[i].UpdateObstacles(true);
                targets.RemoveAt(i);
                i--;
                continue;
            }
            targets[i].UpdateObstacles();
        }
    }
}
