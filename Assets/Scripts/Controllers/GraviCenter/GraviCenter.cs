﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GraviCenter : Gravitator
{
    private bool isSearchingPlace;
    private int energyExplosion;
    private float secondsToReduseEnergy;

    [SerializeField] float distanceFromCamera = 10f;
    [SerializeField] int lifeTime = 10;
    [SerializeField] int energyCost = 100;

    public delegate void ChangeEnergyAction(int energyValue);
    public static event ChangeEnergyAction OnChangeEnergy;

    public delegate void GraviCenterConditionAction();
    public static event GraviCenterConditionAction OnPlacedGC;

    protected override void Start()
    {
        base.Start();

        MaterialChanger.SetTransparency(gameObject);
        FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject);

        isSearchingPlace = true;
        energyExplosion = energyCost / 2;
        secondsToReduseEnergy = 100f / energyCost;
    }

    protected override void Update()
    {
        base.Update();

        if (isSearchingPlace)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GravityPower = -Mathf.Abs(GravityPower);
                MaterialChanger.InvertZoneDirection(gameObject);
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
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
        }

        #region GC removing

        //Alt + left mouse click => destroy this GC
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RaycastTracker.GetPointerObject("GC") == gameObject)
                {
                    GameManager.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(transform.position));
                    FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
                    OnChangeEnergy?.Invoke(energyExplosion);

                    GameManager.CurrentLevel.GCs.Remove(gameObject.transform);
                    Destroy(gameObject);
                }
            }
        }
        #endregion
    }


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
        else if (GameManager.CurrentLevel.Floors.Contains(floor.transform.position))
        {
            SetPositionGC(floor.transform);
        }
    }
    private void SetGC()
    {
        GameObject floor = RaycastTracker.GetPointerObject("Floor");

        Level currentLevel = GameManager.CurrentLevel;

        if (floor != null && currentLevel.EnergyAmount >= energyCost
            && currentLevel.Floors.Contains(floor.transform.position))
        {
            SetPositionGC(floor.transform);
            currentLevel.Floors.Remove(floor.transform.position);
            currentLevel.GCs.Add(gameObject.transform);

            MaterialChanger.SetTransparency(gameObject, 1);
            GetComponent<SphereCollider>().enabled = true;
            FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);

            IsGravitate = true;
            OnPlacedGC?.Invoke();
            OnChangeEnergy?.Invoke(-energyCost);
            StartCoroutine(EnergyReductionTimer());
        }
        else
        {
            //cancel GC selecting 
            Destroy(gameObject);
        }
        FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
        isSearchingPlace = false;
    }

    private IEnumerator EnergyReductionTimer()
    {
        while (true)
        {
            if (GameManager.CurrentLevel.EnergyAmount <= 0)
            {
                yield break;
            }
            OnChangeEnergy?.Invoke(-1);

            yield return new WaitForSeconds(secondsToReduseEnergy);
        }
    }
    private void SetPositionGC(Transform floorTransform) => transform.position = floorTransform.position;

}
