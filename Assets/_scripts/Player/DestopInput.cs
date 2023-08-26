using UnityEngine;

public class DestopInput : IInput
{
    public bool OnHold() => Input.GetMouseButton(0);
    public bool OnRelease() => Input.GetMouseButtonUp(0);
    public bool OnTouch() => Input.GetMouseButtonDown(0);
}