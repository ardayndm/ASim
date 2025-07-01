using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveManager : MonoBehaviour
{
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

    [Header("Camera")]
    public Camera MainCamera;

    [Header("Zoom Settings")]
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 100f;
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float defaultZoom = 5f;

    public Button ZoomInButton;
    public Button ZoomOutButton;
    public Button ResetZoomButton;
    public Button ResetPositionButton;
    public TextMeshProUGUI ZoomLevelText;
    public Scrollbar ZoomLevelScrollbar;
    public UIHoverHelper CamInfoHoverHelper;

    private Vector3 _dragOrigin;

    /// <summary> Aktif kamera kilitleri </summary>
    public static List<CameraLocker> LockCameraList { get; private set; } = new();

    void Start()
    {
        CamInfoHoverHelper.OnHovered += () => LockCamera(gameObject, this);
        CamInfoHoverHelper.OnHoverExit += () => UnlockCamera(gameObject, this);

        ZoomInButton.onClick.AddListener(() => ChangeZoom(true));
        ZoomOutButton.onClick.AddListener(() => ChangeZoom(false));
        ResetZoomButton.onClick.AddListener(() => SetZoom(defaultZoom));
        ResetPositionButton.onClick.AddListener(ResetPosition);

        ZoomLevelScrollbar.onValueChanged.AddListener(value => SetZoom(Mathf.Lerp(minZoom, maxZoom, value)));
    }

    void Update()
    {
        if (MainCamera == null) return;

        ZoomLevelScrollbar.SetValueWithoutNotify(Mathf.InverseLerp(minZoom, maxZoom, MainCamera.orthographicSize));
        ZoomLevelText.text = $"{MainCamera.orthographicSize:0.0}x";

        if (LockCameraList.Count > 0) return;

        HandlePan();
        HandleScrollZoom();
    }

    /// <summary>
    /// Zoomu arttırır veya azaltır.
    /// </summary>
    /// <param name="zoomIn">True ise zoom in, false ise zoom out</param>
    private void ChangeZoom(bool zoomIn)
    {
        float delta = zoomIn ? -5f : 5f;
        SetZoom(MainCamera.orthographicSize + delta);
    }

    /// <summary>
    /// Zoom seviyesini sınırlar ve ayarlar.
    /// </summary>
    private void SetZoom(float size)
    {
        MainCamera.orthographicSize = Mathf.Clamp(size, minZoom, maxZoom);
    }

    /// <summary>
    /// Kamerayı varsayılan pozisyona sıfırlar.
    /// </summary>
    private void ResetPosition()
    {
        MainCamera.transform.position = new Vector3(0, 0, -1);
    }

    /// <summary>
    /// Fare tekerleği ile zoom kontrolü yapar.
    /// </summary>
    private void HandleScrollZoom()
    {
        float scroll = InputActionManager.InputActionsMap.Mouse.MouseScroll.ReadValue<float>();
        float speed = InputActionManager.InputActionsMap.Keys.LShift.IsPressed() ? 10f : 1f;
        SetZoom(MainCamera.orthographicSize + scroll * zoomSpeed * speed);
    }

    /// <summary>
    /// Kamera sürükleme işlemini yönetir.
    /// </summary>
    private void HandlePan()
    {
        var mousePressed = InputActionManager.InputActionsMap.Mouse.MouseScrollPress;

        if (mousePressed.WasPressedThisFrame())
            _dragOrigin = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (mousePressed.IsPressed())
        {
            Vector3 currentPos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = _dragOrigin - currentPos;
            MainCamera.transform.position += delta;
        }
    }

    /// <summary>
    /// Kamerayı kilitler, aynı kilit birden eklenmez.
    /// </summary>
    public static void LockCamera(GameObject lockedFrom, object locker)
    {
        if (lockedFrom == null || locker == null) return;

        foreach (var item in LockCameraList)
            if (item.lockedFrom == lockedFrom && item.locker == locker)
                return;

        LockCameraList.Add(new CameraLocker(lockedFrom, locker));
    }

    /// <summary>
    /// Kameranın kilidini kaldırır.
    /// </summary>
    public static void UnlockCamera(GameObject lockedFrom, object locker)
    {
        if (lockedFrom == null || locker == null) return;

        LockCameraList.RemoveAll(x => x.lockedFrom == lockedFrom && x.locker == locker);
    }
}
