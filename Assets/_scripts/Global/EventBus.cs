using System;
using UnityEngine;

public static class EventBus
{
    public static Action<Transform> OnNewShapeSpawnWithT { get; set; }
    public static Action OnNewShapeSpawn { get; set; }
    public static Action OnCameraEndRotate { get; set; }
    public static Action OnShapeBroke { get; set; }
}