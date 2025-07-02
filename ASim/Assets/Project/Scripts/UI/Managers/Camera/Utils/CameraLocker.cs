using UnityEngine;

/// <summary> Kamera hareket kilit bilgisini tutar. </summary>
public struct CameraLocker
{
    public object locker;
    public GameObject lockedFrom;

    public CameraLocker(GameObject lockedFrom, object locker)
    {
        this.lockedFrom = lockedFrom;
        this.locker = locker;
    }
}