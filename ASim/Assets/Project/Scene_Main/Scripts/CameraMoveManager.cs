using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Kamera sürükleme (pan) ve zoom (scroll) işlemlerini yönetir.
/// Dış müdahaleler için LockCameraList ile kilit kontrolü sağlar.
/// </summary>
public class CameraMoveManager : MonoBehaviour
{
    /// <summary>
    /// Kamera kilidini kimin koyduğunu ve nereden koyduğunu belirten yapı.
    /// </summary>
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

    [Header("Ana Kamera")]
    [Tooltip("Sürükleme ve zoom yapılacak ana kamera referansı")]
    public Camera MainCamera;

    [Header("Zoom Ayarları")]
    [Tooltip("Minimum zoom değeri")]
    [SerializeField] private float minZoom = 1f;

    [Tooltip("Maksimum zoom değeri")]
    [SerializeField] private float maxZoom = 100f;

    [Tooltip("Zoom hızı çarpanı")]
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

    /// <summary>
    /// Kamera hareketini kilitleyen tüm kaynaklar burada tutulur.
    /// </summary>
    public static List<CameraLocker> LockCameraList { get; private set; } = new List<CameraLocker>();

    void Start()
    {
        CamInfoHoverHelper.OnHovered += () => LockCamera(gameObject, this);
        CamInfoHoverHelper.OnHoverExit += () => UnlockCamera(gameObject, this);
        ZoomInButton.onClick.AddListener(() => SetZoomLevel(true));
        ZoomOutButton.onClick.AddListener(() => SetZoomLevel(false));
        ResetZoomButton.onClick.AddListener(() => MainCamera.orthographicSize = defaultZoom);
        ResetPositionButton.onClick.AddListener(() => MainCamera.transform.position = new Vector3(0, 0, -1));
        ZoomLevelScrollbar.onValueChanged.AddListener((value) => MainCamera.orthographicSize = Mathf.Lerp(minZoom, maxZoom, value));
    }

    private void Update()
    {
        if (MainCamera == null) return;

        ZoomLevelScrollbar.SetValueWithoutNotify(Mathf.InverseLerp(minZoom, maxZoom, MainCamera.orthographicSize)); // UI güncellenir ama event tetiklemez
        ZoomLevelText.text = MainCamera.orthographicSize.ToString("0.0") + 'x';
        if (LockCameraList.Count > 0) return;

        HandleMousePan();
        HandleMouseScroll();
    }

    public void SetZoomLevel(bool isZoomIn)
    {
        float targetSize = MainCamera.orthographicSize + (isZoomIn ? -5 : 5);
        MainCamera.orthographicSize = Mathf.Clamp(targetSize, minZoom, maxZoom);
    }

    private void HandleMouseScroll()
    {
        float scroll = InputActionManager.InputActionsMap.Mouse.MouseScroll.ReadValue<float>();
        float shift = InputActionManager.InputActionsMap.Keys.LShift.IsPressed() ? 10f : 1f;
        float targetSize = MainCamera.orthographicSize + scroll * zoomSpeed * shift;
        MainCamera.orthographicSize = Mathf.Clamp(targetSize, minZoom, maxZoom);
    }

    private void HandleMousePan()
    {
        var mouseInput = InputActionManager.InputActionsMap.Mouse.MouseScrollPress;

        if (mouseInput.WasPressedThisFrame())
        {
            _dragOrigin = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (mouseInput.IsPressed())
        {
            Vector3 currentPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = _dragOrigin - currentPosition;
            MainCamera.transform.position += delta;
        }
    }

    /// <summary>
    /// Kamera hareketini kilitler.
    /// Aynı objeden birden fazla kilit varsa yine de sadece bir tane eklenir.
    /// </summary>
    /// <param name="lockedFrom">Kilidin geldiği GameObject</param>
    /// <param name="locker">Kim tarafından kilitlendiği</param>
    public static void LockCamera(GameObject lockedFrom, object locker)
    {
        if (lockedFrom == null || locker == null) return;

        foreach (var item in LockCameraList)
        {
            if (item.lockedFrom == lockedFrom && item.locker == locker)
                return; // zaten eklenmiş
        }

        LockCameraList.Add(new CameraLocker(lockedFrom, locker));
    }

    /// <summary>
    /// Kamera kilidini kaldırır.
    /// </summary>
    /// <param name="lockedFrom">Kilidin geldiği GameObject</param>
    /// <param name="locker">Kim tarafından kilitlendiği</param>
    public static void UnlockCamera(GameObject lockedFrom, object locker)
    {
        if (lockedFrom == null || locker == null) return;

        LockCameraList.RemoveAll(x => x.lockedFrom == lockedFrom && x.locker == locker);
    }
}
