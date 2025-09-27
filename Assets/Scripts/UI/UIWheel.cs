using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class UIWheel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _centerWheel;
    [SerializeField]
    private GameObject _leftWheel;
    [SerializeField]
    private GameObject _rightWheel;
    [SerializeField]
    private List<UIWheelOption> _uiWheelOptions;

    private void Start()
    {
        InitWheelForAnimation();

        // Test Only
        ShowWheel(2.0f);
        // ==============
    }

    public void ShowWheel(float delay = 0)
    {
        this.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(delay);
        _leftWheel.transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutBack).SetDelay(delay+0.1f);
        _rightWheel.transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutBack).SetDelay(delay+0.1f);
    }

    public void HideWheel()
    {
        // TODO
    }

    private void InitWheelForAnimation()
    {
        this.transform.localScale = Vector3.zero;
        _leftWheel.transform.rotation = Quaternion.Euler(0, 0,-45);
        _rightWheel.transform.rotation = Quaternion.Euler(0, 0, 45);
    }
}
