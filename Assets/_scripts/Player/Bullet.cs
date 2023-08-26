using UnityEngine;

public class Bullet : MonoBehaviour
{
    ColorManager _colorManager;
    Transform _transform;
    MeshRenderer _meshRenderer;
    [SerializeField] private float _speed = 10f;

    private void Awake()
    {
        _transform = transform; 
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start() => _colorManager = ColorManager.Instance;

    private void OnEnable()
    {
        if (_colorManager == null) _colorManager = ColorManager.Instance;
        _meshRenderer.material.color = _colorManager.GetRandomColor();
    }

    private void Update() => _transform.Translate(_transform.forward * _speed * Time.deltaTime, Space.World);

    private void OnTriggerEnter(Collider other)
    {
        ShapeHolder shapeHolder = other.GetComponentInParent<ShapeHolder>();
        if(shapeHolder != null) shapeHolder.Break();
        gameObject.SetActive(false);
    }
}
