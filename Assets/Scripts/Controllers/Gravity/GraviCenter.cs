using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenter : Gravitator
{
    [SerializeField] float distanceFromCamera = 10f;
    [SerializeField] int energyCost = 100;

    private bool isSearchingPlace;
    private KeyCode invertKey = KeyCode.LeftAlt;

    public delegate void GraviCenterConditionAction(GraviCenter graviCenter);
    public static event GraviCenterConditionAction OnPlacedGC;
    public static event GraviCenterConditionAction OnDeletedGC;
    public int EnergyCost { get { return energyCost; } }
    public int EnergyPerSecondCost { get { return energyCost / 100; } }

    protected override void Start()
    {
        base.Start();

        MaterialChanger.SetTransparency(gameObject);
        Animator.RotateRings(transform);
        Animator.ScaleAppear(transform);

        isSearchingPlace = true;

        if (Input.GetKey(invertKey))
        {
            GravityPower = -Mathf.Abs(GravityPower);
            MaterialChanger.InvertZoneDirection(gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (isSearchingPlace)
        {
            if (Input.GetKeyDown(invertKey))
            {
                GravityPower = -Mathf.Abs(GravityPower);
                MaterialChanger.InvertZoneDirection(gameObject);
            }
            else if (Input.GetKeyUp(invertKey))
            {
                GravityPower = Mathf.Abs(GravityPower);
                MaterialChanger.InvertZoneDirection(gameObject);
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetGC();
                return;
            }
            MoveToCursorFloorPosition();
            return;
        }
        /*if (Input.GetMouseButtonDown(0) && !Input.GetKey(invertKey) //move already placed GC
            && RaycastTracker.GetPointerObject("GC") == gameObject
            && PlayShortcutHandler.SelectedGC == null)
        {
            //move a standing GC
            isSearchingPlace = true;
            IsGravitate = false;
        }*/
    }
    private void OnDestroy()
    {
        if (!isSearchingPlace && GameManager.Instance.GameState == GameStates.Playing)
        {
            OnDeletedGC?.Invoke(this);
            GameManager.Instance.CurrentLevel.GCs.Remove(gameObject);
            GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(transform.position));
        }
    }

    private void MoveToCursorFloorPosition()
    {
        GameObject floor = RaycastTracker.GetPointerObject("Floor");

        if (floor == null)
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition.z = distanceFromCamera;
            Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = worldPosition;
        }
        else if (GameManager.Instance.CurrentLevel.Floors.Contains(floor.transform.position))
        {
            transform.position = floor.transform.position;
        }
    }
    private void SetGC()
    {
        GameObject floor = RaycastTracker.GetPointerObject("Floor");

        Level currentLevel = GameManager.Instance.CurrentLevel;

        if (floor != null && currentLevel.EnergyAmount >= energyCost
            && currentLevel.Floors.Contains(floor.transform.position))
        {
            transform.position = floor.transform.position;
            currentLevel.Floors.Remove(floor.transform.position);
            currentLevel.GCs.Add(gameObject);

            MaterialChanger.SetTransparency(gameObject, 1);
            GetComponent<SphereCollider>().enabled = true;

            IsGravitate = true;
            OnPlacedGC?.Invoke(this);

            isSearchingPlace = false;
        }
        else
        {
            //cancel GC selecting 
            Destroy(gameObject);
        }
    }
}
