using System.Threading.Tasks;
using UnityEngine;

public class ShapeHolder : MonoBehaviour
{
    private Rigidbody[] _rbs;
    private float _delayToSwitch = 2f;
    private bool _isBreak = false;

    public async void Break()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        if (_isBreak) return;
        _isBreak = true;

        foreach (Rigidbody rb in _rbs)
        {
            rb.transform.parent = null;
            rb.isKinematic = false;
        }

        await WaitForBreak();

        EventBus.OnShapeBroke?.Invoke();
        Destroy(gameObject);
    }

    private async Task WaitForBreak()
    {
        await Task.Delay((int)((_delayToSwitch) * 1000));
    }
}
