﻿using DG.Tweening;
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
    private int energyOnDestroy;
    private float secondsToReduseEnergy;
    private KeyCode invertKey = KeyCode.LeftAlt;
    public int EnergyCost { get { return energyCost; } }

    public delegate void ChangeEnergyAction(int energyValue);
    public static event ChangeEnergyAction OnChangeEnergy;

    public delegate void GraviCenterConditionAction(Transform objTransform);
    public static event GraviCenterConditionAction OnPlacedGC;
    public static event GraviCenterConditionAction OnTakedGC;

    protected override void Start()
    {
        base.Start();

        OnTakedGC?.Invoke(transform);
        MaterialChanger.SetTransparency(gameObject);

        isSearchingPlace = true;
        energyOnDestroy = energyCost / 2;
        secondsToReduseEnergy = 100f / energyCost;

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

        if (Input.GetMouseButtonDown(0) && RaycastTracker.GetPointerObject("GC") == gameObject
            && ShortcutManager.SelectedGC == null)
        {
            //move a standing GC
            isSearchingPlace = true;
            IsGravitate = false;
        }
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
            OnPlacedGC?.Invoke(transform);
            OnChangeEnergy?.Invoke(-energyCost);
            StartCoroutine(EnergyReductionTimer());

            isSearchingPlace = false;
        }
        else
        {
            //cancel GC selecting 
            Destroy(gameObject);
        }       
    }

    private IEnumerator EnergyReductionTimer()
    {
        while (true)
        {
            if (GameManager.Instance.CurrentLevel.EnergyAmount <= 0)
            {
                yield break;
            }
            OnChangeEnergy?.Invoke(-1);

            yield return new WaitForSeconds(secondsToReduseEnergy);
        }
    }

    private void OnDestroy()
    {
        if (!isSearchingPlace)
            OnChangeEnergy?.Invoke(energyOnDestroy);
    }
}
