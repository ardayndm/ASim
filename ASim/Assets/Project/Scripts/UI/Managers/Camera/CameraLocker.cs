using UnityEngine;

/// <summary> Kamera hareket kilit bilgisini tutar. </summary>
public struct CameraLocker
{
    public object locker { get; private set; }
    public GameObject lockedFrom { get; private set; }

    public CameraLocker(GameObject lockedFrom, object locker)
    {
        this.lockedFrom = lockedFrom;
        this.locker = locker;
    }
}