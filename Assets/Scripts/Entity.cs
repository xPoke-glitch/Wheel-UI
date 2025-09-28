using UnityEngine;
using System;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnEntityClicked;

    [Header("References")]
    [SerializeField]
    private WheelController _wheelController;

    [Header("Settings")]
    [SerializeField]
    private Vector2 _minAreaCorner;
    [SerializeField]
    private Vector2 _maxAreaCorner;
    [SerializeField]
    private Vector2 _moveDuration;
    [SerializeField]
    private Vector2 _waitTime;

    private void Start()
    {
        MoveToPoint(GetRandomPointInArea());
    }

    private void OnEnable()
    {
        OnEntityClicked += HandleAnyEntityClicked;
        AbilityExit.OnAbilityExitActivated += HandleAnyExitWheelActivated;
    }

    private void OnDisable()
    {
        OnEntityClicked -= HandleAnyEntityClicked;
        AbilityExit.OnAbilityExitActivated -= HandleAnyExitWheelActivated;
    }

    public void Click()
    {
        if(IsWheelActive())
        {
            return;
        }
        StopMovement();
        OnEntityClicked?.Invoke(this);
    }

    public bool IsWheelActive()
    {
        return _wheelController.IsWheelActive;
    }

    private Vector3 GetRandomPointInArea()
    {
        float randomX = UnityEngine.Random.Range(_minAreaCorner.x, _maxAreaCorner.x);
        float randomY = UnityEngine.Random.Range(_minAreaCorner.y, _maxAreaCorner.y);
        return new Vector3(randomX, this.transform.position.y, randomY);
    }

    private void MoveToPoint(Vector3 destination)
    {
        float moveDuration = UnityEngine.Random.Range(_moveDuration.x, _moveDuration.y);
        float waitTime = UnityEngine.Random.Range(_waitTime.x, _waitTime.y);

        this.transform.DOMove(destination,moveDuration).SetEase(Ease.OutSine).OnComplete(() =>
        {
            MoveToPoint(GetRandomPointInArea());
        }).SetDelay(waitTime);
    }

    private void StopMovement()
    {
        this.transform.DOKill();
    }

    private void HandleAnyEntityClicked(Entity entity)
    {
       StopMovement();
    }

    private void HandleAnyExitWheelActivated(Entity entity)
    {
        MoveToPoint(GetRandomPointInArea());
    }
}
