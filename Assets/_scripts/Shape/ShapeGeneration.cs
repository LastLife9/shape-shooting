using System.Collections;
using UnityEngine;

public class ShapeGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _yOffset = 5f;
    [SerializeField] private float _minDistance = 5f;
    [SerializeField] private float _maxDistance = 20f;
    private IShape[] _shapes;

    private void OnEnable()
    {
        EventBus.OnShapeBroke += GenerateRandomShape;
    }

    private void OnDisable()
    {
        EventBus.OnShapeBroke -= GenerateRandomShape;
    }

    void Start()
    {
        InitShapes();
        GenerateRandomShape();
    }

    private void GenerateRandomShape()
    {
        IShape randShape = _shapes[Random.Range(0, _shapes.Length)];

        Transform shapeParent = randShape.GenerateShape();
        shapeParent.position = GetRandomPosition(transform);
        shapeParent.rotation = Quaternion.LookRotation(shapeParent.position - transform.position);

        EventBus.OnNewShapeSpawnWithT?.Invoke(shapeParent);
        EventBus.OnNewShapeSpawn?.Invoke();
    }

    private Vector3 GetRandomPosition(Transform playerTransform)
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * Random.Range(_minDistance, _maxDistance);
        Vector3 randomPosition = playerTransform.position + new Vector3(randomCirclePoint.x, _yOffset, randomCirclePoint.y);
        return randomPosition;
    }

    private void InitShapes()
    {
        Color[] colors = ColorManager.Instance.GetColorsArray();
        _shapes = new IShape[4];
        _shapes[0] = new RectangleShape(_prefabs, colors, 5, 5);
        _shapes[1] = new PiramideShape(_prefabs, colors, 6);
        _shapes[2] = new SphereShape(_prefabs, colors, 10, 2);
        _shapes[3] = new RectangleShape(_prefabs, colors, 5, 10);
    }
}
