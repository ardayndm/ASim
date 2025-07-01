using UnityEngine;

public class DynamicGridManager : MonoBehaviour
{
    public Camera MainCamera;
    public Material DynamicGridMaterial;
    public GameObject GridObject;
    public Color GridColor = Color.white;
    public GameObject OriginMarkerObject;
    [Min(0.01f)] public float GridThickness = 0.01f;
    [Min(0.01f)] public float OriginMarkerSize = 1f;

    // Önceki değerleri tut
    private float _gridCellSize;
    private float prevCellSize;
    private Color prevGridColor;
    private float prevGridThickness;

    private void Start()
    {
        UpdateShaderProperties(forceUpdate: true);
    }

    private void OnValidate()
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
        _gridCellSize = GridManager.GetGridCellSize(MainCamera.orthographicSize);

        bool changed = forceUpdate ||
        _gridCellSize != prevCellSize ||
          GridColor != prevGridColor ||
           GridThickness != prevGridThickness;

        if (!changed) return;

        OriginMarkerObject.transform.localScale = new Vector3(_gridCellSize * OriginMarkerSize, _gridCellSize * OriginMarkerSize, 0f);
        DynamicGridMaterial.SetFloat("_CellSize", _gridCellSize);
        DynamicGridMaterial.SetColor("_GridColor", GridColor);
        DynamicGridMaterial.SetFloat("_LineThickness", GridThickness);

        prevCellSize = _gridCellSize;
        prevGridColor = GridColor;
        prevGridThickness = GridThickness;
    }
}
