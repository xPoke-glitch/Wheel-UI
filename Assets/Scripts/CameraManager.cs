using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraManager : Singleton<CameraManager>
{
    [Header("References")]
    [SerializeField]
    private CinemachineCamera _cinemachineCamera;

    private void OnEnable()
    {
        Entity.OnEntityClicked += MoveToEntity;
        AbilityExit.OnAbilityExitActivated += ResetPosition;
    }

    private void OnDisable()
    {
        Entity.OnEntityClicked -= MoveToEntity;
        AbilityExit.OnAbilityExitActivated -= ResetPosition;
    }

    public void MoveToEntity(Entity entity)
    {
        Debug.Log("CameraManager: Moving camera to entity " + entity.name);
        _cinemachineCamera.transform.DOMoveX(entity.transform.position.x, 1f).SetEase(Ease.OutSine);
        _cinemachineCamera.transform.DOMoveZ(entity.transform.position.z-10f, 1f).SetEase(Ease.OutSine);
        DOTween.To(() => _cinemachineCamera.Lens.FieldOfView, x => _cinemachineCamera.Lens.FieldOfView = x, 22f, 0.8f).SetEase(Ease.OutSine);
    }

    public void ResetPosition(Entity entity)
    {
        Debug.Log("CameraManager: Resetting camera position");
        DOTween.To(() => _cinemachineCamera.Lens.FieldOfView, x => _cinemachineCamera.Lens.FieldOfView = x, 60f, 0.8f).SetEase(Ease.OutSine);
        _cinemachineCamera.transform.DOMoveX(0f, 1f).SetEase(Ease.OutSine);
        _cinemachineCamera.transform.DOMoveZ(-10f, 1f).SetEase(Ease.OutSine);
    }
}
