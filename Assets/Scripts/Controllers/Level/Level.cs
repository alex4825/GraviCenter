using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int number;
    [SerializeField] GameObject sparkObject;
    [SerializeField] int sparksNumber = 10;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject ballPrefab;

    public delegate void LevelAction();
    public static event LevelAction OnBallMovedToStart;
    public static event LevelAction OnLevelInitiated;

    public int Number { get { return number; } }
    public int EnergyAmount { get { return GetComponent<Energy>().Amount; } }
    public List<Vector3> Floors { get; set; }
    public GameObject Ball { get; private set; }
    public List<GameObject> GCs { get; private set; } = new List<GameObject>();
    public List<GameObject> Sparks { get; private set; }

    public void Restart(float restartTimeInSec)
    {
        StartCoroutine(MoveBallToStart(restartTimeInSec));
    }
    private IEnumerator MoveBallToStart(float restartTime)
    {
        Rigidbody ballRb = Ball.GetComponent<Rigidbody>();
        Collider ballCollider = Ball.GetComponent<Collider>();

        //ball falls some time
        yield return new WaitForSeconds((float)restartTime / 4);

        ballRb.isKinematic = true;
        ballCollider.enabled = false;

        //ball is hanging some time
        yield return new WaitForSeconds((float)restartTime / 8);

        GetComponent<FloorSelector>().enabled = false;
        Animator.ScaleDisappear(Ball.transform);
        Destroyer.DeleteObjectList(GCs);
        Destroyer.DeleteObjectList(Sparks);

        //ball moves to start some time
        yield return Ball.transform.DOMove(startPoint.transform.position, restartTime / 2).WaitForCompletion();

        Animator.ScaleAppear(Ball.transform);

        yield return new WaitForSeconds((float)restartTime / 8);

        Initiate();

        yield return new WaitForSeconds(Destroyer.SpeedDepth);

        ballRb.isKinematic = false;
        ballCollider.enabled = true;
        OnBallMovedToStart?.Invoke();
    }

    public void Initiate()
    {
        if (GameManager.Instance.GameState == GameStates.Started)
            Ball = Instantiate(ballPrefab, startPoint.transform.position, Quaternion.identity, transform);

        Floors = FloorChecker.FindFloors(this);
        GCs = new List<GameObject>();
        Sparks = Distributer.RandomizePositions(this, sparkObject, sparksNumber);

        GetComponent<FloorSelector>().enabled = true;

        OnLevelInitiated?.Invoke();
    }
}