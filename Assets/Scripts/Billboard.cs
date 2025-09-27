using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + _mainCamera.rotation * Vector3.forward, _mainCamera.rotation * Vector3.up);
    }
}
