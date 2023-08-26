using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachinePOV _pov;

    [SerializeField] private float _recenteringTime = 0.5f;
    [SerializeField] private float _maxSpeed = 5f;

    private void Awake() => InitCamera();

    private void OnEnable()
    {
        EventBus.OnNewShapeSpawnWithT += SetLookAtTarget;
    }

    private void OnDisable()
    {
        EventBus.OnNewShapeSpawnWithT -= SetLookAtTarget;
    }

    private async void SetLookAtTarget(Transform target)
    {
        _virtualCamera.LookAt = target;
        _pov.m_HorizontalRecentering.m_enabled = true;
        _pov.m_VerticalRecentering.m_enabled = true;
        _pov.m_HorizontalAxis.m_MaxSpeed = 0f;
        _pov.m_VerticalAxis.m_MaxSpeed = 0f;

        await WaitForRecentering();

        _pov.m_HorizontalRecentering.m_enabled = false;
        _pov.m_VerticalRecentering.m_enabled = false;
        _pov.m_HorizontalAxis.m_MaxSpeed = _maxSpeed;
        _pov.m_VerticalAxis.m_MaxSpeed = _maxSpeed;
        EventBus.OnCameraEndRotate?.Invoke();
    }

    private async Task WaitForRecentering() => await Task.Delay((int)((_recenteringTime) * 1000));

    private void InitCamera()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _pov = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        _pov.m_RecenterTarget = CinemachinePOV.RecenterTargetMode.LookAtTargetForward;
    }
}
