using UnityEngine;

public static class GridManager
{
    /// <summary>
    /// Grid pozisyonunu döndürür.
    /// </summary>
    /// <param name="orthographicSize">Kameranın orthographic size'i</param>
    public static float GetGridCellSize(float orthographicSize)
    {
        int step = Mathf.FloorToInt(orthographicSize / 6f);
        return Mathf.Max(1f, step + 1); // Minimum 1
    }
}