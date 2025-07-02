using UnityEngine;

/// <summary>
/// Grid sistemine ait yardımcı işlemleri yöneten sınıftır.
/// </summary>
public static class GridManager
{
    /// <summary>
    /// Kamera boyutuna göre grid hücre boyutunu hesaplar.
    /// </summary>
    /// <param name="orthographicSize">Kameranın orthographic boyutu.</param>
    /// <returns>Hesaplanan grid hücre boyutu (minimum 1).</returns>
    public static float GetGridCellSize(float orthographicSize)
    {
        int step = Mathf.FloorToInt(orthographicSize / 10f);
        return Mathf.Max(1f, step + 1);
    }
}
