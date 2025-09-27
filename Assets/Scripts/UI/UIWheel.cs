using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System;

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
    }

    public void SetSelectionByIndex(int index, bool isSelected)
    {
        if(index < 0 || index >= _uiWheelOptions.Count)
        {
            Debug.LogError("UIWheel: Index out of Bounds for Wheel Options Selection");
            return;
        }
         
        _uiWheelOptions[index].SetSelection(isSelected);
    }

    public int GetOptionsCount()
    {
        return _uiWheelOptions.Count;
    }

    public int GetWheelOptionIndex(UIWheelOption option)
    {
        return _uiWheelOptions.IndexOf(option);
    }

    public void ShowWheel(float delay = 0, Action OnComplete = null)
    {
        this.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(delay);
        _leftWheel.transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutBack).SetDelay(delay+0.1f);
        _rightWheel.transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutBack).SetDelay(delay + 0.1f).OnComplete(() =>
        {
            OnComplete?.Invoke();
        });
    }

    public void HideWheel(float delay = 0, Action OnComplete = null)
    {
        this.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetDelay(delay).OnComplete(() =>
        {
            OnComplete?.Invoke();
        }); ;
        _leftWheel.transform.DOLocalRotate(new Vector3(0,0,-45), 0.5f).SetEase(Ease.InBack).SetDelay(delay - 0.1f);
        _rightWheel.transform.DOLocalRotate(new Vector3(0, 0, 45), 0.5f).SetEase(Ease.InBack).SetDelay(delay - 0.1f);
    }

    private void InitWheelForAnimation()
    {
        this.transform.localScale = Vector3.zero;
        _leftWheel.transform.rotation = Quaternion.Euler(0, 0,-45);
        _rightWheel.transform.rotation = Quaternion.Euler(0, 0, 45);
    }
}
