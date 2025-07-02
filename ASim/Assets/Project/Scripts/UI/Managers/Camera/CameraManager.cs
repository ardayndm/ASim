using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Zoom")]
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 100f;
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float defaultZoom = 5f;

    [Header("UI")]
    [SerializeField] private Button zoomInButton;
    [SerializeField] private Button zoomOutButton;
    [SerializeField] private Button resetZoomButton;
    [SerializeField] private Button resetPositionButton;
    [SerializeField] private TextMeshProUGUI zoomLevelText;
    [SerializeField] private Scrollbar zoomScrollbar;
    [SerializeField] private UIHoverHelper hoverHelper;

    private Vector3 dragOrigin;

    /// <summary>Kamerayı kilitleyen objeleri tutar.</summary>
    public static List<CameraLocker> LockCameraList { get; private set; } = new();

    private void Start()
    {
        hoverHelper.OnHovered += () => LockCamera(gameObject, this);
        hoverHelper.OnHoverExit += () => UnlockCamera(gameObject, this);

        zoomInButton.onClick.AddListener(() => AdjustZoom(-5f));
        zoomOutButton.onClick.AddListener(() => AdjustZoom(5f));
        resetZoomButton.onClick.AddListener(() => SetZoom(defaultZoom));
        resetPositionButton.onClick.AddListener(ResetPosition);

        zoomScrollbar.onValueChanged.AddListener(value =>
        {
            float targetZoom = Mathf.Lerp(minZoom, maxZoom, value);
            SetZoom(targetZoom);
        });
    }

    private void Update()
    {
        if (mainCamera == null) return;

        UpdateZoomUI();

        if (LockCameraList.Count > 0) return;

        HandlePan();
        HandleMouseWheelZoom();
    }

    private void AdjustZoom(float delta)
    {
        SetZoom(mainCamera.orthographicSize + delta);
    }

    private void SetZoom(float zoom)
    {
        mainCamera.orthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);
    }

    private void ResetPosition()
    {
        mainCamera.transform.position = new Vector3(0, 0, -1);
    }

    private void UpdateZoomUI()
    {
        float value = Mathf.InverseLerp(minZoom, maxZoom, mainCamera.orthographicSize);
        zoomScrollbar.SetValueWithoutNotify(value);
        zoomLevelText.text = $"{mainCamera.orthographicSize:0.0}x";
    }

    private void HandleMouseWheelZoom()
    {
        float scroll = InputActionManager.InputActionsMap.Mouse.MouseScroll.ReadValue<float>();
        float speed = InputActionManager.InputActionsMap.Keys.LShift.IsPressed() ? 10f : 1f;
        AdjustZoom(scroll * zoomSpeed * speed);
    }

    private void HandlePan()
    {
        var scrollPress = InputActionManager.InputActionsMap.Mouse.MouseScrollPress;

        if (scrollPress.WasPressedThisFrame())
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (scrollPress.IsPressed())
        {
            Vector3 delta = dragOrigin - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mainCamera.transform.position += delta;
        }
    }

    /// <summary>Kamerayı kilitler.</summary>
    public static void LockCamera(GameObject from, object locker)
    {
        if (from == null || locker == null) return;

        foreach (var item in LockCameraList)
            if (item.lockedFrom == from && item.locker == locker)
                return;

        LockCameraList.Add(new CameraLocker(from, locker));
    }

    /// <summary>Kilidi kaldırır.</summary>
    public static void UnlockCamera(GameObject from, object locker)
    {
        if (from == null || locker == null) return;

        LockCameraList.RemoveAll(x => x.lockedFrom == from && x.locker == locker);
    }
}
