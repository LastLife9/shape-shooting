using UnityEngine;

public class RectangleShape : IShape
{
    private GameObject[] _prefab;
    private Color[] _colors;
    private int _width;
    private int _height;

    public RectangleShape(GameObject[] prefab, Color[] colors, int width, int height)
    {
        _prefab = prefab;
        _colors = colors;
        _width = width;
        _height = height;
    }

    public Transform GenerateShape()
    {
        GameObject parentGO = new GameObject();
        parentGO.AddComponent<ShapeHolder>();
        Transform parent = parentGO.transform;
        float cubeSize = 1f;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                float xPos = x * cubeSize - _width / 2;
                float yPos = y * cubeSize - _height / 2;

                Vector3 position = new Vector3(xPos, yPos, 0);
                GameObject newObject = GameObject.Instantiate(_prefab[Random.Range(0, _prefab.Length)], position, Quaternion.identity);
                newObject.transform.SetParent(parent);
                MeshRenderer meshRenderer = newObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material.color = _colors[Random.Range(0, _colors.Length)];
                }
            }
        }

        return parent;
    }
}

public class SphereShape : IShape
{
    private GameObject[] _prefab;
    private Color[] _colors;
    private int _numPoints;
    private float _radius;

    public SphereShape(GameObject[] prefab, Color[] colors, int numPoints, float radius)
    {
        _prefab = prefab;
        _colors = colors;
        _numPoints = numPoints;
        _radius = radius;
    }

    public Transform GenerateShape()
    {
        GameObject parentGO = new GameObject();
        parentGO.AddComponent<ShapeHolder>();
        Transform parent = parentGO.transform;

        for (int lat = 0; lat < _numPoints; lat++)
        {
            float theta = Mathf.PI * (float)lat / _numPoints;
            for (int lon = 0; lon < _numPoints; lon++)
            {
                float phi = 2 * Mathf.PI * (float)lon / _numPoints;
                float x = _radius * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = _radius * Mathf.Cos(theta);
                float z = _radius * Mathf.Sin(theta) * Mathf.Sin(phi);

                Vector3 position = new Vector3(x, y, z);
                GameObject newObject = GameObject.Instantiate(_prefab[Random.Range(0, _prefab.Length)], position, Quaternion.identity);
                newObject.transform.SetParent(parent);
                MeshRenderer meshRenderer = newObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material.color = _colors[Random.Range(0, _colors.Length)];
                }
            }
        }

        return parent;
    }
}

public class PiramideShape : IShape
{
    private GameObject[] _prefab;
    private Color[] _colors;
    private int _heigth;

    public PiramideShape(GameObject[] prefab, Color[] colors, int heigth)
    {
        _prefab = prefab;
        _colors = colors;
        _heigth = heigth;
    }

    public Transform GenerateShape()
    {
        float cubeSize = 1f;
        GameObject parentGO = new GameObject();
        parentGO.AddComponent<ShapeHolder>();
        Transform parent = parentGO.transform;

        for (int row = 0; row < _heigth; row++)
        {
            int numCubesInRow = _heigth - row;
            float rowOffset = -numCubesInRow / 2f;

            for (int i = 0; i < numCubesInRow; i++)
            {
                float x = i + rowOffset;
                float y = row + cubeSize / 2;
                float z = 0f;

                Vector3 position = new Vector3(x, y, z);
                GameObject newObject = GameObject.Instantiate(_prefab[Random.Range(0, _prefab.Length)], position, Quaternion.identity);
                newObject.transform.SetParent(parent);
                MeshRenderer meshRenderer = newObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material.color = _colors[Random.Range(0, _colors.Length)];
                }
            }
        }

        return parent;
    }
}
