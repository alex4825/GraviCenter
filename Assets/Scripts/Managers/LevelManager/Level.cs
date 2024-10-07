using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int number;
    [SerializeField] int energyAmount = 500;
    [SerializeField] float restartTimeInSec = 1f;
    [SerializeField] float fallTime = 1f;
    [SerializeField] TextMeshProUGUI energyAmountTMP;
    public int Number { get { return number; } }
    public bool IsActive { get; set; }
    public int EnergyAmount { get { return energyAmount; } set { energyAmount = value; } }
    public List<Vector3> Floors { get; set; }
    public List<GameObject> GCs { get; set; }

    public delegate void BallMoveAction(Transform ball);
    public static event BallMoveAction OnBallMovedToStart;

    void Update()
    {
        if (EnergyAmount <= 0)
        {
            if (ShortcutManager.SelectedGC != null)
            {
                Destroyer.DeleteGC(ShortcutManager.SelectedGC);
            }

            for (int i = 0; i < GCs.Count; i++)
            {
                Destroyer.DeleteGC(GCs[i]);
                i--;
            }
        }
    }
    private void OnEnable()
    {
        IsActive = true;
        Floors = FloorChecker.FindFloors(this);
        GCs = new List<GameObject>();

        energyAmountTMP.text = energyAmount.ToString();

        BallController.OnChangeEnergy += ChangeEnergyAmount;
        GraviCenter.OnChangeEnergy += ChangeEnergyAmount;
        BallController.OnBallFell += Restart;
    }
    private void OnDisable()
    {
        BallController.OnChangeEnergy -= ChangeEnergyAmount;
        GraviCenter.OnChangeEnergy -= ChangeEnergyAmount;
        BallController.OnBallFell -= Restart;
    }

    private void ChangeEnergyAmount(int energySummand)
    {
        energyAmount += energySummand;
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
        Transform startPoint = Searcher.FindChildWithTag(transform, "StartPoint");

        //ball falls some time
        yield return new WaitForSeconds(fallTime);

        ballRb.isKinematic = true;
        ballCollider.enabled = false;

        //ball is hanging some time
        yield return new WaitForSeconds(fallTime / 2);

        //ball moves to start some time
        yield return ball.DOMove(startPoint.position, restartTimeInSec).WaitForCompletion();

        ballRb.isKinematic = false;
        ballCollider.enabled = true;
        ball.GetComponent<BallController>().IsAbove = true;
        OnBallMovedToStart?.Invoke(ball);
    }
}
