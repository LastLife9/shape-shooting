using UnityEngine;

[RequireComponent(typeof(ShootingController))]
public class PlayerController : MonoBehaviour
{
    private ShootingController _shootingController;
    private IInput _input;
    private bool _canInput;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;

        _shootingController = GetComponent<ShootingController>();
        _input = new DestopInput();
    }

    private void OnEnable()
    {
        EventBus.OnNewShapeSpawn += DisableInput;
        EventBus.OnShapeBroke += DisableInput;
        EventBus.OnCameraEndRotate += EnableInput;
    }

    private void OnDisable()
    {
        EventBus.OnNewShapeSpawn -= DisableInput;
        EventBus.OnShapeBroke -= DisableInput;
        EventBus.OnCameraEndRotate -= EnableInput;
    }

    private void Update()
    {
        if (!_canInput) return;

        if (_input.OnRelease())
        {
            _shootingController.Shoot();
        }
    }

    private void DisableInput() => _canInput = false;
    private void EnableInput() => _canInput = true;
}
