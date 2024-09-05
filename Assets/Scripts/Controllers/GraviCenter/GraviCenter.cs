using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenter : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rbPlayer;
    private bool isSearchingPlace;
    private float transparency = 0.1f;
    private int energyExplosion;

    [SerializeField] float gravityPower = 20f;
    [SerializeField] float gravityZone = 10f;
    [SerializeField] float distanceFromCamera = 10f;
    [SerializeField] int energyCost = 100;

    public float GravityZone { get { return gravityZone; } set { gravityZone = value; } }
    public bool IsRepeals { get; set; }

    public delegate void GraviCenterAction(int energyValue);
    public static event GraviCenterAction OnGraviCenterDestroyed;
    public static event GraviCenterAction OnGraviCenterCreated;

    private void Start()
    {
        player = FindObjectOfType<BallController>().gameObject;
        rbPlayer = player.GetComponent<Rigidbody>();
        MaterialChanger.SetTransparency(gameObject, transparency);        
        FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject);

        isSearchingPlace = true;
        energyExplosion = energyCost / 2;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            IsRepeals = true;
            gravityPower *= -1;
            MaterialChanger.InvertZoneDirection(gameObject);
        }
    }

    private void Update()
    { 
        if (isSearchingPlace)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SetGC();
                return;
            }
            MoveToCursorFloorPosition();
        }
        else if (GetDistanceToBall() < GravityZone)
        {
            MoveBall();
        }

        //Alt + left mouse click => destroy this GC
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetMouseButton(0))
            {
               if (RaycastTracker.GetPointerObject("GC") == gameObject)
                {
                    GameManager.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(transform.position));
                    FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
                    OnGraviCenterDestroyed?.Invoke(energyExplosion);
                    Destroy(gameObject);                    
                }
            }
        }
    }

    public float GetDistanceToBall() => (transform.position - player.transform.position).magnitude * 2;
    private void MoveToCursorFloorPosition()
    {
        GameObject floor = RaycastTracker.GetPointerObject("Floor");

        if (floor == null) 
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition.z = distanceFromCamera;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = worldPosition;
        }
        else if(GameManager.CurrentLevel.Floors.Contains(floor.transform.position))
        {
            SetPositionGC(floor.transform);
        }
    }
    private void SetGC()
    {
        GameObject floor = RaycastTracker.GetPointerObject("Floor");

        LevelManager currentLevel = GameManager.CurrentLevel;

        if (floor != null && currentLevel.EnergyAmount >= energyCost
            && currentLevel.Floors.Contains(floor.transform.position))
        {
            SetPositionGC(floor.transform);
            currentLevel.Floors.Remove(floor.transform.position);
            OnGraviCenterCreated?.Invoke(-energyCost);
            MaterialChanger.SetTransparency(gameObject, 1);
            GetComponent<SphereCollider>().enabled = true;
            FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
        }
        else
        {
            //cancel GC selecting 
            Destroy(gameObject);
        }
        FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
        isSearchingPlace = false;
    }
    private void MoveBall()
    {
        //the closer from the ball to GC, the stronger gravitation 
        float powerDivider = 1 - GetDistanceToBall() / gravityZone;
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rbPlayer.AddForce(direction * gravityPower * powerDivider);
    }
    private void SetPositionGC(Transform floorTransform) => transform.position = floorTransform.position;

}
