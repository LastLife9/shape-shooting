using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private const string _bulletTag = "Bullet";
    private ObjectPooling _pooling;

    [SerializeField] private Transform _firePoint;

    private void Start() => _pooling = ObjectPooling.Instance;
    public void Shoot() => _pooling.SpawnFromPool(_bulletTag, _firePoint.position, _firePoint.rotation);
}