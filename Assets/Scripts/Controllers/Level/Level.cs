using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    int energyAmount;
    private float fallTime = 2f;

    [SerializeField] int number;
    [SerializeField] int startEnergyAmount = 500;
    [SerializeField] TextMeshProUGUI energyAmountTMP;
    [SerializeField] float restartTimeInSec = 1f;
    [SerializeField] GameObject sparkObject;
    [SerializeField] int sparksNumber = 10;
    [SerializeField] GameObject startPoint;

    public delegate void BallMoveAction(Transform ball);
    public static event BallMoveAction OnBallMovedToStart;

    public int Number { get { return number; } }
    public int EnergyAmount { get { return energyAmount; } }
    public List<Vector3> Floors { get; set; }
    public List<GameObject> GCs { get; set; }

    private void Start()
    {
        GCs = new List<GameObject>();
        energyAmount = startEnergyAmount;
        energyAmountTMP.text = energyAmount.ToString();
    }
    private void Update()
    {
        if (energyAmount <= 0)
        {
            Destroyer.DeleteObjectList(GCs);
            GCs.Clear();
        }
    }
    private void OnEnable()
    {
        //GameManager.OnGameStateChanged +=
        BallController.OnBallCatchSpark += AddEnergyAmount;
        GraviCenter.OnChangeEnergy += AddEnergyAmount;
        //BallController.OnBallFell += Restart;
    }
    private void OnDisable()
    {
        BallController.OnBallCatchSpark -= AddEnergyAmount;
        GraviCenter.OnChangeEnergy -= AddEnergyAmount;
        //BallController.OnBallFell -= Restart;
    }

    private void AddEnergyAmount(int energySummand)
    {
        energyAmount += energySummand;
        energyAmountTMP.text = energyAmount.ToString();
    }

    private void ResetEnergyAmount()
    {
        energyAmount = startEnergyAmount;
        energyAmountTMP.text = energyAmount.ToString();
    }

    private void Restart(Transform ball)
    {
        StartCoroutine(RestartTimer(ball));
    }
    private IEnumerator RestartTimer(Transform ball)
    {
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        Collider ballCollider = ball.GetComponent<Collider>();

        //ball falls some time
        yield return new WaitForSeconds(fallTime);

        ballRb.isKinematic = true;
        ballCollider.enabled = false;

        //ball is hanging some time
        yield return new WaitForSeconds(fallTime / 2);

        //ball moves to start some time
        yield return ball.DOMove(startPoint.transform.position, restartTimeInSec).WaitForCompletion();

        ballRb.isKinematic = false;
        ballCollider.enabled = true;
        ball.GetComponent<BallController>().IsAbove = true;
        OnBallMovedToStart?.Invoke(ball);

        ResetEnergyAmount();
    }

    public void Initiate(GameObject ballPrefab)
    {
        Floors = FloorChecker.FindFloors(this);
        Distributer.SetRandPositions(this, sparkObject, sparksNumber);
        Instantiate(ballPrefab, startPoint.transform.position, Quaternion.identity, transform);
    }
}