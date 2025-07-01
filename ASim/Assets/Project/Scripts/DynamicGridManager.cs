using UnityEngine;

public class DynamicGridManager : MonoBehaviour
{
    public Camera MainCamera;
    public Material DynamicGridMaterial;
    public GameObject GridObject;

    [Min(0.01f)] public float GridCellSize = 1.0f;
    public Color GridColor = Color.white;
    [Min(0.01f)] public float GridThickness = 0.01f;

    // Önceki değerleri tut
    private float prevCellSize;
    private Color prevGridColor;
    private float prevGridThickness;

    private void Start()
    {
        UpdateShaderProperties(forceUpdate: true);
    }

    private void Update()
    {
        // Grid objesini kamera pozisyonuna taşı
        GridObject.transform.position = MainCamera.transform.position + MainCamera.transform.forward * 1f;
        GridObject.transform.localScale = new Vector3(MainCamera.orthographicSize * 4f, MainCamera.orthographicSize * 2.5f, 1);

        UpdateShaderProperties();
    }

    void UpdateShaderProperties(bool forceUpdate = false)
    {
        GridCellSize = GridManager.GetGridCellSize(MainCamera.orthographicSize);
        
        bool changed = forceUpdate ||
         GridCellSize != prevCellSize ||
          GridColor != prevGridColor ||
           GridThickness != prevGridThickness;

        if (!changed) return;

        DynamicGridMaterial.SetFloat("_CellSize", GridCellSize);
        DynamicGridMaterial.SetColor("_GridColor", GridColor);
        DynamicGridMaterial.SetFloat("_LineThickness", GridThickness);

        prevCellSize = GridCellSize;
        prevGridColor = GridColor;
        prevGridThickness = GridThickness;
    }
}
