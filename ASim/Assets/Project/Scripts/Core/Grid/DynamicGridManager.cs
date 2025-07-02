using UnityEngine;

/// <summary>
/// Kameraya göre dinamik olarak ayarlanan grid sistemini yönetir.
/// Grid boyutu, kalınlığı ve rengi gibi özellikleri shader'a aktarır.
/// </summary>
public class DynamicGridManager : MonoBehaviour
{
    [Header("Bağlantılar")]
    public Camera MainCamera;
    public GameObject GridObject;
    public GameObject OriginMarkerObject;
    public Material DynamicGridMaterial;

    [Header("Grid Ayarları")]
    public Color GridColor = Color.white;
    [Min(0.01f)] public float GridThickness = 0.01f;
    [Min(0.01f)] public float OriginMarkerSize = 1f;

    // Dahili değerler
    private float _gridCellSize;
    private float _prevCellSize;
    private Color _prevGridColor;
    private float _prevGridThickness;

    private void Start()
    {
        UpdateShaderProperties(forceUpdate: true);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateShaderProperties(forceUpdate: true);
    }
#endif

    private void Update()
    {
        UpdateGridTransform();
        UpdateShaderProperties();
    }

    /// <summary>
    /// Grid nesnesini kameranın konumuna göre hizalar ve ölçeklendirir.
    /// </summary>
    private void UpdateGridTransform()
    {
        GridObject.transform.position = MainCamera.transform.position + MainCamera.transform.forward * 1f;
        GridObject.transform.localScale = new Vector3(
            MainCamera.orthographicSize * 4f,
            MainCamera.orthographicSize * 2.5f,
            1f
        );
    }

    /// <summary>
    /// Grid'e ait shader özelliklerini günceller.
    /// Gereksiz güncellemeleri önlemek için önceki değerlerle karşılaştırır.
    /// </summary>
    /// <param name="forceUpdate">Tüm değerleri zorla güncelle.</param>
    private void UpdateShaderProperties(bool forceUpdate = false)
    {
        _gridCellSize = GridManager.GetGridCellSize(MainCamera.orthographicSize);

        bool hasChanged = forceUpdate ||
                          _gridCellSize != _prevCellSize ||
                          GridColor != _prevGridColor ||
                          GridThickness != _prevGridThickness;

        if (!hasChanged) return;

        // Origin işaretçisini güncelle
        OriginMarkerObject.transform.localScale = Vector3.one * _gridCellSize * OriginMarkerSize;

        // Shader parametrelerini güncelle
        DynamicGridMaterial.SetFloat("_CellSize", _gridCellSize);
        DynamicGridMaterial.SetColor("_GridColor", GridColor);
        DynamicGridMaterial.SetFloat("_LineThickness", GridThickness);

        // Önceki değerleri güncelle
        _prevCellSize = _gridCellSize;
        _prevGridColor = GridColor;
        _prevGridThickness = GridThickness;
    }
}
