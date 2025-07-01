using UnityEngine;

public class CameraMoveManager : MonoBehaviour
{
    public Camera MainCamera;
    private Vector3 _dragOrigin;
    void Update()
    {
        HandleMousePan();
        HandleMouseScroll();
    }

    void HandleMouseScroll()
    {
        float scroll = InputActionManager.InputActionsMap.Mouse.MouseScroll.ReadValue<float>();
        MainCamera.orthographicSize = Mathf.Clamp(MainCamera.orthographicSize + (scroll * 0.5f), 1f, 100f);
    }

    void HandleMousePan()
    {
        // Mouse ile pan başlat
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        // Sürükleme işlemi
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = _dragOrigin - MainCamera.ScreenToWorldPoint(Input.mousePosition);
            MainCamera.transform.position += difference;
        }
    }
}