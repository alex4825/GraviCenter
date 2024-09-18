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
    private bool isSearchingPlace;
    private int energyExplosion;
    private float secondsToReduseEnergy;
    private float speedDepth = 0.2f;

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

        if (Input.GetKey(KeyCode.LeftControl))
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
            return;
        }

        if (Input.GetMouseButtonDown(0) && RaycastTracker.GetPointerObject("GC") == gameObject)
        {
            //Alt + left mouse click => destroy this GC
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                DeleteGC();
                return;
            }

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
            SetPositionGC(floor.transform);
        }
    }
    private void SetGC()
    {
        GameObject floor = RaycastTracker.GetPointerObject("Floor");

        Level currentLevel = GameManager.Instance.CurrentLevel;

        if (floor != null && currentLevel.EnergyAmount >= energyCost
            && currentLevel.Floors.Contains(floor.transform.position))
        {
            SetPositionGC(floor.transform);
            currentLevel.Floors.Remove(floor.transform.position);
            currentLevel.GCs.Add(gameObject.transform);

            MaterialChanger.SetTransparency(gameObject, 1);
            GetComponent<SphereCollider>().enabled = true;

            IsGravitate = true;
            OnPlacedGC?.Invoke();
            OnChangeEnergy?.Invoke(-energyCost);
            StartCoroutine(EnergyReductionTimer());
            FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
        }
        else
        {
            //cancel GC selecting 
            Destroy(gameObject);
        }
        
        isSearchingPlace = false;
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
    private void SetPositionGC(Transform floorTransform) => transform.position = floorTransform.position;

    public void DeleteGC()
    {
        GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(transform.position));
        FindFirstObjectByType<PlayerCamera>().UpdateTargets(gameObject, true);
        OnChangeEnergy?.Invoke(energyExplosion);

        GameManager.Instance.CurrentLevel.GCs.Remove(gameObject.transform);

        if (IsAttracts)
        {
            transform.DOScale(Vector3.zero, speedDepth)
                .OnComplete(() => { Destroy(gameObject); });
        }
        else
        {
            MaterialChanger.SetTransparency(gameObject, 0, speedDepth);
            transform.DOScale(transform.localScale * 4, speedDepth).SetEase(Ease.InCubic)
                .OnComplete(() => { Destroy(gameObject); });
        }
    }
}
